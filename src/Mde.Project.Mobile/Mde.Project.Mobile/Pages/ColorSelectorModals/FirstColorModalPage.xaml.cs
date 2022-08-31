using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstColorModalPage : ContentPage
    {
        public Color FirstSelectedColor { get; set; }
        public FirstColorModalPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
        {
            //Probably a bug, the editor says that the method is unused
            //If deleted, will not work anymore obviously ¯\_(ツ)_/¯
            // Use the selected color
            SelectedColorDisplayFrame.BackgroundColor = colorPicked;
            ColorPickerHolderFrame.BackgroundColor = colorPicked;
        }      
    }
}