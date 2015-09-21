<Query Kind="Program">
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Build.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Build.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Build.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Build.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.VersionControl.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.VersionControl.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.VersionControl.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.VersionControl.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Common.dll</Reference>
  <Namespace>Microsoft.TeamFoundation.Build.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.Framework.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.VersionControl.Client</Namespace>
</Query>

void Main()
{
	var tfs = TfsTeamProjectCollectionFactory.
	    GetTeamProjectCollection(new Uri("http://win-f0mra7cfnp6:8080/tfs/"));
	 
	tfs.EnsureAuthenticated();
	 
	var versionControl = tfs.GetService<VersionControlServer>();
	 
	versionControl.QueryRootBranchObjects(RecursionType.Full)
	    .Where(b => !b.Properties.RootItem.IsDeleted)
	    .Select(s => new
	    {
	        Project = s.Properties.RootItem.Item
	            .Substring(0, s.Properties.RootItem.Item.IndexOf('/', 2)),
	        Properties = s.Properties,
	        DateCreated = s.DateCreated,
	        ChildBranches = s.ChildBranches
	    })
	    .Select(s => new
	    {
	        s.Project,
	        Branch = s.Properties.RootItem.Item.Replace(s.Project, ""),
	        Parent = s.Properties.ParentBranch != null ?
	            s.Properties.ParentBranch.Item.Replace(s.Project, "") : "",
	        Version = (s.Properties.RootItem.Version as ChangesetVersionSpec)
	            .ChangesetId,
	        DateCreated = s.DateCreated,
	        Owner = s.Properties.Owner,
	        ChildBranches = s.ChildBranches
	            .Where (cb => !cb.IsDeleted)
	            .Select(cb => new
	            {
	                Branch = cb.Item.Replace(s.Project, ""),
	                Version = (cb.Version as ChangesetVersionSpec).ChangesetId
	            })
	    })
	    .OrderBy(s => s.Project).ThenByDescending(s => s.Version)
	    .Dump();
	
}

// Define other methods and classes here
