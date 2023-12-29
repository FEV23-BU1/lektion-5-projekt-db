namespace Oop;

public class Todo
{
    private static int IdCounter = 0;

    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }

    public Todo(string title, bool completed)
    {
        this.Id = IdCounter++;
        this.Title = title;
        this.Completed = completed;
    }
}
