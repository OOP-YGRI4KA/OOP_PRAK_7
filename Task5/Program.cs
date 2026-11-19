using System;
using System.IO;
using System.Text.Json;

string defaultPath = Path.Combine("..", "Task4", "product.json");

Console.WriteLine("Введіть шлях до JSON-файлу:");
Console.WriteLine($"Натисніть Enter для використання {defaultPath}");

string? input = Console.ReadLine();

string filePath = string.IsNullOrWhiteSpace(input)
    ? defaultPath
    : input.Trim();

try
{
    string json = File.ReadAllText(filePath);

    Product? product = JsonSerializer.Deserialize<Product>(json);

    if (product is not null)
    {
        Console.WriteLine("\nІнформація про товар:");

        Console.WriteLine($"ID: {product.Id}");
        Console.WriteLine($"Назва: {product.Name}");
        Console.WriteLine($"Ціна: {product.Price}");
        Console.WriteLine($"Кількість: {product.Quantity}");
    }
    else
    {
        Console.WriteLine("У файлі немає об'єкта товару.");
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("Помилка: файл не знайдено!");
}
catch (JsonException)
{
    Console.WriteLine("Помилка: неправильний формат JSON!");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Помилка: немає доступу до файлу!");
}
catch (IOException ex)
{
    Console.WriteLine($"Помилка читання файлу: {ex.Message}");
}
catch (ArgumentException)
{
    Console.WriteLine("Помилка: неправильний шлях до файлу!");
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}