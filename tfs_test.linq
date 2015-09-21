<Query Kind="Program">
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Common.dll</Reference>
  <Namespace>Microsoft.TeamFoundation.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.Framework.Client</Namespace>
</Query>

void Main()
{
	//http://win-f0mra7cfnp6:8080/tfs/
	var tfs = TfsTeamProjectCollectionFactory
	    .GetTeamProjectCollection(new Uri("http://win-f0mra7cfnp6:8080/tfs/"));
	 
	tfs.Dump();
	
}

// Define other methods and classes here
