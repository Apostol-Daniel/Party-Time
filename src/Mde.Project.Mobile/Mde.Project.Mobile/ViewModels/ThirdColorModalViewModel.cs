using FreshMvvm;
using Mde.Project.Mobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class ThirdColorModalViewModel : FreshBasePageModel
    {
        public PartyConfiguration PartyConfiguration { get; set; }
        public ICommand CloseModal { get; set; }
        private Color thirdSelectorColor;
        public Color ThirdSelectorColor
        {
            get { return thirdSelectorColor; }
            set
            {
                thirdSelectorColor = value;
                RaisePropertyChanged();
            }
        }

        public ThirdColorModalViewModel()
        {

            CloseModal = new Command(
            async () =>
            {
                PartyConfiguration.ColorNumber = 2;
                PartyConfiguration.ThirdColor = ThirdSelectorColor;
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
