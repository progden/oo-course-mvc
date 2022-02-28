namespace TodoListMVC.Lib;

public class TodoListService 
{
    private ITodoListDao todoListDao { set; get; }

    public TodoListService(ITodoListDao todoListDao)
    {
        this.todoListDao = todoListDao;
    }

    public TodoItem AddItem(TodoItem todo)
    {
        if (todo == null)
        {
            throw new Exception("TodoItem cannot be null");
        }
        if (string.IsNullOrEmpty(todo.Description))
        {
            throw new Exception("TodoItem description cannot be empty");
        }
        
        // 實作邏輯
        todo.Id = new Guid();
        todoListDao.AddItem(todo);
        // 回傳? 

        return todo;
    }

    public TodoItem AddSubTodoItem(Guid parentId, TodoItem todoItem)
    {
        // 驗證
        var parentItem = todoListDao.GetItemById(parentId);
        if(parentItem == null)
        {
            throw new Exception("Parent item not found");
        }
        
        if (todoItem == null)
        {
            throw new Exception("TodoItem cannot be null");
        }
        if (string.IsNullOrEmpty(todoItem.Description))
        {
            throw new Exception("TodoItem description cannot be empty");
        }
        
        // 實作邏輯
        todoItem.ParentId = parentId;
        todoListDao.AddItem(todoItem);
        
        return todoItem;
    }

    public TodoItem MarkItemDone(Guid id)
    {
        // 驗證
        var item = todoListDao.GetItemById(id);
        if(item == null)
        {
            throw new Exception("Item not found");
        }

        // 業務邏輯
        item.IsDone = true;
        todoListDao.MarkItemDone(id);

        return item;
    }
}

public interface ITodoListDao
{
    public int AddItem(TodoItem item);
    public TodoItem GetItemById(Guid id);
    public void MarkItemDone(Guid id);
}

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