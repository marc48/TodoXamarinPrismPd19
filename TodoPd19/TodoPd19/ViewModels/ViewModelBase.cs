using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoPd19.ViewModels
{
    public interface IInitialize
    {
        void OnInitialized(INavigationParameters parameters);
    }

    public class ViewModelBase : BindableBase, IInitialize, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        //private string _title;
        //public string Title
        //{
        //    get { return _title; }
        //    set { SetProperty(ref _title, value); }
        //}

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public void OnInitialized(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
