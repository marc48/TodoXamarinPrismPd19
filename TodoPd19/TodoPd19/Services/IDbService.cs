using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoPd19.Models;

namespace TodoPd19.Services
{
    public interface IDbService
    {
        Task<List<TodoItem>> GetItemsAsync();
        Task<List<TodoItem>> GetItemsNotDoneAsync();
        Task<List<TodoItem>> GetItemsDoneAsync();
        Task<TodoItem> GetItemAsync(int id);
        Task<int> SaveItemAsync(TodoItem item);
        Task<int> UpdateItemAsync(TodoItem item);
        Task<int> DeleteItemAsync(TodoItem item);
    }
}
