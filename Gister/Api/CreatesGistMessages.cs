namespace EchelonTouchInc.Gister.Api
{
    public class CreatesGistMessages
    {
        public string CreateMessage(string filename, string content)
        {
            var realContent = new CleansGistContent().Clean(content);
            
            return @"{
  ""public"": true,
  ""files"": {
    """ + filename + @""": {
      ""content"": """ + realContent + @"""
    }
  }
}";
        }
    }
}