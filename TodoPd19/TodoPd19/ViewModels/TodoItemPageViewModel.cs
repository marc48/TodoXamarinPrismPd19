using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoPd19.Models;

namespace TodoPd19.ViewModels
{
	public class TodoItemPageViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

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

        public TodoItemPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnSaveClicked = new DelegateCommand(SaveItem);
            OnDeleteClicked = new DelegateCommand(DeleteItem);
            OnCancelClicked = new DelegateCommand(CancelPage);
            OnSpeakClicked = new DelegateCommand(SpeakNote);
        }

        private void SpeakNote()
        {
            throw new NotImplementedException();
        }

        private void CancelPage()
        {
            throw new NotImplementedException();
        }

        private void DeleteItem()
        {
            throw new NotImplementedException();
        }

        private void SaveItem()
        {
            throw new NotImplementedException();
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
