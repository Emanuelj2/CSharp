using System.Data;

namespace BudgetTracker.Models
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Color { get; private set; } //for ui display


        private Category() { }

        public static Category Create(string name, string color = "#000000")
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Color = color
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category name cannot be empty", nameof(newName));

            Name = newName;
        }
    }
}
