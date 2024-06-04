using System;
using System.Collections.Generic;

class Program
{
    static List<List<string>> tasks = new List<List<string>>();

    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\t\t--Welcome to Simple Task Application--\n");
            Console.WriteLine("\tSelect Task from the Options");
            Console.WriteLine("Task List Application");
            Console.WriteLine("Enter 1.- Create a task");
            Console.WriteLine("Enter 2.- Read tasks");
            Console.WriteLine("ENter 3.- Update a task");
            Console.WriteLine("Enter 4.- Delete a task");
            Console.WriteLine("Enter 5.- Exit");
            Console.Write("Enter your choice: ");

            int choice= Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateTask();
                    break;
                case 2:
                    ReadTasks();
                    break;
                case 3:
                    UpdateTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }

    static void CreateTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        tasks.Add(new List<string> { title, description });
        Console.WriteLine("Task created successfully!! :)\n");
    }

    static void ReadTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available. :(\n");
        }
        else
        {
            Console.WriteLine("----------Tasks in the List:----------");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Title: {tasks[i][0]}, Description: {tasks[i][1]}");
            }
            Console.WriteLine("--------------------------------------\n");
        }
    }

    static void UpdateTask()
    {
        Console.Write("Enter index of the task to update: ");
        int index;
        if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        Console.Write("Enter new title for the task (leave empty to keep current): ");
        string newTitle = Console.ReadLine();
        Console.Write("Enter new description for the task (leave empty to keep current): ");
        string newDescription = Console.ReadLine();

        if (!string.IsNullOrEmpty(newTitle))
        {
            tasks[index][0] = newTitle;
        }
        if (!string.IsNullOrEmpty(newDescription))
        {
            tasks[index][1] = newDescription;
        }

        Console.WriteLine("Task updated successfully.\n");
    }

    static void DeleteTask()
    {
        Console.Write("Enter index of the task to delete: ");
        int index;
        if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        tasks.RemoveAt(index);
        Console.WriteLine("Task deleted successfully.\n");
    }
}
