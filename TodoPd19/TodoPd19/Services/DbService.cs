using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TodoPd19.Models;

namespace TodoPd19.Services
{
    public class DbService : IDbService
    {
        bool initialized;
        readonly SQLiteAsyncConnection database;
        public int ResumeAtTodoId { get; set; }      // hier ??? (war app.cs)

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
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item) 
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
