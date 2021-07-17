using System;
using BenchmarkDotNet.Running;
using Benchmarks.HtmlParsers.Benchmarks;
using Benchmarks.HtmlParsers.Helpers;

namespace BenchmarkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AHrefBenchmark>();
        }
    }
}
