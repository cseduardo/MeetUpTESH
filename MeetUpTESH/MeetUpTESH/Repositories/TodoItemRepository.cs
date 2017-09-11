﻿using MeetUpTESH.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpTESH.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        readonly SQLiteAsyncConnection connection;

        public TodoItemRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<TodoItem>().Wait();
        }

        public Task<List<TodoItem>> GetAllItemsAsync()
        {
            return connection.Table<TodoItem>().ToListAsync();
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return connection.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return connection.UpdateAsync(item);
            }
            else
            {
                return connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return connection.DeleteAsync(item);
        }
    }
}
