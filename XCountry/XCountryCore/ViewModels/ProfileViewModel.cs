using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XCountryCore.Models;
using XCountryCore.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace XCountryCore.ViewModels
{
    public class ProfileViewModel : MvxViewModel
    {
        public ObservableCollection<Profile> Profile { get; set; }
        private IProfileGetter _profileGetter;
        private IMvxNavigationService _navigationService;
        private Profile _selectedItem;

        public ProfileViewModel(IMvxNavigationService navigationService, IProfileGetter profileGetter)
        {
            _navigationService = navigationService;
           _profileGetter = profileGetter;
        }

        public Profile SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (_selectedItem == null)
                    return;               
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Profile = await _profileGetter.GetProfile();
            await RaisePropertyChanged(nameof(Profile));
        }
    }
}
