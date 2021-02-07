namespace alternatrr.Services
{
    public class SceneMappingService
    {

        public string CleanParseTitle(string title)
        {
            var result = title.Trim();

            result = result.Replace("Ä", "AE");
            result = result.Replace("Ü", "UE");
            result = result.Replace("Ö", "OE");

            result = result.Replace("ä", "ae");
            result = result.Replace("ü", "ue");
            result = result.Replace("ö", "oe");

            result = result.Replace(" ", "");

            result = result.ToLower();

            return result;
        }

        public string CleanSearchTitle(string title)
        {
            var result = title.Trim();

            result = result.Replace("Ä", "AE");
            result = result.Replace("Ü", "UE");
            result = result.Replace("Ö", "OE");

            result = result.Replace("ä", "ae");
            result = result.Replace("ü", "ue");
            result = result.Replace("ö", "oe");

            return result;
        }

    }
}