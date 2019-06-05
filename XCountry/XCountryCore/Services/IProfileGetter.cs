using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XCountryCore.Models;

namespace XCountryCore.Services
{
    public interface IProfileGetter
    {
        Task<ObservableCollection<Profile>> GetProfile();
    }
}
