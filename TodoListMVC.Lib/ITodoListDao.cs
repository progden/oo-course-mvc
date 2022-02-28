namespace TodoListMVC.Lib;

public interface ITodoListDao
{
    public int AddItem(TodoItem item);
    public TodoItem GetItemById(Guid id);
    public void MarkItemDone(Guid id);
}