package com.hyt.oocourse.todolist;

import lombok.*;

import java.util.UUID;

@Data
public class TodoItem {
    private String description;
    private UUID Id;

    public TodoItem(String description) {
        this.description = description;
    }
}
