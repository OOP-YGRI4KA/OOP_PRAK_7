using System;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

Product product = new Product
{
    Id = 2,
    Name = "телефон",
    Price = 70000,
    Quantity = 3
};

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.Cyrillic)
};

string json = JsonSerializer.Serialize(product, options);

File.WriteAllText("product.json", json);

Console.WriteLine("JSON товару:");
Console.WriteLine(json);

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}