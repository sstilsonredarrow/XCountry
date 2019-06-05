using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace XCountryCore.ViewModels
{
    public class AboutMeViewModel : MvxViewModel
    {
        public string url { get; set; }

        public AboutMeViewModel()
        {
        }

        public override Task Initialize()
        {
            base.Initialize();
            url = "http://softdev.mstclab.com/tkennedy/KennedyWebDesignFinalProject/KennedyWebDesignFinalProject/html/tk_launch.html";
            RaisePropertyChanged(nameof(url));
            return Task.Delay(0);
        }
    }
}

