using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoPd19.Models;

namespace TodoPd19.Services
{
    public class DbService : IDbService
    {
        bool initialized;
        readonly SQLiteAsyncConnection database;

        public DbService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (!initialized)
                {
                    database.CreateTableAsync<TodoItem>().Wait();
                    initialized = true;
                }
            }
            catch (Exception)
            {
                initialized = false;
            }
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            // SQLite does not have a separate Boolean storage class. 
            // Instead, Boolean values are stored as integers 0 (false) and 1 (true).
        }

        public Task<List<TodoItem>> GetItemsDoneAsync()
        {
            // SQL
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 1");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            // not used?
            return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item) 
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(TodoItem item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
