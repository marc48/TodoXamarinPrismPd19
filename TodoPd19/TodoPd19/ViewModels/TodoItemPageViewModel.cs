using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TodoPd19.Models;
using TodoPd19.Services;

namespace TodoPd19.ViewModels
{
	public class TodoItemPageViewModel : ViewModelBase, INavigationAware
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;

        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set { SetProperty(ref _todoItem, value); }
        }

        public DelegateCommand OnSaveClicked { get; set; }
        public DelegateCommand OnDeleteClicked { get; set; }
        public DelegateCommand OnCancelClicked { get; set; }
        public DelegateCommand OnSpeakClicked { get; set; }

        public TodoItemPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dbService = dbService;

            OnSaveClicked = new DelegateCommand(SaveItem);
            OnDeleteClicked = new DelegateCommand(DeleteItem);
            OnCancelClicked = new DelegateCommand(CancelPage);
            OnSpeakClicked = new DelegateCommand(SpeakNote);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //int id;
            if (parameters.ContainsKey("item"))
            {
                TodoItem = (TodoItem)parameters["item"];
                //id = (int)parameters["itemid"];
                //_todoItem = new TodoItem();
                //_todoItem = await _dbService.GetItemAsync(id);   <<<< so!

                //_dialogService.DisplayAlertAsync("Item", TodoItem.ID.ToString() + ", " + TodoItem.Name, "OK");

            }
        }

        private void SpeakNote()
        {
            _dialogService.DisplayAlertAsync("SpeakNote", "Not Implemented", "OK");
        }

        private void CancelPage()
        {
            _dialogService.DisplayAlertAsync("Cancel Page", "Go back async", "OK");
            _navigationService.GoBackAsync();
        }

        private void DeleteItem()
        {
            //_dialogService.DisplayAlertAsync("Del Item", "Not Implemented", "OK");
            //_dbService.DeleteItemAsync(TodoItem item);
        }

        private async void SaveItem()
        {
            TodoItem item = new TodoItem
            {
                ID = 0,
                Name = Name,
                Notes = Notes,
                Done = Done
            };

            await _dbService.SaveItemAsync(item);
            await _dialogService.DisplayAlertAsync("SaveItem", "eingefügt...", "OK");

            //Debug.WriteLine("Save Item erreicht");
 
            await _navigationService.GoBackAsync();
        }

        // Properties
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
