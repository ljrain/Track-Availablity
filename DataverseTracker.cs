using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Diagnostics;

namespace Track_Availablity
{
    internal class DataverseTracker
    {
        internal bool TrackDataverseAvailability(string connectionString, string aiConnectionString)
        {
            bool bResults = false;
            // Check if the Dataverse service is available
            AvailabilityTelemetry availabilityTelemetry = CheckDataverseAvailable(connectionString);
            // Submit the results to Application Insights
            bResults = SubmitToAppInsights(aiConnectionString, availabilityTelemetry);
            return (bResults);
        }

        /// <summary>
        /// This method checks if the Dataverse service is available using a provided connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static AvailabilityTelemetry CheckDataverseAvailable(string connectionString, string runLocation = "GitHub", string name = "WhoAmI")
        {
            AvailabilityTelemetry availabilityTelemetry = new AvailabilityTelemetry();
            availabilityTelemetry.RunLocation = runLocation;
            availabilityTelemetry.Name = name;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (ServiceClient serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    // Create the WhoAmI request
                    WhoAmIRequest whoAmIRequest = new WhoAmIRequest();

                    // Execute the request
                    WhoAmIResponse whoAmIResponse = (WhoAmIResponse)serviceClient.Execute(whoAmIRequest);
                    stopwatch.Stop();

                    availabilityTelemetry.Success = serviceClient.IsReady;
                    availabilityTelemetry.Message = System.String.Format("Connected to {0}", serviceClient.ConnectedOrgFriendlyName);
                    availabilityTelemetry.Duration = stopwatch.Elapsed;

                    // Output the results
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Connected to ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(serviceClient.ConnectedOrgFriendlyName);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" {serviceClient.IsReady}");
                    Console.ResetColor();
                }
                else
                {
                    stopwatch.Stop();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Failed to connect to Dataverse. ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(serviceClient.LastError);
                    Console.ResetColor();
                    availabilityTelemetry.Success = false;
                    availabilityTelemetry.Message = System.String.Format("Failed to connect to Dataverse.{0}", serviceClient.LastError);
                    availabilityTelemetry.Duration = stopwatch.Elapsed;
                }
            }
            return (availabilityTelemetry);
        }

        /// <summary>
        /// This method submits the AvailabilityTelemetry data to Application Insights.
        /// </summary>
        /// <param name="aiConnectionString"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool SubmitToAppInsights(string aiConnectionString, AvailabilityTelemetry data)
        {
            bool bResults = false;

            TelemetryConfiguration config = new TelemetryConfiguration();
            config.ConnectionString = aiConnectionString;

            var telemetryClient = new TelemetryClient(config);
            telemetryClient.TrackAvailability(data);
            telemetryClient.Flush();

            return (bResults);
        }
    }
}
