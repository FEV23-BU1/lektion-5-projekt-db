namespace Oop;

public class HelpCommand : Command
{
    public override void execute(string[] args)
    {
        Console.WriteLine("help - Displays help information about the commands.");
        Console.WriteLine("list - List all todo-items in storage.");
        Console.WriteLine("add <title> - Add a new todo-item to storage.");
        Console.WriteLine("remove <id> - Remove a todo-item from storage.");
        Console.WriteLine("complete <id> - Mark a todo-item as completed.");
        Console.WriteLine("uncomplete <id> Unmark a todo-item as completed.");
        Console.WriteLine("exit - Exit the program.");
    }
}

public class ListCommand : Command
{
    private TodoService todoService;

    public ListCommand(TodoService todoService)
    {
        this.todoService = todoService;
    }

    public override void execute(string[] args)
    {
        foreach (Todo todo in todoService.GetAll())
        {
            Utils.PrintTodo(todo);
        }
    }
}

public class AddCommand : Command
{
    private TodoService todoService;

    public AddCommand(TodoService todoService)
    {
        this.todoService = todoService;
    }

    public override void execute(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine("Usage: add <title>");
            return;
        }

        string title = "";
        for (int i = 1; i < args.Length; i++)
        {
            title += args[i] + " ";
        }

        title = title.Substring(0, title.Length - 1);

        Todo todo = new Todo(title, false);
        todoService.Add(todo);

        Console.WriteLine("Created todo.");
        Utils.PrintTodo(todo);
    }
}

public class RemoveCommand : Command
{
    private TodoService todoService;

    public RemoveCommand(TodoService todoService)
    {
        this.todoService = todoService;
    }

    public override void execute(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: remove <id>");
            return;
        }

        int id = -1;
        try
        {
            id = Int32.Parse(args[1]);
        }
        catch (FormatException)
        {
            Console.WriteLine("<id> must be a valid integer.");
            return;
        }

        Todo? todo = todoService.Remove(id);
        if (todo == null)
        {
            Console.WriteLine($"A todo-item with id {id} was not found.");
            return;
        }

        Console.WriteLine($"Todo-item with id {id} and title {todo.Title} was removed.");
    }
}

public class UpdateCommand : Command
{
    private TodoService todoService;

    public UpdateCommand(TodoService todoService)
    {
        this.todoService = todoService;
    }

    public override void execute(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: update <id> true|false");
            return;
        }

        int id = -1;
        try
        {
            id = Int32.Parse(args[1]);
        }
        catch (FormatException)
        {
            Console.WriteLine("<id> must be a valid integer.");
            return;
        }

        bool completed = Utils.CompareStrings(args[2], "true");

        Todo? todo = todoService.Update(id, completed);
        if (todo == null)
        {
            Console.WriteLine($"A todo-item with id {id} was not found.");
            return;
        }

        Console.WriteLine(
            $"Todo-item with id {id} and title {todo.Title} was marked as {todo.Completed}."
        );
    }
}
