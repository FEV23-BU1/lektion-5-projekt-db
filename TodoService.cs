namespace Oop;

using MongoDB.Bson;
using MongoDB.Driver;

public abstract class TodoService
{
    public abstract void Add(Todo todo);
    public abstract Todo? Remove(int id);
    public abstract Todo? Update(int id, bool completed);
    public abstract List<Todo> GetAll();
}

public class LocalTodoService : TodoService
{
    private List<Todo> todos = new List<Todo>();

    public override void Add(Todo todo)
    {
        todos.Add(todo);
    }

    public override List<Todo> GetAll()
    {
        return todos;
    }

    public override Todo? Remove(int id)
    {
        Todo? todo = todos.Find(all => all.Id == id);
        if (todo == null)
        {
            return null;
        }

        todos.Remove(todo);
        return todo;
    }

    public override Todo? Update(int id, bool completed)
    {
        Todo? todo = todos.Find(all => all.Id == id);
        if (todo == null)
        {
            return null;
        }

        todo.Completed = completed;
        return todo;
    }
}

public class DatabaseTodoService : TodoService
{
    private MongoClient mongoClient;
    private IMongoDatabase database;
    private IMongoCollection<Todo> collection;

    public DatabaseTodoService()
    {
        this.mongoClient = new MongoClient("mongodb://localhost:27017/backendutveckling1");
        this.database = this.mongoClient.GetDatabase("backendutveckling1");
        this.collection = this.database.GetCollection<Todo>("todos");
    }

    public override void Add(Todo todo)
    {
        this.collection.InsertOne(todo);
    }

    public override List<Todo> GetAll()
    {
        var filter = Builders<Todo>.Filter.Empty;
        return this.collection.Find(filter).ToList();
    }

    public override Todo? Remove(int id)
    {
        var filter = Builders<Todo>.Filter.Eq(todo => todo.Id, id);
        Todo todo = this.collection.Find(filter).First();
        if (todo == null)
        {
            return null;
        }

        this.collection.DeleteOne(filter);
        return todo;
    }

    public override Todo? Update(int id, bool completed)
    {
        var filter = Builders<Todo>.Filter.Eq(todo => todo.Id, id);
        Todo todo = this.collection.Find(filter).First();
        if (todo == null)
        {
            return null;
        }

        todo.Completed = completed;
        var update = Builders<Todo>.Update.Set(todo => todo.Completed, completed);
        this.collection.UpdateOne(filter, update);
        return todo;
    }
}
