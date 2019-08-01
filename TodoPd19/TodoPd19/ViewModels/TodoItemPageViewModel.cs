using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using System;
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                
                //_todoItem = new TodoItem();
                //_todoItem = parameters.GetValue<TodoItem>("item");
                TodoItem = (TodoItem)parameters["item"];
                _dialogService.DisplayAlertAsync("On NavTo", ItemsExpander(TodoItem), "OK");
                TodoItem.ID = TodoItem.ID;
                Name = TodoItem.Name;
                Notes = TodoItem.Notes;
                _todoItem.Done = TodoItem.Done;
                // Item is filled by parameters, not by DB - correct solution?
                // TodoItem = await _dbService.GetItemAsync(id);
                EditMode = "Edit";
                StrId = _todoItem.Name + " (" + _todoItem.ID.ToString() + ")";  // ???
            }
            else
            {
                EditMode = "Add";
                StrId = "(New..)";
            }
        }

        private string ItemsExpander(TodoItem item)
        {
            string result;
            result = item.ID + " || " + item.Name + " || " + item.Notes + " || " + item.Done.ToString();
            return result;
        }

        private void SpeakNote()
        {
            _dialogService.DisplayAlertAsync("SpeakNote", "Not Implemented", "OK");
        }

        private void CancelPage()
        {
            //_dialogService.DisplayAlertAsync("Cancel Page", "Go back async", "OK");
            _navigationService.GoBackAsync();
        }

        private void DeleteItem()
        {
            int id = TodoItem.ID;   // nur für Alert
            _dbService.DeleteItemAsync(TodoItem);
            _dialogService.DisplayAlertAsync("Del Item", "Deleted ID: " + id.ToString(), "OK");
            _navigationService.GoBackAsync();
        }

        private async void SaveItem()
        {
            //var todoItem = (TodoItem)BindingContext;
            //await _dbService.SaveItemAsync(TodoItem);
            // TODO try
            if (EditMode == "Edit")       // (TodoItem.ID > 0)
            {
            //    int id = TodoItem.ID;
            //    await _dbService.GetItemAsync(id);
            //    //TodoItem item = new TodoItem;
                //int id = ID;
                TodoItem.Name = Name;
                TodoItem.Notes = Notes;
                TodoItem.Done = Done;
                await _dbService.UpdateItemAsync(TodoItem);
            //    //await _dialogService.DisplayAlertAsync("Update", "entry ID: " + id.ToString(), "OK");
            }
            else if (EditMode == "Add")
            {
                TodoItem item = new TodoItem();
                //{
                    //ID = 0,
                item.Name = Name;
                item.Notes = Notes;
                item.Done = Done;
                //};

                await _dbService.InsertItemAsync(item);
            //    //await _dialogService.DisplayAlertAsync("Insert", "New entry..." + item.Name, "OK");
            }

            await _navigationService.GoBackAsync();
        }

        // Properties
        private string _editMode;    // NMackay Crud
        public string EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private string _strid;
        public string StrId
        {
            get { return _strid; }
            set { SetProperty(ref _strid, value); }
        }

        //private int _id;
        //public int ID
        //{
        //    get { return _id; }
        //    set { SetProperty(ref _id, value); }
        //}

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
