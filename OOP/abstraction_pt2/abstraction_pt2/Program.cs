
//make an abstract class
abstract class Aminal
{
    public string? Name { get; set; }
    public int? Age { get; set; }

    public abstract void Speak();
}


class Dog : Aminal
{
    //default constructor
    public Dog()
    {
        Name = "Doggo";
        Age = 2;
    }
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}

public class Program
{

    public static void Main(string[] args)
    {
        Dog dog = new Dog();
        dog.Name = "Buddy";
        dog.Age = 3;

        Dog dog2 = new Dog();

        Console.WriteLine($"Dog Name: {dog.Name}\nAge: {dog.Age}");

        Console.WriteLine($"Dog2 Name: {dog2.Name}\nAge: {dog2.Age}");
        dog.Speak(); // Output: Bark
    }

}
