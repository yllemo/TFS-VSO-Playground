-- Change this to the name of your collection DB. You’ll need to run these queries for each of your collection DBs. 
USE Tfs_DefaultCollection 
GO 

-- Recent Users 
select count(distinct IdentityName) as [Recent Users] from tbl_Command with (nolock) 

-- Users with Assigned Work Items 
select count(distinct [System.AssignedTo]) AS [Users with Assigned Work Items] from WorkItemsAreUsed with (nolock) 

-- Version Control Users 
select COUNT(*) AS [Version Control Users] from [Tfs_Configuration].[dbo].tbl_security_identity_cache as ic JOIN tbl_Identity as i ON i.TeamFoundationId=ic.tf_id where ic.is_group = 0 

-- Total Work Items 
select count(*) AS [Total Work Items] from WorkItemsAreUsed with (nolock) 

-- Areas and Iterations 
select count(*) AS [Areas and Iterations] from tbl_nodes with (nolock) 

-- Work Item Versions 
select count(*) AS [Work Item Versions] from (select [System.Id] from WorkItemsAreUsed with (nolock) union all select [System.Id] from WorkItemsWereUsed with (nolock)) x 
-- Work Item Attachments 
select count(*) AS [Work Item Attachments] from WorkItemFiles with (nolock) where FldID = 50 
-- Work Item Queries 
select count(*) AS [Work Item Queries] from QueryItems with (nolock) 

-- Files 
select count(*) as [Files] from tbl_VersionedItem vi with (nolock) join tbl_Version v with (nolock) on v.ItemId = vi.ItemId where VersionTo = 2147483647 

-- Compressed File Sizes 
select (sum(convert(bigint,OffsetTo - OffsetFrom + 1)) / (1024 * 1024)) AS [Compressed File Sizes] from tbl_Content with (nolock) 

-- Uncompressed File Sizes 
select (sum(FileLength) / (1024 * 1024)) AS [Uncompressed File Sizes] from tbl_File with (nolock) 

-- Checkins 
select max(ChangeSetId) AS [Checkins] from tbl_ChangeSet with (nolock) 

-- Shelvesets 
select COUNT(*) AS [Shelvesets] from tbl_Workspace with (nolock) where type='1' 

-- Merge History 
select SUM(st.row_count) AS [Merge History] from sys.dm_db_partition_stats st WHERE object_name(object_id) = 'tbl_MergeHistory' AND (index_id < 2) 

-- Pending Changes 
select count(*) AS [Pending Changes] from tbl_PendingChange pc with (nolock) join tbl_Workspace w with (nolock) on pc.WorkspaceId = w.WorkspaceId where w.Type = 0 
-- Workspaces 
select COUNT(*) AS [Workspaces] from tbl_Workspace with (nolock) where type='0' 
-- Local Copies 
select SUM(st.row_count) AS [Local Copies] from sys.dm_db_partition_stats st WHERE object_name(object_id) = 'tbl_LocalVersion' AND (index_id < 2) 

-- Command Counts 
select Command, count(*) as [Execution Count] from tbl_Command with (nolock) WHERE Command IN ('QueryWorkitems', 'Update', 'GetWorkItem', 'Get', 'VCDownloadHandler', 'Checkin', 'Upload', 'Shelve') GROUP BY Command, Application ORDER BY [Application],[Command] 
