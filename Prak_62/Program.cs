﻿using System;

class InsufficientStockException : Exception
{
    public InsufficientStockException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        int stock = 20;

        try
        {
            Console.WriteLine($"Кількість товарів на складі: {stock}");

            Console.Write("Введіть кількість товарів для продажу: ");

            int quantity = int.Parse(Console.ReadLine());

            if (quantity < 0)
            {
                Console.WriteLine("Кількість товарів не може бути від'ємною!");
                return;
            }

            if (quantity > stock)
            {
                throw new InsufficientStockException(
                    "Недостатньо товарів на складі!"
                );
            }

            stock -= quantity;

            Console.WriteLine("Продаж виконано успішно!");
            Console.WriteLine($"Залишок товарів: {stock}");
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка: потрібно ввести число!");
        }
    }
}