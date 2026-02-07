

// database connection
var connectionString = "Data Source=localhost;Initial Catalog=FoodDB;Integrated Security=True;";


public class Food
{
    public int FoodId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

}

public class FoodRepository
{
    public int SaveFood(Food food)
    {
        int result = SaveFoodInDatabase(food); //this is a placeholder for actual database save logic
        return result;
    }

    public Food GetFood(int foodId)
    {
        Food result = new Food();
        result = GetFoodFromDatabase(foodId); //this is a placeholder for actual database retrieval logic
        return result;
    }
}

