// See https://aka.ms/new-console-template for more information
using Csv;
using Ocsf.Azure.Mapper;
using Ocsf.Schema;
using Parquet.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Parquet.Schema;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Console.WriteLine($"Environment: {environmentName}");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json")
    .AddJsonFile($"appSettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfigurationRoot config = builder.Build();

var section = config.GetRequiredSection("Settings");

Settings? settings = section.Get<Settings>();

Console.WriteLine(settings);

if(settings == null)
{
    Console.WriteLine("Settings not found");
    return;
}

var csvOptions = new CsvOptions // Defaults
{
    RowsToSkip = 0, // Allows skipping of initial rows without csv data
    //SkipRow = (row, idx) => string.IsNullOrEmpty(row) || row[0] == '#',
    //Separator = '\0', // Autodetects based on first row
    Separator = ',', // Autodetects based on first row
    TrimData = false, // Can be used to trim each cell
    Comparer = null, // Can be used for case-insensitive comparison for names
    HeaderMode = HeaderMode.HeaderPresent, // Assumes first row is a header row
    ValidateColumnCount = false, // Checks each row immediately for column count
    ReturnEmptyForMissingColumn = false, // Allows for accessing invalid column names
    Aliases = null, // A collection of alternative column names
    AllowNewLineInEnclosedFieldValues = false, // Respects new line (either \r\n or \n) characters inside field values enclosed in double quotes.
    AllowBackSlashToEscapeQuote = false, // Allows the sequence "\"" to be a valid quoted value (in addition to the standard """")
    AllowSingleQuoteToEncloseFieldValues = false, // Allows the single-quote character to be used to enclose field values
    NewLine = Environment.NewLine // The new line string to use when multiline field values are read (Requires "AllowNewLineInEnclosedFieldValues" to be set to "true" for this to have any effect.)
};

if(settings.ProgramType == "WATCH")
{
    using (FileSystemWatcher watcher = new())
    {
        watcher.Path = settings.SourceFolder;

        // Watch for changes in LastAccess and LastWrite times, and the creation of new files.
        watcher.NotifyFilter = NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.FileName
                               | NotifyFilters.DirectoryName;

        // Only watch CSV files.
        watcher.Filter = "*.csv";

        // Add event handlers.
        watcher.Created += OnCreated;

        // Begin watching.
        watcher.EnableRaisingEvents = true;

        // Wait for the user to quit the program.
        Console.WriteLine("Press 'q' to quit the sample.");
        while (Console.Read() != 'q') ;
    }
}
else
{
    // Read source folder files (*.csv) by time created (oldest first)
    var files = Directory.GetFiles(settings.SourceFolder, "InteractiveSignIns*.csv", SearchOption.TopDirectoryOnly)
        .Select(f => new FileInfo(f))
        .OrderBy(f => f.CreationTimeUtc);

    // Read files one by one
    foreach (var file in files)
    {
        ValidateCSV(file.FullName);

        var list = new List<OcsfRoot>();

        var csv = File.ReadAllText(file.FullName);

        var curDate = DateTime.MinValue;
        var curCount = 0;

        //File has first column as time and need to split data per 5 minutes
        foreach (var line in CsvReader.ReadFromText(csv, csvOptions))
        {
            var ocsf = ActiveDirectoryLogMapper.Map(line);
            if (ocsf == null || ocsf.Time == null)
            {
                Console.WriteLine("ocsf is null or ocsf.Time is null");
                continue;
            }

            var dateTime = DataExtensions.ConvertToDateTime(ocsf.Time.Value);

            if(curDate != dateTime.Date)
            {
                curDate = dateTime.Date;
                curCount = 0;
            }

            var newCount = (int)(dateTime.TimeOfDay.TotalMinutes / 5);

            if (newCount != curCount && list.Count > 0)
            {
                WriteToJson(list, curDate.ToString("yyyyMMdd"), curCount);
                WriteToParquet(list, curDate.ToString("yyyyMMdd"), curCount);
                list.Clear();
                curCount = newCount;
                continue;
            } 

            list.Add(ocsf);
            curCount = newCount;
        }

        if(list.Count() > 0)
        {
            WriteToJson(list, curDate.ToString("yyyyMMdd"), curCount);
            WriteToParquet(list, curDate.ToString("yyyyMMdd"), curCount);
            list.Clear();
        }
    }

    Console.WriteLine("Press any key to exit");
}

void ValidateCSV(string fileName)
{
    Console.WriteLine($"File: {fileName}");

    if (!File.Exists(fileName))
    {
        throw new FileNotFoundException($"The file {fileName} does not exist.");
    }

    var lines = File.ReadAllLines(fileName);

    if (lines.Length == 0)
    {
        throw new InvalidOperationException("The file is empty.");
    }

    // Read headers and trim whitespaces
    var headers = lines[0].Split(',').Select(h => h.Trim('"', ' ')).ToList();

    // Check and update duplicate headers
    var updatedHeaders = UpdateDuplicateHeaders(headers).Select(h => h = $"\"{h}\"").ToList();

    // Replace the first line (header line) with updated headers
    lines[0] = string.Join(",", updatedHeaders);

    // Write back to file
    File.WriteAllLines(fileName, lines);
}

List<string> UpdateDuplicateHeaders(List<string> headers)
{
    var headerCounts = new Dictionary<string, int>();
    var updatedHeaders = new List<string>();

    foreach (var header in headers)
    {
        if (!headerCounts.TryGetValue(header, out int value))
        {
            headerCounts[header] = 1;
            updatedHeaders.Add(header);
        }
        else
        {
            headerCounts[header] = ++value;
            updatedHeaders.Add($"{header}{value}");
        }
    }

    return updatedHeaders;
}

//Write to Parquet
void WriteToParquet(List<OcsfRoot> list, string date, int count)
{
    var destinationFolder = Path.Combine(settings.DestinationFolder, "parquet", date);
    if (!Directory.Exists(destinationFolder))
    {
        Directory.CreateDirectory(destinationFolder);
    }

    var destinationFileName = Path.Combine(destinationFolder, count.ToString() + ".parquet");

    ParquetSerializer.SerializeAsync(list, destinationFileName).Wait();
}

//Write to JSON
void WriteToJson(List<OcsfRoot> list, string date, int count)
{
    var destinationFolder = Path.Combine(settings.DestinationFolder, "json", date);
    if(!Directory.Exists(destinationFolder))
    {
        Directory.CreateDirectory(destinationFolder);
    }

    var destinationFileName = Path.Combine(destinationFolder, count.ToString() + ".json");

    File.WriteAllText(destinationFileName, JsonSerializer.Serialize(list, OcsfJsonSerializerContext.Default.ListOcsfRoot));
}

// Process all existing files
void OnCreated(object sender, FileSystemEventArgs e)
{
    Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

    var list = new List<OcsfRoot>();

    var csv = File.ReadAllText(e.FullPath);
    foreach (var line in CsvReader.ReadFromText(csv, csvOptions))
    {
        //var ocsf = AuditLogMapper.Map(line);   
        var ocsf = ActiveDirectoryLogMapper.Map(line);
        if (ocsf != null)
        {
            list.Add(ocsf);
            Console.WriteLine(JsonSerializer.Serialize(ocsf, OcsfJsonSerializerContext.Default.OcsfRoot));
        }
    }

    // Write to Parquet
    using var fileStream = File.OpenWrite(e.FullPath.Replace(".csv", ".parquet"));
    var task = ParquetSerializer.SerializeAsync(list, fileStream);
    task.Wait();
}

Console.ReadLine();