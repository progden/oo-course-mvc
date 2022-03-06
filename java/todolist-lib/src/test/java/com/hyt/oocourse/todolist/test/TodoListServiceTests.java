package com.hyt.oocourse.todolist.test;

import com.hyt.oocourse.todolist.ITodoListDao;
import com.hyt.oocourse.todolist.TodoItem;
import com.hyt.oocourse.todolist.TodoListService;
import lombok.val;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.internal.matchers.Any;

import java.util.UUID;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

public class TodoListServiceTests {
    private TodoListService todoListService;
    private TodoItem todoItem;
    private TodoItem emptyItem;
    private TodoItem nullItem;
    private TodoItem todoItemSub;
    private ITodoListDao todoListDao;
    private TodoItem storedItem;

    @BeforeEach
    public void setup() {
        todoListDao = Mockito.mock(ITodoListDao.class);

        todoListService = new TodoListService(todoListDao);
        todoItem = new TodoItem("Buy milk");
        emptyItem = new TodoItem("");
        nullItem = new TodoItem(null);
        todoItemSub = new TodoItem("Check validate date");
        storedItem = new TodoItem("Buy milk");
    }

    @Test
    public void AddTodoItem_should_HasAId() {
        val item = todoListService.addTodoItem(todoItem);
        Assertions.assertNotNull(item.getId());
    }

    @Test()
    void AddTodoItem_should_descriptionNotEmpty() {
        assertThrows(IllegalArgumentException.class, () -> todoListService.addTodoItem(null));
        assertThrows(IllegalArgumentException.class, () -> todoListService.addTodoItem(emptyItem));
        assertThrows(IllegalArgumentException.class, () -> todoListService.addTodoItem(nullItem));

        todoListService.addTodoItem(todoItem);
        // success
    }

    @Test
    void addSubItem_when_parentNotExists_should_throwException() {
        // not exists
        assertThrows(IllegalArgumentException.class, () -> todoListService.addSubItem(todoItem, todoItemSub));

        // exists
        storedItem.setId(UUID.randomUUID());
        when(todoListDao.getTodoItem(any(UUID.class))).thenReturn(storedItem);
        todoListService.addSubItem(storedItem, todoItemSub);
        verify(todoListDao, times(1)).getTodoItem(any(UUID.class));
    }

    @Test
    void addSubItem_should_hasId() {
        storedItem.setId(UUID.randomUUID());
        when(todoListDao.getTodoItem(any(UUID.class))).thenReturn(storedItem);
        todoListService.addSubItem(storedItem, todoItemSub);
        verify(todoListDao, times(1)).getTodoItem(any(UUID.class));
    }

}
