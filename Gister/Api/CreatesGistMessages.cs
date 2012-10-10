namespace EchelonTouchInc.Gister.Api
{
    public class CreatesGistMessages
    {
        public string CreateMessage(string filename, string content, string description,bool isPublic)
        {
            var realContent = new CleansGistContent().Clean(content);
            if (string.IsNullOrEmpty(description))
            {
                description = "the description for this gist";
            }

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
                + "\"public\": "+isPublic.ToString()+","
                + "\"files\": {"
                + "\""+filename+"\": {"
                + "\"content\": \""+realContent+"\""
                + "}}}";
            return retValue;

            //string temp = "{"
            //    + "\"description\": \"{0}\","
            //    + "\"public\": true,"
            //    + "\"files\": {"
            //    + "\"{1}\": {"
            //    + "\"content\": \"{2}\""
            //    + "}}}";
            //string retValue = string.Format(temp,description, filename, realContent);
            

            return retValue;


        }
    }
}