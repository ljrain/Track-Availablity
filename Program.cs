using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.UserSecrets;

using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Track_Availablity;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Dataverse Track Availability");

        if(args.Length < 2)
        {
            Console.WriteLine("Usage: DataverseTrackAvailability <DataverseConnectionString> <ApplicationInsightsConnectionString>");
            return;
        }

        string dataverseConnectionString = args[0];
        string applicationInsightsConnectionString = args[1];

        DataverseTracker tracker = new DataverseTracker();
        tracker.TrackDataverseAvailability(dataverseConnectionString, applicationInsightsConnectionString);
    }

}
