using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using XCountryCore.Models;
using XCountryCore.Services;
using System.Linq;
using System.Xml.Linq;

namespace XCountryCore.ViewModels
{
    public class ItemsViewModel : MvxViewModel
    {
        public string Title { get; set; }
        private bool _isBusy { get; set; }
        public bool IsBusy { 
            get { return _isBusy; }
            set { _isBusy = value;  RaisePropertyChanged(nameof(IsBusy)); }
            }
        private ObservableCollection<RunnerViewModel> _items;
        public ObservableCollection<RunnerViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public IMvxCommand LoadItemsCommand { get; set; }
        public IMvxCommand HandleSplit1Command { get; set; }
        public ObservableCollection<Runner> Runners { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        private IRunnerService _runnerService;
        IDataStore<RunnerViewModel> DataStore;
        public bool RaceStarted { get; set; }

        public List<string> AutomationIds { get; set; }
        public string StartText { get; set; }

        public ItemsViewModel( IRunnerService runnerService,
            IDataStore<RunnerViewModel> dataStore)
        {
            Title = "Varsity Race";
            StartText = "Start";

            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());
            HandleSplit1Command = new MvxCommand(async () => await UpdateTimeCommand());
            AutomationIds = new List<string>();
            _runnerService = runnerService;
            DataStore = dataStore;
            RaceStarted = false;

        }

        async Task UpdateTimeCommand()
        {
            await Task.Delay(0);
        }

        public async Task UpdateTime(RunnerViewModel runner, int whichTime)
        {
            await _runnerService.SaveTimeAsync(runner, whichTime);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await ExecuteLoadItemsCommand();
            await RaisePropertyChanged(nameof(Items));
        }

        public async Task Reset(bool restart = false)
        {
            //IsBusy = true;

            //foreach (RunnerViewModel vm in this.Items)
            //{
            //    vm.Split1 = "0:00:00";
            //    vm.Split2 = "0:00:00";
            //    vm.Finish = "0:00:00";
            //    vm.Enabled = restart;
            //}
            //InvokeOnMainThread(() =>
            //{
            //    RaisePropertyChanged(nameof(Items));
            //    IsBusy = false;
            //});
            if ( restart )
            {
                await ExecuteLoadItemsCommand(true);
                await RaiseAllPropertiesChanged();
            }
                
            
        }

        public void UpdateTimes()
        {
        
            foreach(RunnerViewModel runner in Items)
            {

                if (!runner.Split1Set) { runner.Split1 = ElapsedTime.ToString(@"hh\:mm\:ss"); RaisePropertyChanged(nameof(RunnerViewModel.Split1)); }
                if (!runner.Split2Set) { runner.Split2 = ElapsedTime.ToString(@"hh\:mm\:ss"); RaisePropertyChanged(nameof(RunnerViewModel.Split2)); }
                if (!runner.FinishSet) { runner.Finish = ElapsedTime.ToString(@"hh\:mm\:ss"); RaisePropertyChanged(nameof(RunnerViewModel.Split2)); }
                    // runners.Add(runner);
                }
        }

        async Task ExecuteLoadItemsCommand(bool isRestart = false)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (Items != null)
                    Items.Clear();
                else
                    Items = new ObservableCollection<RunnerViewModel>();

              // var items = await DataStore.GetItemsAsync(true);
                var items = await _runnerService.GetRunnersForReal();

                foreach (var item in items)
                {
                    Items.Add(new RunnerViewModel
                    {
                        Name = item.Name,
                        Split1 = item.Split1,
                        Split2 = item.Split2,
                        Split1Set = false,
                        Split2Set = false,
                        Finish = item.Finish,
                        FinishSet = false,
                        Id = item.Id,
                        Enabled = isRestart,
                        Split1Enabled = isRestart,
                        Split2Enabled = isRestart,
                        FinishEnabled = isRestart
                    });
                    AutomationIds.Add(item.Id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}