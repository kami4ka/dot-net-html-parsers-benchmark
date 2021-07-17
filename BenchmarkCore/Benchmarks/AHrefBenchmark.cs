using System.Collections.Generic;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using Benchmarks.HtmlParsers.Benchmarks.Base;
using CsQuery;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace Benchmarks.HtmlParsers.Benchmarks
{
    [SimpleJob]
    public class AHrefBenchmark : HtmlBenchmarkBase
    {

        protected override string ResourcePath
        {
            get { return "BenchmarkCore.Examples.01.Hrefs.html"; }
        }

        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// </summary>
        [Benchmark]
        public List<string> HtmlAgilityPackScrape()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using Fizzler
        /// </summary>
        [Benchmark]
        public List<string> FizzlerScrape()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode node in htmlSnippet.DocumentNode.QuerySelectorAll("a"))
            {
                hrefTags.Add(node.GetAttributeValue("href", null));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using CsQuery
        /// </summary>
        [Benchmark]
        public List<string> CsQueryScrape()
        {
            List<string> hrefTags = new List<string>();

            CQ cq = CQ.Create(Html);
            foreach (IDomObject obj in cq.Find("a"))
            {
                hrefTags.Add(obj.GetAttribute("href"));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using AngleSharp
        /// </summary>
        [Benchmark]
        public List<string> AngleSharpScrape()
        {
            List<string> hrefTags = new List<string>();

            var parser = new HtmlParser();
            var document = parser.ParseDocument(Html);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                hrefTags.Add(element.GetAttribute("href"));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using RegExp
        /// </summary>
        [Benchmark]
        public List<string> RegExpScrape()
        {
            List<string> hrefTags = new List<string>();

            Regex reHref = new Regex(@"(?inx)
        <a \s [^>]*
            href \s* = \s*
                (?<q> ['""] )
                    (?<url> [^""]+ )
                \k<q>
        [^>]* >");
            foreach (Match match in reHref.Matches(Html))
            {
                hrefTags.Add(match.Groups["url"].ToString());
            }

            return hrefTags;
        }
    }
}