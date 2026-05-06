using System.Text.Json;
using Goods;

const string fileName = "products.json";

var options = new JsonSerializerOptions
{
    WriteIndented = true
};

List<Item> items = new List<Item>
{
    new Product("Молоко", 90, new DateOnly(2026, 4, 20), 10),
    new Product("Хлеб", 45, new DateOnly(2026, 5, 1), 5),

    new Batch("Йогурт", 70, 12, new DateOnly(2026, 4, 15), 14),
    new Batch("Печенье", 60, 6, new DateOnly(2026, 3, 1), 90),

    new Set("Завтрак", 250, new List<Item>
    {
        new Product("Кофе", 150, new DateOnly(2026, 2, 1), 180),
        new Product("Круассан", 100, new DateOnly(2026, 5, 4), 3)
    })
};

string json = JsonSerializer.Serialize(items, options);
File.WriteAllText(fileName, json);

string jsonFromFile = File.ReadAllText(fileName);
List<Item>? goods = JsonSerializer.Deserialize<List<Item>>(jsonFromFile, options);

Console.WriteLine("Все товары:");

foreach (Item item in goods!)
{
    Console.WriteLine(item);
    Console.WriteLine();
}

Console.WriteLine("Просроченные товары:");

foreach (Item item in goods)
{
    if (item.Expired())
    {
        Console.WriteLine(item);
        Console.WriteLine();
    }
}
