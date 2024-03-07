// See https://aka.ms/new-console-template for more information
using DotNetTrainingConsoleApp.AdoDotNetExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
AdoDotNetExample ado = new AdoDotNetExample();

ado.Read();

// ado.Edit(3);
// ado.Edit(30);

// ado.Create("test title", "test autor", "test content");

//ado.Update(6, "title6", "author6", "content6");
//ado.Update(60, "title6", "author6", "content6");

//ado.Delete(7);
//ado.Delete(70);

Console.ReadKey();