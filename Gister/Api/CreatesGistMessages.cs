namespace EchelonTouchInc.Gister.Api
{
    public class CreatesGistMessages
    {
        public string CreateMessage(string filename, string content, string description,bool isPublic)
        {
            var realContent = new CleansGistContent().Clean(content);

            //            return @"{
            //  ""description"": @"""+description + @"""
            //  ""public"": true,
            //  ""files"": {
            //    """ + filename + @""": {
            //      ""content"": """ + realContent + @"""
            //    }
            //  }
            //}";

            return string.Format(
                "{"
                + "\"description\": {0},"
                + "\"public\": {1},"
                + "\"files\": {"
                +   "\"{2}\": {"
                +   "\"content\": \"{3}\""
                +   "}}}",
                        description, isPublic, filename, realContent);


        }
    }
}