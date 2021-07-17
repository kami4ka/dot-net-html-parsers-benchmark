using Benchmarks.HtmlParsers.Helpers;

namespace Benchmarks.HtmlParsers.Benchmarks.Base
{
    public abstract class HtmlBenchmarkBase
    {
        public HtmlBenchmarkBase()
        {
            Html = Loader.LoadResourceAsText(ResourcePath);
        }

        protected abstract string ResourcePath { get; }

        protected virtual string Html { get; private set; }
    }
}