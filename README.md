# Benchmart for HTML parsing libraries ib C#

Benchmark for my article [How to parse HTML in .NET](https://scrapingant.com/blog/parse-html-dot-net)

To measure parser performance I used the [BenchmarkDotNet](https://github.com/PerfDotNet/BenchmarkDotNet) library from DreamWalker.

The measurements were made on an Intel® Core(TM) i9-9880H CPU @ 2.30GHz.

# Results

## URL extraction from page links

|          Method |      Mean |     Error |     Median |
|---------------- |----------:|----------:|-----------:|
| [HtmlAgilityPack](https://html-agility-pack.net/) |  3.653 ms |  0.087 ms |   3.579 ms |
|      [AngleSharp](https://anglesharp.github.io/) |  5.864 ms |  0.091 ms |   5.853 ms |
|         [CsQuery](https://github.com/jamietre/CsQuery) | 14.269 ms |  0.284 ms |  13.931 ms |
|         [Fizzler](https://github.com/atifaziz/Fizzler) |  4.147 ms |  0.081 ms |   4.105 ms |
|           [Regex](https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex(v=vs.110).aspx) |  0.547 ms |  0.010 ms |  0.543.0 ms |


## Data extraction from HTML table

|          Method |     Mean |     Error |   Median |
|---------------- |---------:|----------:|---------:|
| [HtmlAgilityPack](https://html-agility-pack.net/) | 3.323 ms | 0.0947 ms | 3.317 ms |
|      [AngleSharp](https://anglesharp.github.io/) | 3.920 ms | 0.0557 ms | 3.929 ms |
|         [CsQuery](https://github.com/jamietre/CsQuery) | 8.475 ms | 0.2227 ms | 8.400 ms |
|         [Fizzler](https://github.com/atifaziz/Fizzler) | 3.217 ms | 0.0637 ms | 3.205 ms |
|           [Regex](https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex(v=vs.110).aspx) | 9.636 ms | 0.1904 ms | 9.456 ms |
