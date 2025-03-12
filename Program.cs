using System;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the first name:");
        string name1 = Console.ReadLine();
        Console.WriteLine("Enter the second name:");
        string name2 = Console.ReadLine();
        Console.WriteLine("Enter the third name:");
        string name3 = Console.ReadLine();

        int age1 = await GetAge(name1);
        int age2 = await GetAge(name2);
        int age3 = await GetAge(name3);

        Console.WriteLine($"Name 1: {name1} is apx {age1} years old");
        Console.WriteLine($"Name 2: {name2} is apx {age2} years old");
        Console.WriteLine($"Name 3: {name3} is apx {age3} years old");

        string oldest = GetOldest(name1, age1, name2, age2, name3, age3);
        Console.WriteLine($"{oldest} is the oldest.");
    }

    static async Task<int> GetAge(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://api.agify.io?name={name}";
            var response = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<AgeResponse>(response);
            return result.Age ?? 0;
        }
    }

    static string GetOldest(string name1, int age1, string name2, int age2, string name3, int age3)
    {
        if (age1 >= age2 && age1 >= age3) return name1;
        if (age2 >= age1 && age2 >= age3) return name2;
        return name3;
    }
}

class AgeResponse
{
    public int? Age { get; set; }
}
