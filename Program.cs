using System.Text.Json;
using System.Text.Json.Serialization;
using Goods;

const string FilePath = "products.json";

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    Converters = { new DateOnlyJsonConverter() }
};

List<Item> items;

if (File.Exists(FilePath))
{
    string existingJson = File.ReadAllText(FilePath);

    items = JsonSerializer.Deserialize<List<Item>>(existingJson, jsonOptions) ?? new List<Item>();

    Console.WriteLine($"Загружено товаров из файла: {items.Count}");
}
else
{
    Console.WriteLine($"Файл {FilePath} не найден. Создаю тестовую базу товаров.");

    items = new List<Item>
    {
        new Product("Молоко", 89.99, new DateOnly(2026, 4, 20), 10),
        new Product("Хлеб", 45.50, new DateOnly(2026, 5, 1), 5),

        new Batch("Йогурт", 720.00, 12, new DateOnly(2026, 4, 15), 14),
        new Batch("Печенье", 360.00, 6, new DateOnly(2026, 3, 1), 90),

        new Set("Завтрак", 250.00, new List<Item>
        {
            new Product("Кофе", 150.00, new DateOnly(2026, 2, 1), 180),
            new Product("Круассан", 100.00, new DateOnly(2026, 5, 4), 3)
        })
    };

    string initialJson = JsonSerializer.Serialize(items, jsonOptions);
    File.WriteAllText(FilePath, initialJson);

    Console.WriteLine($"Файл {FilePath} создан.");
}

Console.WriteLine();
Console.WriteLine("=== Все товары ===");

foreach (Item item in items)
{
    Console.WriteLine(item);
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine($"=== Просроченные товары на {DateOnly.FromDateTime(DateTime.Now):dd.MM.yyyy} ===");

var expiredItems = items.Where(item => item.Expired()).ToList();

if (expiredItems.Count == 0)
{
    Console.WriteLine("Просроченных товаров нет.");
}
else
{
    foreach (Item item in expiredItems)
    {
        Console.WriteLine(item);
        Console.WriteLine();
    }
}

Console.WriteLine();
Console.WriteLine("=== Сортировка по цене ===");

items.Sort();

foreach (Item item in items)
{
    Console.WriteLine(item);
    Console.WriteLine();
}

string finalJson = JsonSerializer.Serialize(items, jsonOptions);
File.WriteAllText(FilePath, finalJson);

Console.WriteLine($"Данные сохранены в файл {FilePath}.");

file sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}