using System;
using System.IO;
using System.Text.Json;

// Шлях до JSON-файлу з першого завдання
string filePath = Path.Combine("..", "Task1", "product.json");

// Перевірка існування файлу
if (!File.Exists(filePath))
{
    Console.WriteLine("Помилка! Файл product.json не знайдено.");
    return;
}

// Читання JSON із файлу
string json = File.ReadAllText(filePath);

// Десеріалізація JSON
Product? product = JsonSerializer.Deserialize<Product>(json);

// Виведення властивостей товару
if (product is not null)
{
    Console.WriteLine("Інформація про товар:");

    Console.WriteLine($"ID: {product.Id}");
    Console.WriteLine($"Назва: {product.Name}");
    Console.WriteLine($"Ціна: {product.Price}");
    Console.WriteLine($"Кількість: {product.Quantity}");
}
else
{
    Console.WriteLine("Не вдалося відновити товар.");
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}