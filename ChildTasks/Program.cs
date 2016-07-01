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
			var v1Sprint = "Iteration 1";
//			var passThroughQuery = @"{ 
//""from"": ""Defect"",
//""select"": [""Name"",""Description"",""Resolution"",""ResolutionReason.Name"",""Children:Task.Name"", ""Children:Task.Description""],
//""where"": {
//	""Children:Task.Name"" : ""Release Notes"",
//	""Scope.Name"" : " + v1ProjectName + @"}
//}";

			var passThroughQuery = @"
from: Defect
select: 
  - Name
  - Number
  - Description
  - Resolution
  - ResolutionReason.Name
  - from: Children:Task
    select:
      - Name
      - Description
    where: 
      Name: Release Notes
where:
  Scope.Name: " + v1ProjectName + @"
  Timebox.Name: " + v1Sprint;


//var passThroughQuery = @"
//from: Defect
//select: 
//  - Name
//  - Number
//  - Description
//  - Resolution
//  - ResolutionReason.Name
//  - from: Children:Task
//    select:
//      - Name
//      - Description
//    filter:
//      - Name!='Release Notes'
//where:
//  Scope.Name: " + v1ProjectName + @"
//  Timebox.Name: " + v1Sprint;

//			var passThroughQuery = @"
//from: Defect
//select: 
//  - Name
//  - Number
//  - Description
//  - Resolution
//  - ResolutionReason.Name
//where:
//  Scope.Name: " + v1ProjectName + @"
//  Timebox.Name: " + v1Sprint + @"
//filter:
//  - Number!='D-01001'
//";


			var result = services.ExecutePassThroughQuery(passThroughQuery);

			Debug.Print("Here it is:" + result);
			
			
			Debug.Print("--- End ---");
		}
	}
}
