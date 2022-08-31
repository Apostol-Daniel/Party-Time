using FreshMvvm;
using Mde.Project.Mobile.Domain.Constants;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Services;
using MediaManager.Library;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class PartyConfigurationViewModel : FreshBasePageModel
    {
        #region Variables
        private readonly PartyActionService _partyActionService;
        public ICommand OpenFirstColorModalPage { get; set; }
        public ICommand OpenSecondColorModalPage { get; set; }
        public ICommand OpenThirdColorModalPage { get; set; }
        public ICommand OpenPartySongSelectorPage { get; set; }
        public ICommand OpenPartyActionPage { get; set; }

        private Color? firstColorModal;
        public Color? FirstColorModal
        {
            get { return firstColorModal; }
            set
            {
                firstColorModal = value;
                RaisePropertyChanged();
            }
        }

        private Color? secondColorModal;
        public Color? SecondColorModal
        {
            get { return secondColorModal; }
            set
            {
                secondColorModal = value;
                RaisePropertyChanged();
            }
        }

        private Color? thirdColorModal;
        public Color? ThirdColorModal
        {
            get { return thirdColorModal; }
            set
            {
                thirdColorModal = value;
                RaisePropertyChanged();
            }
        }

        private IMediaItem selectedSong;
        public IMediaItem SelectedSong
        {
            get { return selectedSong; }
            set
            {
                selectedSong = value;
                RaisePropertyChanged();
            }
        }

        private string selectedSongTitleArtist;
        public string SelectedSongTitleArtist
        {
            get { return selectedSongTitleArtist; }
            set
            {
                selectedSongTitleArtist = value;
                RaisePropertyChanged();
            }
        }

        private string colorFeedbackTxt;
        public string ColorFeedbackTxt
        {
            get { return colorFeedbackTxt; }
            set
            {
                colorFeedbackTxt = value;
                RaisePropertyChanged();
            }
        }

        private int? colorNumber;
        public int? ColorNumber
        {
            get { return colorNumber; }
            set
            {
                colorNumber = value;
                RaisePropertyChanged();
            }
        }

        private bool isFlash;
        public bool IsFlash
        {
            get { return isFlash; }
            set
            {
                isFlash = value;
                RaisePropertyChanged();
            }
        }

        private bool isVibrate;
        public bool IsVibrate
        {
            get { return isVibrate; }
            set
            {
                isVibrate = value;
                RaisePropertyChanged();
            }
        }

        private Color songColorFeedback;
        public Color SongColorFeedback
        {
            get { return songColorFeedback; }
            set
            {
                songColorFeedback = value;
                RaisePropertyChanged();
            }
        }

        private Color colorSelectColorFeedback;
        public Color ColorSelectColorFeedback
        {
            get { return colorSelectColorFeedback; }
            set
            {
                colorSelectColorFeedback = value;
                RaisePropertyChanged();
            }
        }

        //private bool isPartyReady;
        //public bool IsPartyReady
        //{
        //    get { return isPartyReady; }
        //    set
        //    {
        //        isPartyReady = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public bool IsPartyReady { get; set; }
        #endregion

        public PartyConfigurationViewModel(PartyActionService partyActionService)
        {
            _partyActionService = partyActionService;
            FirstColorModal = Color.Transparent;
            SecondColorModal = Color.Transparent;
            ThirdColorModal = Color.Transparent;
            if (SelectedSong == null)
            {
                SongColorFeedback = Color.White;
                SelectedSongTitleArtist = FeedbackMessages.NoSongSelected;
            }

            ColorSelectColorFeedback = Color.White;
            ColorFeedbackTxt = FeedbackMessages.SelectColors;

            var PartySettings = new PartyConfiguration
            {
                FirstColor = FirstColorModal,
                SecondColor = SecondColorModal,
                ThirdColor = ThirdColorModal,
                SelectedSong = SelectedSong,
                ColorNumber = ColorNumber,
                SongTitleArtist = SelectedSongTitleArtist,
                IsFlash = IsFlash,
                IsVibrate = IsVibrate,
            };

            #region Commands
            OpenFirstColorModalPage = new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<FirstColorModalViewModel>(PartySettings, modal: true);
            });

            OpenSecondColorModalPage = new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<SecondColorModalViewModel>(PartySettings, modal: true);
            });

            OpenThirdColorModalPage = new Command(
           async () =>
           {
               await CoreMethods.PushPageModel<ThirdColorModalViewModel>(PartySettings, modal: true);
           });

            OpenPartySongSelectorPage = new Command(
            async () =>
            {
                //Update bools here because otherwise they do not get updated ???
                PartySettings.IsVibrate = IsVibrate;
                PartySettings.IsFlash = IsFlash;               
                await CoreMethods.PushPageModel<PartySongSelectorViewModel>(PartySettings, modal: true);
            });

            OpenPartyActionPage = new Command(
            async () =>
            {
                // returned IsPartyReady bool is always false when using this function
                //await PartyActionValidations(IsPartyReady);
                #region Validation
                if (SelectedSong == null)
                {
                    SelectedSongTitleArtist = FeedbackMessages.SelectSong;
                    SongColorFeedback = Color.OrangeRed;
                    return;
                }

                if (FirstColorModal == Color.Transparent)
                {
                    ColorFeedbackTxt = FeedbackMessages.SelectColors;
                    ColorSelectColorFeedback = Color.OrangeRed;
                    return;
                }

                if(SecondColorModal == Color.Transparent) 
                {
                    ColorFeedbackTxt = FeedbackMessages.SelectColors;
                    ColorSelectColorFeedback = Color.OrangeRed;
                    return;
                }

                if (ThirdColorModal == Color.Transparent)
                {
                    ColorFeedbackTxt = FeedbackMessages.SelectColors;
                    ColorSelectColorFeedback = Color.OrangeRed;
                    return;
                }

                if (FirstColorModal != Color.Transparent && SecondColorModal != Color.Transparent && ThirdColorModal != Color.Transparent)
                {
                    ColorFeedbackTxt = FeedbackMessages.ColorReady;
                    ColorSelectColorFeedback = Color.ForestGreen;
                }
                #endregion

                //Update bools here because otherwise they do not get updated ???
                PartySettings.IsVibrate = IsVibrate;
                PartySettings.IsFlash = IsFlash;               
                await CoreMethods.PushPageModel<PartyActionViewModel>(PartySettings, modal: false);
                
            });
            #endregion
        }

        public override void ReverseInit(object returnedData)
        {

            base.ReverseInit(returnedData);
            Debug.WriteLine(returnedData.ToString());

            var PartyConfiguration = returnedData as PartyConfiguration;
            if (PartyConfiguration.ColorNumber == 0)
            {
                FirstColorModal = PartyConfiguration.FirstColor;
            }
            if (PartyConfiguration.ColorNumber == 1)
            {
                SecondColorModal = PartyConfiguration.SecondColor;
            }
            if (PartyConfiguration.ColorNumber == 2)
            {
                ThirdColorModal = PartyConfiguration.ThirdColor;
            }
            if (PartyConfiguration.SelectedSong != null)
            {
                SongColorFeedback = Color.White;
                SelectedSong = PartyConfiguration.SelectedSong;
                SelectedSongTitleArtist = PartyConfiguration.SongTitleArtist;
            }

        }

        //public async Task<bool> PartyActionValidations(bool IsPartyReady)
        //{
        //    if (SelectedSong == null)
        //    {
        //        SelectedSongTitleArtist = FeedbackMessages.SelectSong;
        //        SongColorFeedback = Color.OrangeRed;
        //        return IsPartyReady == false;
        //    }

        //    if (FirstColorModal == null && SecondColorModal == null && ThirdColorModal == null)
        //    {
        //        ColorFeedbackTxt = FeedbackMessages.SelectColors;
        //        ColorSelectColorFeedback = Color.OrangeRed;
        //        return IsPartyReady == false;
        //    }

        //    if (FirstColorModal != null && SecondColorModal != null && ThirdColorModal != null)
        //    {
        //        ColorFeedbackTxt = FeedbackMessages.ColorReady;
        //        ColorSelectColorFeedback = Color.ForestGreen;
        //    }

        //    return IsPartyReady == true;
        //}
    }
}
