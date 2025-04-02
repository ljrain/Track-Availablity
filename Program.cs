using Track_Availablity;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Dataverse Track Availability");

        string dataverseConnectionString = args[0].ToString();
        string applicationInsightsConnectionString = args[1].ToString();

        if (string.IsNullOrEmpty(dataverseConnectionString) || string.IsNullOrEmpty(applicationInsightsConnectionString))
        {
            Console.WriteLine("Environment variables DATAVERSE_CONNECTION_STRING and APPLICATION_INSIGHTS_CONNECTION_STRING must be set.");
            return;
        }

        DataverseTracker tracker = new DataverseTracker();
        tracker.TrackDataverseAvailability(dataverseConnectionString, applicationInsightsConnectionString);
    }

}
