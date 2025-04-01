using Track_Availablity;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Dataverse Track Availability");

        string dataverseConnectionString = Environment.GetEnvironmentVariable("DATAVERSE_CONNECTION_STRING");
        string applicationInsightsConnectionString = Environment.GetEnvironmentVariable("APPLICATION_INSIGHTS_CONNECTION_STRING");

        if (string.IsNullOrEmpty(dataverseConnectionString) || string.IsNullOrEmpty(applicationInsightsConnectionString))
        {
            Console.WriteLine("Environment variables DATAVERSE_CONNECTION_STRING and APPLICATION_INSIGHTS_CONNECTION_STRING must be set.");
            return;
        }

        DataverseTracker tracker = new DataverseTracker();
        tracker.TrackDataverseAvailability(dataverseConnectionString, applicationInsightsConnectionString);
    }

}
