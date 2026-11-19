using System;

class InvalidGradeException : Exception
{
    public InvalidGradeException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введіть оцінку студента (0-100): ");

            int grade = int.Parse(Console.ReadLine());

            if (grade < 0 || grade > 100)
            {
                throw new InvalidGradeException(
                    "Оцінка повинна бути від 0 до 100!"
                );
            }

            Console.WriteLine($"Оцінка студента: {grade}");
        }
        catch (InvalidGradeException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка: потрібно ввести число!");
        }
    }
}