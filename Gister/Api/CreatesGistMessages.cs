namespace EchelonTouchInc.Gister.Api
{
    public class CreatesGistMessages
    {
        public string CreateMessage(string filename, string content, string description,bool isPublic)
        {
            var realContent = new CleansGistContent().Clean(content);

//            string retValue=@"{
//              ""description"": @""" + description + @""",
//              ""public"": true,
//              ""files"": {
//                """ + filename + @""": {
//                  ""content"": """ + realContent + @"""
//                }
//              }
//            }";
            string retValue = "{"
                + "\"description\": \""+description+"\","
                + "\"public\": "+isPublic.ToString().ToLower()+","
                + "\"files\": {"
                + "\""+filename+"\": {"
                + "\"content\": \""+realContent+"\""
                + "}}}";
            return retValue;


        }
    }
}