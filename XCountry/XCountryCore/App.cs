using System;
using System.Reflection;
using XCountryCore.Services;
using XCountryCore.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace XCountryCore
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            //var builder = new ContainerBuilder();
            Mvx.IoCProvider.RegisterType<IRunnerService, RunnerService>();
            Mvx.IoCProvider.RegisterType<IDataStore<RunnerViewModel>, MockDataStore>();
            RegisterAppStart<MainViewModel>();
        }
    }
}
