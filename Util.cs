namespace Oop;

public class Utils
{
    public static bool CompareStrings(string a, string b)
    {
        return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }

    public static void PrintTodo(Todo todo)
    {
        string completed = todo.Completed ? "Yes" : "No";
        Console.WriteLine($"Todo #{todo.Id}:");
        Console.WriteLine($"  Title: {todo.Title}");
        Console.WriteLine($"  Completed: {completed}");
    }
}
