package com.hyt.oocourse.todolist;

import java.util.UUID;

public interface ITodoListDao {
    TodoItem getTodoItem(UUID any);
}
