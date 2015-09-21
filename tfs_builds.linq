<Query Kind="Statements">
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Build.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Build.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Build.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Build.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.Common.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.TeamFoundation.VersionControl.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.TeamFoundation.VersionControl.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Client.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Client.dll</Reference>
  <Reference Relative="..\..\Downloads\Microsoft.VisualStudio.Services.Common.dll">C:\Users\Henrik Yllemo\Downloads\Microsoft.VisualStudio.Services.Common.dll</Reference>
  <Namespace>Microsoft.TeamFoundation.Build.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.Framework.Client</Namespace>
  <Namespace>Microsoft.TeamFoundation.VersionControl.Client</Namespace>
</Query>

var tfs = TfsTeamProjectCollectionFactory
 .GetTeamProjectCollection(new Uri("http://win-f0mra7cfnp6:8080/tfs/"));
 
tfs.EnsureAuthenticated();
 
var buildServer = tfs.GetService<IBuildServer>();
 
var spec = buildServer.CreateBuildDetailSpec("*");
spec.MinFinishTime = DateTime.Now.Subtract(TimeSpan.FromDays(7));
spec.MaxFinishTime = DateTime.Now;
spec.QueryDeletedOption = QueryDeletedOption.IncludeDeleted;
 
var builds = buildServer.QueryBuilds(spec).Builds;
var total = builds.Sum(b => b.FinishTime.Subtract(b.StartTime).TotalMinutes);
 
builds.Select(x => new
{
 Project = x.TeamProject,
 Definition = x.BuildDefinition.Name,
 Version = x.SourceGetVersion,
 Developer = x.RequestedFor,
 Type = x.Reason,
 Start = x.StartTime,
 Duration = x.FinishTime.Subtract(x.StartTime),
 Build = x.CompilationStatus,
 Tests = x.TestStatus,
 Result = x.Status,
 StyleCopViolations = InformationNodeConverters.GetBuildWarnings(x)
  .Count(inc => inc.Message.StartsWith("SA")),
 FxCopViolations = InformationNodeConverters.GetBuildWarnings(x)
  .Count(inc => inc.Message.StartsWith("CA")),
 Warnings = InformationNodeConverters.GetBuildWarnings(x)
  .Select(inc => inc.Message)
  .Where(m => !m.StartsWith("SA") && !m.StartsWith("CA")),
 Errors = InformationNodeConverters.GetBuildErrors(x)
  .Select(inc => inc.Message)
})
.OrderByDescending(b => b.Start)
.Dump("All builds of the week.  Total build duration: " + total);