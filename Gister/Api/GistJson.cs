namespace EchelonTouchInc.Gister.Api
{
    public class GistJson
    {
        public string CreateFrom(string filename, string content)
        {
            var realContent = new GistCleaner().Clean(content);
            
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