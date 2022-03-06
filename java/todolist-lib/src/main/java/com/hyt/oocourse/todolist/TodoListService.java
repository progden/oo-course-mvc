package com.hyt.oocourse.todolist;

import java.util.UUID;

public class TodoListService {
    private final ITodoListDao todoListDao;

    public TodoListService(ITodoListDao todoListDao) {
        this.todoListDao = todoListDao;
    }

    public TodoItem addTodoItem(TodoItem todoItem) {
        if(todoItem == null || todoItem.getDescription() == null || todoItem.getDescription().length() == 0) {
            throw new IllegalArgumentException("Invalid todo item");
        }
        todoItem.setId(UUID.randomUUID());
        return todoItem;
    }

    public TodoItem addSubItem(TodoItem parent, TodoItem todoItem) {
        if(parent == null || parent.getId() == null) {
            throw new IllegalArgumentException("Invalid parent");
        }
        TodoItem item = todoListDao.getTodoItem(parent.getId());
        return null;
    }
}
