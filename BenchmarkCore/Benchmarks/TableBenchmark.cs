using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using Benchmarks.HtmlParsers.Benchmarks.Base;
using CsQuery;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace Benchmarks.HtmlParsers.Benchmarks
{
    [SimpleJob]
    public class TableBenchmark : HtmlBenchmarkBase
    {
        protected override string ResourcePath
        {
            get { return "BenchmarkCore.Examples.02.Table.html"; }
        }

        #region Benchmarks

        /// <summary>
        /// Extract proxies using HtmlAgilityPack
        /// </summary>
        [Benchmark]
        public List<Proxy> HtmlAgilityPackScrape()
        {
            var proxies = new List<Proxy>();

            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);

            foreach (HtmlNode row in htmlSnippet.DocumentNode.SelectNodes("//table/tr").Skip(1))
            {
                HtmlNodeCollection cells = row.SelectNodes("td");

                var proxy = new Proxy();

                var cellsText = cells.Select(x => x.InnerText).ToArray();

                proxy.IP = cellsText.ElementAt(0);
                proxy.Port = cellsText.ElementAt(1);
                proxy.Country = cellsText.ElementAt(2);

                proxies.Add(proxy);
            }

            return proxies;
        }

        /// <summary>
        /// Extract proxies using Fizzler
        /// </summary>
        [Benchmark]
        public List<Proxy> FizzlerScrape()
        {
            var proxies = new List<Proxy>();

            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);

            foreach (HtmlNode row in htmlSnippet.DocumentNode.QuerySelector("table").QuerySelectorAll("tr").Skip(1))
            { 
                HtmlNodeCollection cells = row.SelectNodes("td");

                var proxy = new Proxy();

                var cellsText = cells.Select(x => x.InnerText).ToArray();

                proxy.IP = cellsText.ElementAt(0);
                proxy.Port = cellsText.ElementAt(1);
                proxy.Protocol = cellsText.ElementAt(2);
                proxy.Country = cellsText.ElementAt(3);

                proxies.Add(proxy);
            }

            return proxies;
        }

        /// <summary>
        /// EExtract proxies using CsQuery
        /// </summary>
        [Benchmark]
        public List<Proxy> CsQueryScrape()
        {
            var proxies = new List<Proxy>();

            CQ doc = CQ.Create(Html);
            var currencyTable = doc[".proxies-table"];

            foreach (var row in currencyTable.Find("tr").Skip(1))
            { 
                var cellsText = row.Cq().Find("td").Select(td => td.Cq().Text()).ToArray();

                if (cellsText.Any())
                {
                    var proxy = new Proxy();

                    proxy.IP = cellsText.ElementAt(0);
                    proxy.Port = cellsText.ElementAt(1);
                    proxy.Protocol = cellsText.ElementAt(2);
                    proxy.Country = cellsText.ElementAt(3);

                    proxies.Add(proxy);
                }
            }

            return proxies;
        }

        /// <summary>
        /// Extract proxies using AngleSharp
        /// </summary>
        [Benchmark]
        public List<Proxy> AngleSharpScrape()
        {
            var proxies = new List<Proxy>();

            var parser = new HtmlParser();
            var document = parser.ParseDocument(Html);
            var currencyTable = document.QuerySelector("table") as IHtmlTableElement;

            foreach (var row in currencyTable.Rows.Skip(1))
            {

                var proxy = new Proxy();
                var cellsText = row.Cells.Select(x => x.TextContent).ToArray();

                proxy.IP = cellsText.ElementAt(0);
                proxy.Port = cellsText.ElementAt(1);
                proxy.Protocol = cellsText.ElementAt(2);
                proxy.Country = cellsText.ElementAt(3);

                proxies.Add(proxy);
            }

            return proxies;
        }

        /// <summary>
        /// Extract proxies using RegExp
        /// </summary>
        [Benchmark]
        public List<Proxy> RegExpScrape()
        {
            var proxies = new List<Proxy>();

            Regex rowRegex = new Regex("<tr[^>]*?>(?<rowContent>((?!</tr>).)*)</tr>",
                RegexOptions.Compiled | RegexOptions.Singleline);
            Regex cellRegex = new Regex("<td[^>]*?>(?<cell>((?!</td>).)*)</td>",
                RegexOptions.Compiled | RegexOptions.Singleline);

            foreach (var row in rowRegex.Matches(Html).Cast<Match>().Select(match => new
            {
                InnerHtml = WebUtility.HtmlDecode(match.Groups["rowContent"].ToString()),
                Content = WebUtility.HtmlDecode(match.ToString())
            }).Skip(1))
            {
                var cells = cellRegex.Matches(row.InnerHtml)
                    .Cast<Match>()
                    .Select(match => WebUtility.HtmlDecode(match.Groups["cell"].ToString()))
                    .ToList();

                if (cells.Any(cell => !string.IsNullOrWhiteSpace(cell)) && cells.Count > 2)
                {
                    var proxy = new Proxy();

                    proxy.IP = cells.ElementAt(0);
                    proxy.Port = cells.ElementAt(1);
                    proxy.Protocol = cells.ElementAt(2);
                    proxy.Country = cells.ElementAt(3);

                    proxies.Add(proxy);
                }
            }

            return proxies;
        }

        #endregion

        #region Models

        public class Proxy
        {
            public string IP { get; set; }
            public string Port { get; set; }
            public string Protocol { get; set; }
            public string Country { get; set; }
        }

        #endregion
    }
}