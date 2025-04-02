# Dataverse Track Availability

## Project Overview
The Dataverse Track Availability project is designed to monitor the availability of the Microsoft Dataverse service and submit telemetry data to Application Insights. This project includes a C# application that checks the service's availability and a GitHub Actions workflow to automate the process.

## Prerequisites
- .NET 8.0 SDK
- Microsoft Power Platform Dataverse Client
- Microsoft Application Insights

## Setup Instructions
1. Clone the repository:
   
2. Set up environment variables:
   - `DATAVERSE_CONNECTION_STRING`: Your Dataverse connection string.
   - `APPLICATION_INSIGHTS_CONNECTION_STRING`: Your Application Insights connection string.

3. Restore dependencies:

## Usage
To run the application locally, use the following command:

## Configuration
The application requires two connection strings:
- `DATAVERSE_CONNECTION_STRING`: Used to connect to the Dataverse service.
- `APPLICATION_INSIGHTS_CONNECTION_STRING`: Used to submit telemetry data to Application Insights.

## Running the Application
The application can be run manually or through the GitHub Actions workflow. To run it manually, use the command mentioned in the Usage section. The GitHub Actions workflow is configured to run every 30 minutes, on push to the main branch, and on manual dispatch.

## Workflow Details
The GitHub Actions workflow is defined in `.github/workflows/dataverse-track-availability.yml`. It includes the following steps:
1. Set up environment variables.
2. Check out the repository.
3. Set up .NET.
4. Restore dependencies.
5. Build the project.
6. Run the Dataverse Tracker.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## License
This project is licensed under the MIT License.