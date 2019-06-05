using System;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XCountry.Views
{
    [MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}