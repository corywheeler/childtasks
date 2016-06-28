using System.Diagnostics;
using VersionOne.SDK.APIClient;

namespace ChildTasks
{
	class Program
	{
		static void Main(string[] args)
		{
			Debug.Print("--- Start ---");

			V1Connector connector = V1Connector
				.WithInstanceUrl("http://localhost/VersionOne.Web")
				.WithUserAgentHeader("ChildTasks", "1.0")
				.WithUsernameAndPassword("admin", "admin")
				.Build();

			IServices services = new Services(connector);
			var v1ProjectName = "Batting Order";
			var passThroughQuery = "{" +
			                       "  \"from\": \"Defect\"," +
			                       "  \"select\": [\"Name\",\"Description\",\"Resolution\",\"ResolutionReason.Name\",\"Children:Task.Name\", \"Children:Task.Description\"]," +
								   "  \"where\": {\"Scope.Name\" : " + v1ProjectName + "} " +
			                       "}";

			var result = services.ExecutePassThroughQuery(passThroughQuery);

			Debug.Print("Here it is:" + result);
			
			
			Debug.Print("--- End ---");
		}
	}
}
