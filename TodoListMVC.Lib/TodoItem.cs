namespace TodoListMVC.Lib;

public class TodoItem
{
    public TodoItem(string desc)
    {
        this.Description = desc;
    }

    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    internal string Description { get; set; }
    public bool IsDone { get; set; }
}