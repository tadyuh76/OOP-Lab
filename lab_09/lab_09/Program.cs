using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_09
{
    // Custom exceptions
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }

    public class NotReadyToHarvestException : Exception
    {
        public NotReadyToHarvestException(string message) : base(message) { }
    }

    // Abstract base class for all products
    public abstract class Product
    {
        public int Cost { get; protected set; }
        public int Value { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public int Fertilizer { get; protected set; }
        public int Water { get; protected set; }
        public bool IsPlanted { get; protected set; }
        public string Name { get; protected set; } = null!;
        public string Emoji { get; protected set; } = null!;

        public virtual void Seed()
        {
            StartTime = DateTime.Now;
            IsPlanted = true;
            Console.WriteLine($"{Name} has been planted!");
        }

        public virtual int Harvest()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException($"Cannot harvest {Name} that hasn't been planted yet");
            }

            if (DateTime.Now - StartTime < Duration)
            {
                throw new NotReadyToHarvestException($"{Name} is not ready for harvesting yet!");
            }

            IsPlanted = false;
            return Value;
        }

        public bool IsReadyToHarvest()
        {
            return IsPlanted && (DateTime.Now - StartTime >= Duration);
        }
    }

    // Derived product classes
    public class Wheat : Product
    {
        public int NumFertilizer { get; private set; }
        public int NumWater { get; private set; }
        public int MaxFertilizer { get; private set; }
        public int MaxWater { get; private set; }

        public Wheat()
        {
            Name = "Wheat";
            Emoji = "🌾";
            Cost = 10;
            Value = 25;
            Duration = TimeSpan.FromSeconds(10); // For demonstration, use 10 seconds
            Fertilizer = 5;
            Water = 3;
            MaxFertilizer = 3;
            MaxWater = 5;
            NumFertilizer = 0;
            NumWater = 0;
        }

        public void Feed()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot fertilize wheat that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot fertilize wheat that is ready to harvest");
            }

            if (NumFertilizer >= MaxFertilizer)
            {
                throw new InvalidOperationException("Maximum fertilizer already applied");
            }

            NumFertilizer++;
            Value += 8; // Increase value with each fertilizer application
            Console.WriteLine($"Wheat {Emoji} fertilized ({NumFertilizer}/{MaxFertilizer})");
        }

        public void ProvWater()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot water wheat that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot water wheat that is ready to harvest");
            }

            if (NumWater >= MaxWater)
            {
                throw new InvalidOperationException("Maximum water already applied");
            }

            NumWater++;
            Value += 5; // Increase value with each watering
            Console.WriteLine($"Wheat {Emoji} watered ({NumWater}/{MaxWater})");
        }
    }

    public class Tomato : Product
    {
        public int NumFertilizer { get; private set; }
        public int NumWater { get; private set; }
        public int MaxFertilizer { get; private set; }
        public int MaxWater { get; private set; }

        public Tomato()
        {
            Name = "Tomato";
            Emoji = "🍅";
            Cost = 20;
            Value = 45;
            Duration = TimeSpan.FromSeconds(15); // For demonstration, use 15 seconds
            Fertilizer = 8;
            Water = 6;
            MaxFertilizer = 4;
            MaxWater = 6;
            NumFertilizer = 0;
            NumWater = 0;
        }

        public void Feed()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot fertilize tomato that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot fertilize tomato that is ready to harvest");
            }

            if (NumFertilizer >= MaxFertilizer)
            {
                throw new InvalidOperationException("Maximum fertilizer already applied");
            }

            NumFertilizer++;
            Value += 10; // Increase value with each fertilizer application
            Console.WriteLine($"Tomato {Emoji} fertilized ({NumFertilizer}/{MaxFertilizer})");
        }

        public void ProvWater()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot water tomato that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot water tomato that is ready to harvest");
            }

            if (NumWater >= MaxWater)
            {
                throw new InvalidOperationException("Maximum water already applied");
            }

            NumWater++;
            Value += 8; // Increase value with each watering
            Console.WriteLine($"Tomato {Emoji} watered ({NumWater}/{MaxWater})");
        }
    }

    public class Sunflower : Product
    {
        public int NumFertilizer { get; private set; }
        public int NumWater { get; private set; }
        public int MaxFertilizer { get; private set; }
        public int MaxWater { get; private set; }

        public Sunflower()
        {
            Name = "Sunflower";
            Emoji = "🌻";
            Cost = 30;
            Value = 70;
            Duration = TimeSpan.FromSeconds(20); // For demonstration, use 20 seconds
            Fertilizer = 10;
            Water = 8;
            MaxFertilizer = 5;
            MaxWater = 7;
            NumFertilizer = 0;
            NumWater = 0;
        }

        public void Feed()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot fertilize sunflower that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot fertilize sunflower that is ready to harvest");
            }

            if (NumFertilizer >= MaxFertilizer)
            {
                throw new InvalidOperationException("Maximum fertilizer already applied");
            }

            NumFertilizer++;
            Value += 13; // Increase value with each fertilizer application
            Console.WriteLine($"Sunflower {Emoji} fertilized ({NumFertilizer}/{MaxFertilizer})");
        }

        public void ProvWater()
        {
            if (!IsPlanted)
            {
                throw new InvalidOperationException("Cannot water sunflower that hasn't been planted yet");
            }

            if (IsReadyToHarvest())
            {
                throw new InvalidOperationException("Cannot water sunflower that is ready to harvest");
            }

            if (NumWater >= MaxWater)
            {
                throw new InvalidOperationException("Maximum water already applied");
            }

            NumWater++;
            Value += 9; // Increase value with each watering
            Console.WriteLine($"Sunflower {Emoji} watered ({NumWater}/{MaxWater})");
        }
    }

    public class Player
    {
        public string UserName { get; private set; }
        public int Reward { get; private set; }
        public List<Product> Crops { get; private set; }

        public Player(string userName, int initialReward)
        {
            UserName = userName;
            Reward = initialReward;
            Crops = new List<Product>();
        }

        public void BuyAndPlant(Product product)
        {
            if (Reward < product.Cost)
            {
                throw new InsufficientFundsException($"Not enough funds to buy {product.GetType().Name}");
            }

            Reward -= product.Cost;
            product.Seed();
            Crops.Add(product);
        }

        public void HarvestCrop(int index)
        {
            if (index < 0 || index >= Crops.Count)
            {
                throw new IndexOutOfRangeException("Invalid crop index");
            }

            try
            {
                int value = Crops[index].Harvest();
                Reward += value;
                Console.WriteLine($"Harvested {Crops[index].GetType().Name} for {value} points!");
                Crops.RemoveAt(index);
            }
            catch (NotReadyToHarvestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FertilizeCrop(int index)
        {
            if (index < 0 || index >= Crops.Count)
            {
                throw new IndexOutOfRangeException("Invalid crop index");
            }

            // First, check if the crop is ready to harvest
            if (Crops[index].IsReadyToHarvest())
            {
                throw new InvalidOperationException($"Cannot fertilize {Crops[index].Name} that is ready to harvest");
            }

            if (Crops[index] is Wheat wheat)
            {
                if (Reward < wheat.Fertilizer)
                {
                    throw new InsufficientFundsException("Not enough funds to fertilize wheat");
                }

                // Only deduct points and call Feed() if all checks pass
                Reward -= wheat.Fertilizer;
                wheat.Feed();
            }
            else if (Crops[index] is Tomato tomato)
            {
                if (Reward < tomato.Fertilizer)
                {
                    throw new InsufficientFundsException("Not enough funds to fertilize tomato");
                }

                // Fix the order: deduct points before calling Feed()
                Reward -= tomato.Fertilizer;
                tomato.Feed();
            }
            else if (Crops[index] is Sunflower sunflower)
            {
                if (Reward < sunflower.Fertilizer)
                {
                    throw new InsufficientFundsException("Not enough funds to fertilize sunflower");
                }

                Reward -= sunflower.Fertilizer;
                sunflower.Feed();
            }
        }

        public void WaterCrop(int index)
        {
            if (index < 0 || index >= Crops.Count)
            {
                throw new IndexOutOfRangeException("Invalid crop index");
            }

            // First, check if the crop is ready to harvest
            if (Crops[index].IsReadyToHarvest())
            {
                throw new InvalidOperationException($"Cannot water {Crops[index].Name} that is ready to harvest");
            }

            if (Crops[index] is Wheat wheat)
            {
                if (Reward < wheat.Water)
                {
                    throw new InsufficientFundsException("Not enough funds to water wheat");
                }

                Reward -= wheat.Water;
                wheat.ProvWater();
            }
            else if (Crops[index] is Tomato tomato)
            {
                if (Reward < tomato.Water)
                {
                    throw new InsufficientFundsException("Not enough funds to water tomato");
                }

                Reward -= tomato.Water;
                tomato.ProvWater();
            }
            else if (Crops[index] is Sunflower sunflower)
            {
                if (Reward < sunflower.Water)
                {
                    throw new InsufficientFundsException("Not enough funds to water sunflower");
                }

                Reward -= sunflower.Water;
                sunflower.ProvWater();
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\n--- {UserName}'s Farm Status ---");
            Console.WriteLine($"Rewards: {Reward} points");
            Console.WriteLine("Crops:");

            if (Crops.Count == 0)
            {
                Console.WriteLine("No crops planted yet.");
            }
            else
            {
                for (int i = 0; i < Crops.Count; i++)
                {
                    Product crop = Crops[i];
                    TimeSpan timeGrown = DateTime.Now - crop.StartTime;
                    double progress = Math.Min(100, (timeGrown.TotalSeconds / crop.Duration.TotalSeconds) * 100);

                    Console.WriteLine($"{i}. {crop.Emoji} {crop.GetType().Name} - Growth: {progress:F1}% " +
                                     (progress >= 100 ? "(Ready to harvest! 🌟)" : $"({crop.Duration.TotalSeconds - timeGrown.TotalSeconds:F1} seconds left)"));
                }
            }
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Ensure proper emoji display
            Console.WriteLine("Welcome to HarvestFarm! 🚜🌱");
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine() ?? "Player";

            Player player = new Player(playerName, 100);
            bool running = true;

            while (running)
            {
                player.DisplayStatus();
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Buy and plant crops");
                Console.WriteLine("2. Water crops");
                Console.WriteLine("3. Fertilize crops");
                Console.WriteLine("4. Harvest crops");
                Console.WriteLine("5. Exit game");
                Console.Write("Your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        BuyAndPlantMenu(player);
                        break;
                    case 2:
                        WaterMenu(player);
                        break;
                    case 3:
                        FertilizeMenu(player);
                        break;
                    case 4:
                        HarvestMenu(player);
                        break;
                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

            Console.WriteLine($"Thank you for playing, {player.UserName}!");
            Console.WriteLine($"Final score: {player.Reward} points");
        }

        static void BuyAndPlantMenu(Player player)
        {
            Console.WriteLine("\n--- Buy and Plant Menu ---");
            Console.WriteLine("Available crops:");
            Console.WriteLine("1. 🌾 Wheat - Cost: 10, Growth time: 10 seconds");
            Console.WriteLine("2. 🍅 Tomato - Cost: 20, Growth time: 15 seconds");
            Console.WriteLine("3. 🌻 Sunflower - Cost: 30, Growth time: 20 seconds");
            Console.WriteLine("4. Return to main menu");
            Console.Write("Your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid selection. Returning to main menu.");
                return;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        player.BuyAndPlant(new Wheat());
                        break;
                    case 2:
                        player.BuyAndPlant(new Tomato());
                        break;
                    case 3:
                        player.BuyAndPlant(new Sunflower());
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Returning to main menu.");
                        break;
                }
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void WaterMenu(Player player)
        {
            if (player.Crops.Count == 0)
            {
                Console.WriteLine("You don't have any crops to water.");
                return;
            }

            Console.WriteLine("\n--- Water Crops Menu ---");
            player.DisplayStatus();

            Console.Write("Enter the index of the crop to water (or -1 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index == -1)
            {
                return;
            }

            try
            {
                player.WaterCrop(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void FertilizeMenu(Player player)
        {
            if (player.Crops.Count == 0)
            {
                Console.WriteLine("You don't have any crops to fertilize.");
                return;
            }

            Console.WriteLine("\n--- Fertilize Crops Menu ---");
            player.DisplayStatus();

            Console.Write("Enter the index of the crop to fertilize (or -1 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index == -1)
            {
                return;
            }

            try
            {
                player.FertilizeCrop(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void HarvestMenu(Player player)
        {
            if (player.Crops.Count == 0)
            {
                Console.WriteLine("You don't have any crops to harvest.");
                return;
            }

            Console.WriteLine("\n--- Harvest Crops Menu ---");
            player.DisplayStatus();

            Console.Write("Enter the index of the crop to harvest (or -1 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index == -1)
            {
                return;
            }

            try
            {
                player.HarvestCrop(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
