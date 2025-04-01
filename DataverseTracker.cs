﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private static AvailabilityTelemetry CheckDataverseAvailable(string connectionString)
        {
            AvailabilityTelemetry availabilityTelemetry = new AvailabilityTelemetry();
            availabilityTelemetry.RunLocation = "GitHub";
            availabilityTelemetry.Name = "WhoAmI";

            using (ServiceClient serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    // Create the WhoAmI request
                    WhoAmIRequest whoAmIRequest = new WhoAmIRequest();

                    // Execute the request
                    WhoAmIResponse whoAmIResponse = (WhoAmIResponse)serviceClient.Execute(whoAmIRequest);
                    availabilityTelemetry.Success = serviceClient.IsReady;
                    availabilityTelemetry.Message = System.String.Format("Connected to {0}", serviceClient.ConnectedOrgFriendlyName);

                    // Output the results
                    Console.WriteLine("Connected to {0} {1}", serviceClient.ConnectedOrgFriendlyName, serviceClient.IsReady);
                }
                else
                {
                    Console.WriteLine("Failed to connect to Dataverse.{0}", serviceClient.LastError);
                    availabilityTelemetry.Success = false;
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
