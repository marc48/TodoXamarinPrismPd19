using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TodoPd19.Models;
using TodoPd19.Services;

namespace TodoPd19.ViewModels
{
	public class TodoListPageViewModel : ViewModelBase, INavigationAware
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;

        public int ResumeAtTodoId { get; set; }      // hier ??? (war app.cs)

        public DelegateCommand ItemAddedCommand { get; set; }
        public DelegateCommand ListDoneCommand { get; set; }
        public DelegateCommand ListNotDoneCommand { get; set; }

        public DelegateCommand<TodoItem> ItemSelectedCommand => new DelegateCommand<TodoItem>(OnItemSelectedCommand);

        public TodoListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dbService = dbService;

            ItemAddedCommand = new DelegateCommand(AddNewItem);
            ListNotDoneCommand = new DelegateCommand(ListNotDoneEntries);
            ListDoneCommand = new DelegateCommand(ListDoneEntries);

        }

        private async void OnItemSelectedCommand(TodoItem item)
        {
            var p = new NavigationParameters();
            p.Add("item", item);

            await _navigationService.NavigateAsync("TodoItemPage", p);
        }

        //private async void AddFixEntry()
        //{
        //    TodoItem item = new TodoItem
        //    {
        //        ID = 0,
        //        Name = "FixEntry",
        //        Notes = "Notes: Text",
        //        Done = false
        //    };

        //    await _dbService.SaveItemAsync(item);
        //    await _dialogService.DisplayAlertAsync("AddFixEntry", "Item added...", "OK");

        //    // Refresh Obs.collection
        //    var res = await _dbService.GetItemsAsync();
        //    TodoItems = new ObservableCollection<TodoItem>(res);
        //}

        private async void ListNotDoneEntries()   
        {
            int anzahl;
            var res = await _dbService.GetItemsNotDoneAsync();
            TodoItems = new ObservableCollection<TodoItem>(res);  // (await _dbService.GetItemsAsync());
            anzahl = _todoitems.Count;
            await _dialogService.DisplayAlertAsync("Undone", "Obs.collection count Undone: " + anzahl.ToString(), "OK");
        }

        private async void ListDoneEntries()
        {
            int anzahl;
            var res = await _dbService.GetItemsDoneAsync();
            TodoItems = new ObservableCollection<TodoItem>(res); 
            anzahl = _todoitems.Count;
            await _dialogService.DisplayAlertAsync("Done", "Obs.collection count Done: " + anzahl.ToString(), "OK");
        }

        private async void AddNewItem()
        {
            await _navigationService.NavigateAsync("TodoItemPage");
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            // Fill List
            // Reset the 'resume' id, since we just want to re-start here
            //ResumeAtTodoId = -1;
            //listView.ItemsSource = await App.Database.GetItemsAsync();

            try
            {
                SelectedItem = null;

                var res = await _dbService.GetItemsAsync();

                if (!Equals(res, null))
                {
                     TodoItems = new ObservableCollection<TodoItem>(res);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Properties
        /// </summary>

        private ObservableCollection<TodoItem> _todoitems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoitems; }
            set { SetProperty(ref _todoitems, value); }
        }

        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set { SetProperty(ref _todoItem, value); }
        }

        private TodoItem _selectedItem;
        public TodoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                IsItemSelected = !Equals(_selectedItem, null);
            }
        }

        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set { SetProperty(ref _isItemSelected, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }
        private bool _done;
        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }
    }
}
