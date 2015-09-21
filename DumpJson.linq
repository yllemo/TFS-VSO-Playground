<Query Kind="Expression" />

/*

Add this section to My Extensions to get the .DumpJson option on all Dumps

*/
	  public static object DumpJson(this object value, string description, int depth)

       {

              return GetJsonDumpTarget(value).Dump(description, depth);

       }     

      

       public static object DumpJson(this object value, string description, bool toDataGrid)

       {

              return GetJsonDumpTarget(value).Dump(description, toDataGrid);

       }     

      

       private static object GetJsonDumpTarget(object value)

       {

              object dumpTarget = value;

              //if this is a string that contains a JSON object, do a round-trip serialization to format it:

              var stringValue = value as string;

              if (stringValue != null)

              {

                     if (stringValue.Trim().StartsWith("{"))

                     {

                           var obj = JsonConvert.DeserializeObject(stringValue);

                           dumpTarget = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);

                     }

                     else

                     {

                           dumpTarget = stringValue;

                     }

              }

              else

              {

                     dumpTarget = JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);

              }

              return dumpTarget;

       }
