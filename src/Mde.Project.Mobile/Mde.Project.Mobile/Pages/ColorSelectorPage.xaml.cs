using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorSelectorPage : ContentPage
    {
        public Color FirstSelectedColor { get; set; }
        public Color SecondSelectedColor { get; set; }
        public Color ThirdSelectedColor { get; set; }
        public ColorSelectorPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true; 
        }

        private void ColorPicker0_PickedColorChanged(object sender, Color colorPicked)
        {
            //Probably a bug, the editor says that the method is unused
            //If deleted, will not work anymore obviously ¯\_(ツ)_/¯
            // Use the selected color
            SelectedColorDisplayFrame0.BackgroundColor = colorPicked;
            ColorPickerHolderFrame0.BackgroundColor = colorPicked;
            
        }

        private void ColorPicker1_PickedColorChanged(object sender, Color colorPicked)
        {
            //Probably a bug, the editor says that the method is unused
            //If deleted, will not work anymore obviously ¯\_(ツ)_/¯
            // Use the selected color
            SelectedColorDisplayFrame1.BackgroundColor = colorPicked;
            ColorPickerHolderFrame1.BackgroundColor = colorPicked;
            
        }

        private void ColorPicker2_PickedColorChanged(object sender, Color colorPicked)
        {
            //Probably a bug, the editor says that the method is unused
            //If deleted, will not work anymore obviously ¯\_(ツ)_/¯
            // Use the selected color
            SelectedColorDisplayFrame2.BackgroundColor = colorPicked;
            ColorPickerHolderFrame2.BackgroundColor = colorPicked;
           
        }

        private async void ConfirmColors_Clicked(object sender, EventArgs e)
        {
            FirstSelectedColor = ColorPicker0.PickedColor;
            SecondSelectedColor = ColorPicker1.PickedColor;
            ThirdSelectedColor = ColorPicker2.PickedColor;
            await Navigation.PopModalAsync(true);
        }
    }
}