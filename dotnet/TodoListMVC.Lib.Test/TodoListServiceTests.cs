using System;
using NSubstitute;
using NUnit.Framework;

namespace TodoListMVC.Lib.Test;

public class TodoListServiceTests
{
    private ITodoListDao? _todoListDao;
    private TodoListService _todoListService;
    private Guid _parentId;
    private TodoItem _parentItem;

    [SetUp]
    public void SetUp()
    {
        _todoListDao = NSubstitute.Substitute.For<ITodoListDao>();
        _todoListService = new TodoListService(_todoListDao);
        
        _parentId = Guid.NewGuid();
        _parentItem = new TodoItem("parent")
        {
            Id = _parentId
        };
    }

    [Test]
    public void AddItem_Should_DescriptionNotNullOrEmpty()
    {
        Assert.Throws<Exception>(() =>
        {
            _todoListService.AddItem(new TodoItem(""));
        });
        Assert.Throws<Exception>(() =>
        {
            _todoListService.AddItem(new TodoItem(null));
        });
        
        Assert.Throws<Exception>(() =>
        {
            _todoListService.AddItem(null);
        });
        
        // 怎麼驗證? 
        // 讀資料庫 => 與資料庫耦合
        // 透過 Exception => ??
        // 透過回傳值 => ??
        _todoListService.AddItem(new TodoItem("test"));
        Assert.Pass();
    }

    [Test]
    public void AddItem_Should_HasId()
    {
        var todoItem = _todoListService.AddItem(new TodoItem("test"));
        Assert.NotNull(todoItem.Id);
    }

    [Test]
    public void AddSubItem_When_NoParent_Should_ThrowException()
    {
        Assert.Throws<Exception>(() =>
        {
            _todoListDao.GetItemById(Arg.Any<Guid>()).Returns(default(TodoItem));
            
            _todoListService.AddSubTodoItem(Guid.NewGuid(), new TodoItem("test"));
        });
    }

    [Test]
    public void AddSubItem_Should_DescriptionNotNullOrEmpty()
    {
        _todoListDao.GetItemById(Arg.Any<Guid>()).Returns(_parentItem);
        
        Assert.Throws<Exception>(() =>
        {
            _todoListService.AddSubTodoItem(_parentId, new TodoItem(null));
        });
        Assert.Throws<Exception>(() =>
        {
            _todoListService.AddSubTodoItem(_parentId, new TodoItem(""));
        });
        
        _todoListService.AddSubTodoItem(_parentId, new TodoItem("test"));
        Assert.Pass();
    }

    [Test]
    public void AddSubItem_Should_HasParentId()
    {
        _todoListDao.GetItemById(Arg.Any<Guid>()).Returns(_parentItem);
        var todoItem = new TodoItem("test");
        _todoListService.AddSubTodoItem(_parentId, todoItem);

        Assert.AreEqual(_parentId, todoItem.ParentId);
    }
    [Test]
    public void AddSubItem_Should_SaveToDb()
    {
        _todoListDao.GetItemById(Arg.Any<Guid>()).Returns(_parentItem);
        var todoItem = new TodoItem("test");
        
        _todoListService.AddSubTodoItem(_parentId, todoItem);
        _todoListDao.Received().AddItem(Arg.Any<TodoItem>());
    }

    [Test]
    public void MarkItemDone_Should_()
    {
        // id not exists
        Assert.Throws<Exception>(() =>
        {
            _todoListDao.GetItemById(Guid.NewGuid()).Returns(default(TodoItem));
            _todoListService.MarkItemDone(Guid.NewGuid());
        });

        var newGuid = Guid.NewGuid();
        _todoListDao.GetItemById(newGuid).Returns(new TodoItem("test"){ Id = newGuid});
        var todoItem = _todoListService.MarkItemDone(newGuid);
        
        _todoListDao.Received().MarkItemDone(newGuid);
        Assert.AreEqual(true, todoItem.IsDone);
    }
}