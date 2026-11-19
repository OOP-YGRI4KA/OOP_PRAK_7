using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

Product product = new Product
{
    Id = 1,
    Name = "Навушники",
    Price = 1599,
    Quantity = 10,
    InternalCode = "SECRET-123"
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

Console.WriteLine($"\nВнутрішній код товару: {product.InternalCode}");

Console.WriteLine("\nФайл створено:");
Console.WriteLine(Path.GetFullPath("product.json"));

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    [JsonIgnore]
    public string InternalCode { get; set; } = "";
}