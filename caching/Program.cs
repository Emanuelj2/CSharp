using Microsoft.Extensions.Caching.Memory;

public record Person(string FirstName, string LastName);

//api: 
public class PersonApi
{
    public async Task<Person> GetPerson()
    {
        await Task.Delay(2000); // Simulate async operation
        Console.WriteLine("Retrieved person from service.");
        return new Person("John", "Doe");
    }
}

//service: 
public class PersonService
{
    private readonly PersonApi _personApi;
    private readonly IMemoryCache _cache;
    public PersonService(IMemoryCache cache, PersonApi personApi)
    {
        _personApi = personApi;
        _cache = cache;
    }
    public async Task<Person> GetPersonAsync()
    {
        return (await _cache.GetOrCreateAsync("person", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30); // Cache for 10 seconds
            return await _personApi.GetPerson();
        }))!;
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var cache = new MemoryCache(new MemoryCacheOptions());  
        var personService = new PersonService(cache, new PersonApi());

        Console.WriteLine("First call:");
        Console.WriteLine(await personService.GetPersonAsync());   // spy prints, ~2s wait

        Console.WriteLine("Second call:");
        Console.WriteLine(await personService.GetPersonAsync());   // instant, no spy

        Console.WriteLine("Third call:");
        Console.WriteLine(await personService.GetPersonAsync());
    }
}