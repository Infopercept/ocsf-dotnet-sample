// See https://aka.ms/new-console-template for more information
using Csv;
using OcsfDemo.Schema;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("OCSF Demo!");


var csvOptions = new CsvOptions // Defaults
{
    RowsToSkip = 0, // Allows skipping of initial rows without csv data
    //SkipRow = (row, idx) => string.IsNullOrEmpty(row) || row[0] == '#',
    Separator = '\0', // Autodetects based on first row
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

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
};

string directoryToWatch = @"sample"; // Replace with your directory

using (FileSystemWatcher watcher = new ())
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

    var csv = File.ReadAllText(e.FullPath);
    foreach (var line in CsvReader.ReadFromText(csv))
    {
        // Header is handled, each line will contain the actual row data
        var dateUtc = ConvertDateTimeToLong(line["Date (UTC)"]);
        var correlationId = line["CorrelationId"];
        var service = line["Service"];
        var category = line["Category"];
        var activity = line["Activity"];
        var result = line["Result"];
        var resultReason = line["ResultReason"];
        var actorType = line["ActorType"];
        var actorName = actorType == "User" ? line["ActorUserPrincipalName"] : line["ActorDisplayName"];
        var username = line["ActorUserPrincipalName"];

        var ipAddress = line["IPAddress"];

        var ocsf = new OcsfRoot
        {
            ActivityId = 3001,
            Time = dateUtc,
            Message = correlationId,
            CategoryName = category,
            ActivityName = activity,
            Status = result,
            StatusDetail = result,
            Actor = actorType,
            Observables = service,
            User = new User
            {
                Type = actorType,
                Name = actorName,
                uid = correlationId                
            },
            
          //  UserResult = "Successfully joined device using account type: User with identifier: 02effdba-ad08-48e7-9a6f-825a44d670ab.",
          //  SourceEndpoint = "Device",
        };

        if(actorType == "User")
        {
            ocsf.User.SetUserType(UserType.User);
        }
        else
        {
            ocsf.User.SetUserType(UserType.System);
        }

        Console.WriteLine(JsonSerializer.Serialize(ocsf, jsonOptions));
    }
}

static long ConvertDateTimeToLong(string utcString)
{
    if (!DateTime.TryParse(utcString, null, DateTimeStyles.AdjustToUniversal, out DateTime dateTime))
    {
        return 0;
    }

    // Unix epoch starts on 1970-01-01T00:00:00Z
    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    TimeSpan elapsedTime = dateTime - epoch;
    return elapsedTime.Ticks / TimeSpan.TicksPerMillisecond; // Convert ticks to milliseconds
}


Console.ReadLine();