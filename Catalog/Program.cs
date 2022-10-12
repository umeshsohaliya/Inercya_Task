// See https://aka.ms/new-console-template for more information
using Catalog;
using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


Console.WriteLine("Customer Execution started");

//Read Config
var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json");
var configuration = builder.Build();

#region load Categories.csv
DataTable dtCategories = new DataTable("Categories");
using (StreamReader sr = new StreamReader(configuration["catfilepath"]))
{
    dtCategories = CsvHelper.CsvToDatatable(sr.ReadToEnd().ToString());
}
#endregion

#region load Products.csv
DataTable dtProducts = new DataTable("Products");
using (StreamReader sr = new StreamReader(configuration["prodfilepath"]))
{
    dtProducts = CsvHelper.CsvToDatatable(sr.ReadToEnd().ToString());
}
#endregion


#region Prepare Data
Categories listCategory = new Categories();
Category category = new Category();

listCategory.Category = new List<Category>();
foreach (DataRow row in dtCategories.Rows)
{
    category.Id = Convert.ToInt32(row["Id"]);
    category.Name = (string)row["Name"];
    category.Description = (string)row["Description"];

    DataRow[] products = dtProducts.Select("CategoryId = " + category.Id);
    category.Products = new List<Product>();
    foreach (DataRow rowprod in products)
    {
        Product product = new Product()
        {
            Id = Convert.ToByte(rowprod["Id"]),
            Name = (string)rowprod["Name"],
            CategoryId = category.Id,
            Price = Convert.ToDecimal(rowprod["Price"])
        };
        category.Products.Add(product);
    }
    listCategory.Category.Add(category);
}
#endregion

#region generate Catalog.xml

XmlSerializer xsSubmit = new XmlSerializer(typeof(Categories));
var subReq = listCategory;
var xml = "";

using (var sww = new StringWriter())
{
    using (XmlWriter writer = XmlWriter.Create(sww))
    {
        xsSubmit.Serialize(writer, subReq);
        xml = sww.ToString(); // Your XML
    }
}
await File.WriteAllTextAsync("Catalog.xml", xml);

#endregion


#region generate Catalog.json

string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(listCategory.Category);
await File.WriteAllTextAsync("Catalog.json", jsonResult);

#endregion


Console.Write("XML & Json generated.");
Console.ReadLine();