using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Benchmarks.HtmlParsers.Helpers
{
    public static class Loader
    {
        public static string LoadResourceAsText(string fullPath)
        {
            var htmlResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);
            Debug.Assert(htmlResourceStream != null, "htmlResourceStream != null");
            using (var reader = new StreamReader(htmlResourceStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
