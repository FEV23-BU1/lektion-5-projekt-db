namespace Oop;

class Program
{
    private static TodoService todoService = new DatabaseTodoService();
    private static Dictionary<string, Command> commands = new Dictionary<string, Command>();

    static void Main(string[] _args)
    {
        Console.WriteLine("Starting Todo Application...");

        commands.Add("help", new HelpCommand());
        commands.Add("list", new ListCommand(todoService));
        commands.Add("add", new AddCommand(todoService));
        commands.Add("remove", new RemoveCommand(todoService));
        commands.Add("update", new UpdateCommand(todoService));

        string? line = Console.ReadLine();
        while (line != null && !Utils.CompareStrings(line, "exit"))
        {
            string[] args = line.Split(" ");
            if (args.Length == 0)
            {
                continue;
            }

            string action = args[0];
            Command command = commands[action.ToLower()];
            command.execute(args);

            Console.WriteLine("-------------------");
            line = Console.ReadLine();
        }
    }
}
