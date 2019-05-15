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
        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand ListCommand { get; set; }

        public TodoListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dbService = dbService;

            ItemAddedCommand = new DelegateCommand(AddNewItem);
            NewCommand = new DelegateCommand(AddFixEntry);
            ListCommand = new DelegateCommand(ListEntries);


        }

        private void AddFixEntry()
        {
            //Task<int> SaveItemAsync(TodoItem item)
            TodoItem item = new TodoItem();
            item.ID = 0;
            item.Name = "FixEintrag";
            item.Notes = "Notes: Text (Länge ???)";
            item.Done = false;

            _dbService.SaveItemAsync(item);
            _dialogService.DisplayAlertAsync("AddFixEntry", "eingefügt...", "OK");
        }

        private async void ListEntries()
        {
            int anzahl;
            _todoitems = new ObservableCollection<TodoItem>(await _dbService.GetItemsAsync());
            anzahl = _todoitems.Count;
            await _dialogService.DisplayAlertAsync("ListEntries", "Obs.collection Anzahl: " + anzahl.ToString(), "OK");

        }

        private async void AddNewItem()
        {
            await _navigationService.NavigateAsync("TodoItemPage");
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {

            // Reset the 'resume' id, since we just want to re-start here
            ResumeAtTodoId = -1;
            //listView.ItemsSource = await App.Database.GetItemsAsync();

            try
            {
                //IsItemSelected = false;
                SelectedItem = null;

                var res = await _dbService.GetItemsAsync();

                if (!Equals(res, null))
                    //_deviceService.BeginInvokeOnMainThread(() =>
                {
                     TodoItems = new ObservableCollection<TodoItem>(res);
                        //TestItemCount = res.Count;
                }
                //return true;
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
                //return false;
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
