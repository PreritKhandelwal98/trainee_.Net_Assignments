using System;
using System.Collections.Generic;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Item(int id, string name, decimal price, int quantity)
    {
        ID = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public List<Item> GetAllItems()
    {
        return items;
    }

    public Item FindItemById(int id)
    {
        return items.Find(item => item.ID == id);
    }

    public void UpdateItem(Item item)
    {
        var index = items.FindIndex(i => i.ID == item.ID);
        if (index != -1)
        {
            items[index] = item;
        }
    }

    public bool DeleteItem(int id)
    {
        var item = FindItemById(id);
        if (item != null)
        {
            items.Remove(item);
            return true;
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        bool exit = false;

        while (!exit)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine("\t\t--Welcome to Inventory Management System--\n");
            Console.WriteLine("\tEnter 1. to Add a new item");
            Console.WriteLine("\tEnter 2. to Display all items");
            Console.WriteLine("\tEnter 3. to Find an item by ID");
            Console.WriteLine("\tEnter 4. to Update an item");
            Console.WriteLine("\tEnter 5. to Delete an item");
            Console.WriteLine("\tEnter 6. to Exit\n");
            Console.Write("Enter your choice: ");
            Console.ResetColor();

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a number.\n");
                Console.ResetColor();
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddItem(inventory);
                    break;
                case 2:
                    DisplayAllItems(inventory);
                    break;
                case 3:
                    FindItemById(inventory);
                    break;
                case 4:
                    UpdateItem(inventory);
                    break;
                case 5:
                    DeleteItem(inventory);
                    break;
                case 6:
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }
        }
    }

    static void AddItem(Inventory inventory)
    {
        Console.Write("Enter item ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter item name: ");
        string name = Console.ReadLine();

        Console.Write("Enter item price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter item quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        inventory.AddItem(new Item(id, name, price, quantity));
        Console.WriteLine("Item added successfully!\n");
    }

    static void DisplayAllItems(Inventory inventory)
    {
        List<Item> items = inventory.GetAllItems();
        if (items.Count == 0)
        {
            Console.WriteLine("No items in inventory.\n");
        }
        else
        {
            Console.WriteLine("----------Inventory Items:----------");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------\n");
        }
    }

    static void FindItemById(Inventory inventory)
    {
        Console.Write("Enter item ID to find: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Item item = inventory.FindItemById(id);

        if (item == null)
        {
            Console.WriteLine("Item not found.\n");
        }
        else
        {
            Console.WriteLine("Item found: \n" + item + "\n");
        }
    }

    static void UpdateItem(Inventory inventory)
    {
        Console.Write("Enter item ID to update: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Item item = inventory.FindItemById(id);
        if (item == null)
        {
            Console.WriteLine("Item not found.\n");
            return;
        }

        Console.Write("Enter new name (leave empty to keep current): ");
        string newName = Console.ReadLine();
        Console.Write("Enter new price (leave empty to keep current): ");
        string newPriceStr = Console.ReadLine();
        Console.Write("Enter new quantity (leave empty to keep current): ");
        string newQuantityStr = Console.ReadLine();

        if (!string.IsNullOrEmpty(newName))
        {
            item.Name = newName;
        }
        if (decimal.TryParse(newPriceStr, out decimal newPrice))
        {
            item.Price = newPrice;
        }
        if (int.TryParse(newQuantityStr, out int newQuantity))
        {
            item.Quantity = newQuantity;
        }

        inventory.UpdateItem(item);
        Console.WriteLine("Item updated successfully!\n");
    }

    static void DeleteItem(Inventory inventory)
    {
        Console.Write("Enter item ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        if (inventory.DeleteItem(id))
        {
            Console.WriteLine("Item deleted successfully!\n");
        }
        else
        {
            Console.WriteLine("Item not found.\n");
        }
    }
}


