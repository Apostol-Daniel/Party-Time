using FreshMvvm;
using Mde.Project.Mobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class SecondColorModalViewModel : FreshBasePageModel
    {
        public PartyConfiguration PartyConfiguration { get; set; }
        public ICommand CloseModal { get; set; }
        private Color secondSelectorColor;
        public Color SecondSelectorColor 
        {
            get { return secondSelectorColor; }
            set { secondSelectorColor = value;
                RaisePropertyChanged();
                }
        }

        public SecondColorModalViewModel()
        {

            CloseModal = new Command(
            async () =>
            {
                PartyConfiguration.ColorNumber = 1;
                PartyConfiguration.SecondColor = SecondSelectorColor;
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

