using System;
using System.Collections.ObjectModel;

namespace XCountryCore.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
        public string PreviousCollege { get; set; }
        public string CurrentCollege { get; set; }
    }

    public class ProfileList
    {
        public ObservableCollection<Profile> profile { get; set; }
    }
}
