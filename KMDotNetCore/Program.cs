// See https://aka.ms/new-console-template for more information

using KMDotNetCore.ConsoleApp.AdoDotNetExamples;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Run("read");
adoDotNetExample.Run("create", "title 1", "author 1", "content 1");

Console.ReadLine();