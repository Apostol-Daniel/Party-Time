using FreshMvvm;
using Mde.Project.Mobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Udara.Plugin.XFColorPickerControl;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class FirstColorModalViewModel : FreshBasePageModel
    {
        public PartyConfiguration PartyConfiguration { get; set; }
        public ICommand CloseModal { get; set; }
        private Color firstSelectorColor;
        public Color FirstSelectorColor 
        {
            get { return firstSelectorColor; }
            set { firstSelectorColor = value;
                RaisePropertyChanged();
            }
        }

        public FirstColorModalViewModel()
        {
            CloseModal = new Command(
            async () =>
            {
                PartyConfiguration.ColorNumber = 0;
                PartyConfiguration.FirstColor = FirstSelectorColor;
                await CoreMethods.PopPageModel(PartyConfiguration, modal: true);
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            PartyConfiguration = initData as PartyConfiguration;
        }
    }
}
