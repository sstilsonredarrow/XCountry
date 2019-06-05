using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XCountryCore.Models;
using XCountryCore.ViewModels;

namespace XCountryCore.Services
{
    public interface IRunnerService
    {
        Task<List<RunnerViewModel>> GetRunners();
        Task<List<RunnerViewModel>> GetRunnerFauxData();
        Task SaveTimeAsync(RunnerViewModel item, int time);
        Task<ObservableCollection<RunnerViewModel>> GetRunnersForReal();
    }
}
