using MeetUpTESH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpTESH.Repositories
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllItemsAsync();
        Task<TodoItem> GetItemAsync(int id);
        Task<int> SaveItemAsync(TodoItem item);
        Task<int> DeleteItemAsync(TodoItem item);
    }
}
