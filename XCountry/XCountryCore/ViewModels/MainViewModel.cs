using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace XCountryCore.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService service)
        {
            _navigationService = service;
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override async void ViewAppearing()
        {
            await ShowInitialViewModels();
            base.ViewAppearing();
        }

        private async Task ShowInitialViewModels()
        {      

           // await _navigationService.Navigate<AboutMeViewModel>();
           // await _navigationService.Navigate<ProfileViewModel>();

        }
    }
}