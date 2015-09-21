<Query Kind="Program">
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Build.Client.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Build.Client.dll</Reference>
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Build.Common.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Build.Common.dll</Reference>
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Client.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Client.dll</Reference>
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Common.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.Common.dll</Reference>
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.VersionControl.Client.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.VersionControl.Client.dll</Reference>
  <Reference Relative="..\..\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.VersionControl.Common.dll">C:\Users\henriky\Source\Workspaces\Internal\Services\Platform\Tools\TFSAnalytics\TFSAnalytics\bin\Microsoft.TeamFoundation.VersionControl.Common.dll</Reference>
  <Namespace>Microsoft.TeamFoundation.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.VersionControl.Client</Namespace>
</Query>

void Main()
{

	// var tfs = TfsTeamProjectCollectionFactory     .GetTeamProjectCollection(new Uri("http://localhost:8088/tfs"));   tfs.Dump();

/*
    var tfs = TfsTeamProjectCollectionFactory
                .GetTeamProjectCollection(new Uri("https://yllemo.visualstudio.com/DefaultCollection/MyCoolScrumProject"));
    tfs.EnsureAuthenticated();
    tfs.Dump();
	
*/
	
	
	const String CollectionAddress = "http://tfsserver:8080/tfs/MyCollection";
	using (var tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(CollectionAddress)))
	{
		tfs.EnsureAuthenticated();
		//	var server = tfs.GetService<>();
		tfs.Dump();
	}

}