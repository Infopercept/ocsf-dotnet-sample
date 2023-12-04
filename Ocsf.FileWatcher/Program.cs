// See https://aka.ms/new-console-template for more information
using Csv;
using Ocsf.Azure.Mapper;
using Ocsf.Schema;
using Parquet.Serialization;
using System.Text.Json;

Console.WriteLine("OCSF File Watcher.");

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

string directoryToWatch = @"sample"; // Replace with your directory

using (FileSystemWatcher watcher = new())
{
    watcher.Path = directoryToWatch;

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