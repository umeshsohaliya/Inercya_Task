// See https://aka.ms/new-console-template for more information
using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;

Console.WriteLine("Hello, World!");

//Read Config
var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json");

var configuration = builder.Build();


#region Csv to DataTable
DataTable dtCustomer;
using (StreamReader sr = new StreamReader(configuration["filepath"]))
{
    string csvData = sr.ReadToEnd().ToString();
    dtCustomer = CsvHelper.CsvToDatatable(csvData);
}
#endregion

//Push Datatable to DB using SqlBulkCopy
var sbCopy = new SqlBulkCopy(configuration["Connectionstring"]);
sbCopy.BatchSize = 10000;
sbCopy.DestinationTableName = configuration["tablename"];
sbCopy.WriteToServer(dtCustomer);

Console.WriteLine("Done with loading data on server.");
Console.ReadLine();