using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

List<Student> students = new List<Student>()
{
    new Student { Id = 1, Name = "Ілля", AverageGrade = 91.5 },
    new Student { Id = 2, Name = "Артем", AverageGrade = 87.0 },
    new Student { Id = 3, Name = "Олександр", AverageGrade = 96.2 },
    new Student { Id = 4, Name = "Данило", AverageGrade = 83.7 },
    new Student { Id = 5, Name = "Ярослав", AverageGrade = 94.8 }
};

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.Cyrillic)
};

string json = JsonSerializer.Serialize(students, options);

string filePath = Path.Combine(
    Directory.GetCurrentDirectory(), "students.json");

File.WriteAllText(filePath, json);

Console.WriteLine("Список студентів збережено!");
Console.WriteLine($"Шлях до файлу: {filePath}");

string fileContent = File.ReadAllText(filePath);

List<Student> restored =
    JsonSerializer.Deserialize<List<Student>>(fileContent)
    ?? new List<Student>();

Console.WriteLine("\nСписок студентів:");

foreach (Student student in restored)
{
    Console.WriteLine(
        $"ID: {student.Id}, Ім'я: {student.Name}, Середній бал: {student.AverageGrade}");
}

Student? best = restored
    .OrderByDescending(s => s.AverageGrade)
    .FirstOrDefault();

if (best is not null)
{
    Console.WriteLine("\nСтудент із найвищим середнім балом:");

    Console.WriteLine($"Ім'я: {best.Name}");
    Console.WriteLine($"Середній бал: {best.AverageGrade}");
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double AverageGrade { get; set; }
}