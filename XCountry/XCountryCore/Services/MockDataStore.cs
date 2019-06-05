using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XCountryCore.Models;
using XCountryCore.ViewModels;

namespace XCountryCore.Services
{
    public class MockDataStore : IDataStore<RunnerViewModel>
    {
        ObservableCollection<RunnerViewModel> items;

        public MockDataStore()
        {
            items = new ObservableCollection<RunnerViewModel>();
            items = new ObservableCollection<RunnerViewModel>
            {
                new RunnerViewModel { Name = "Jim Dolan Jr.", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString()},
                new RunnerViewModel { Name = "Usain Bolt II", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString() },
                new RunnerViewModel { Name = "Asafa Powell Jr", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString() },
                new RunnerViewModel { Name = "Justin Gatlin 2", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString() },
                new RunnerViewModel { Name = "Houston McTear III", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString() },
                new RunnerViewModel { Name = "Roger Banister IV", Split1="0:00:00", Split2="0:00:00", Finish="0:00:00", Id = Guid.NewGuid().ToString() }
            };

        }

        public async Task<bool> AddItemAsync(RunnerViewModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(RunnerViewModel item)
        {
            var oldItem = items.Where((RunnerViewModel arg) => arg.Name == item.Name).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((RunnerViewModel arg) => arg.Name == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<RunnerViewModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Name == id));
        }

        public async Task<IEnumerable<RunnerViewModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}