<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	/*		
	VSO REST API
	https://www.visualstudio.com/en-us/integrate/api/overview
	*/
	
	string sAPI = "https://yllemo.visualstudio.com/DefaultCollection/MyCoolScrumProject/_apis/build/builds";
	//string sAPI = "https://yllemo.visualstudio.com/DefaultCollection/MyCoolScrumProject/_apis/work/boards/?api-version=2.0-preview";
	//string sAPI = "https://yllemo.visualstudio.com/DefaultCollection/MyCoolScrumProject/_apis/wit/queries?$depth=1&api-version=1.0";

	string s =  GetVSO(sAPI).Result;

	
	s.DumpJson();

	
	JObject json = JObject.Parse(s);
	json.Dump();
}



public async Task<string> GetVSO(string sAPI)
{
    try
    {
        var username = "yllemo2";
        var password = Util.GetPassword("yllemo.tfs.password"); 
        string s = sAPI;		
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", username, password))));

            using (HttpResponseMessage response = client.GetAsync(s).Result)            			
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
				//responseBody.DumpJson();
				return responseBody;
            }
        }
    }
    catch (Exception ex)
    {
	   //ex.Dump();
	   return(ex.ToString());
    }	
}