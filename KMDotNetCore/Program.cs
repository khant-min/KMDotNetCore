// See https://aka.ms/new-console-template for more information

using KMDotNetCore.ConsoleApp.AdoDotNetExamples;
using KMDotNetCore.ConsoleApp.DapperExamples;
using KMDotNetCore.ConsoleApp.EFCoreExamples;
using KMDotNetCore.ConsoleApp.HttpClientExamples;

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Run();
//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();
//EFCoreExample efCore = new EFCoreExample();
//efCore.Run();
HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.Run();

Console.ReadLine();