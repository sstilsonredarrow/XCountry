using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XCountryCore.Models;
using XCountry.Views;
using XCountryCore.ViewModels;
using System.Windows.Input;
using XCountryCore.Services;

namespace XCountry.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ItemsPage
    {
        public DateTime StartTime { get; set; }
        public ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
           // ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }


        async void Handle_Time_Entered(object sender, System.EventArgs e)
        {
            if (!viewModel.RaceStarted)
                return;

            Button b = sender as Button;

            var id = b.AutomationId;
            var runner = viewModel.Items.FirstOrDefault(r => r.Id == id);
            int theOne = runner.UpdateTime(DateTime.Now.Subtract(StartTime));
            HandleButtonEnablement(b, theOne);
            await viewModel.UpdateTime(runner, 0);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel = this.BindingContext.DataContext as ItemsViewModel;
        }

        private bool HandleButtonEnablement(Button button, int theOne)
        {
            var stacker = button.Parent as StackLayout;
            var children = stacker.Children;
            var siblings = children.Where(c => c.GetType() == typeof(Button)).ToList();
            bool isSet = true;
            switch (theOne)
            {
                case 0:
                    siblings.First().IsEnabled = false;
                    break;
                case 1:
                    siblings.ElementAt(1).IsEnabled = false;
                    break;
                case 2:
                    siblings.Last().IsEnabled = false;
                    break;
                default: 
                    isSet = false;
                    break;
            }

            return isSet;
        }


        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            StartTime = DateTime.Now;
            viewModel.RaceStarted = !viewModel.RaceStarted;

            StartButton.Text = viewModel.RaceStarted ? "Stop" : "Start";

            await viewModel.Reset(viewModel.RaceStarted);

            if ( viewModel.RaceStarted)
            {
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    viewModel.ElapsedTime = DateTime.Now.Subtract(StartTime);

                    foreach (string s in viewModel.AutomationIds)
                    {

                    }

                    Device.BeginInvokeOnMainThread(() => viewModel.UpdateTimes());

                    //foreach(Runner r in viewModel.Items)
                    //{
                    //    var obj = this.ItemsListView.FindByName(r.Name) as Button;
                    //    obj.Text = viewModel.ElapsedTime.ToString("c");
                    //}
                    return viewModel.RaceStarted; // True = Repeat again, False = Stop the timer
                });
            }

        }
    }
}