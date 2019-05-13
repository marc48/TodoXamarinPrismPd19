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
	public class TodoListPageViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;


        public TodoListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dbService = dbService;


        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {

            // Reset the 'resume' id, since we just want to re-start here
            //((App)App.Current).ResumeAtTodoId = -1;
            //listView.ItemsSource = await App.Database.GetItemsAsync();

            try
            {
                //IsItemSelected = false;
                //SelectedItem = null;

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



    }
}
