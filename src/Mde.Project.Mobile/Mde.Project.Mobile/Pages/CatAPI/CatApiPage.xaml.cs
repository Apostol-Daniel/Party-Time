using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatApiPage : ContentPage
    {
        public CatApiPage()
        {
            InitializeComponent();
            GetCatTypes();
            
            chkGif.Toggled += HandleSwitchToggledByUser;

            #region Hardcoded data
            //Supossed to be user input, better put in pickers to avoid writing validation
            List<string> catFilters = new List<string>()
            {
                "",
                "blur",
                "mono",
                "sepia",
                "negative",
                "paint",
                "pixel"
            };

            List<string> catTextColors = new List<string>()
            {
                "",
                "mistyrose",
                "midnightblue",
                "indigo",
                "lawngreen",
                "babyblue",
                "red",
                "green",
                "blue",
                "yellow",
                "orange",
                "purple",
                "black",
                "white",
            };

            foreach (var filter in catFilters)
            {
                slcCatFilter.Items.Add(filter);
            }

            foreach (var color in catTextColors)
            {
                slcCatPictureTextColor.Items.Add(color);
            }
            #endregion

            var placeholderUrl = new Uri("https://cataas.com/cat/gif/says/I%20am%20a%20placeholder?filter=&color=red");

            ReturnedCatPhoto.Source = ImageSource.FromUri(placeholderUrl);

        }


        public void HandleSwitchToggledByUser(object sender, ToggledEventArgs e)
        {

            if (chkGif.IsToggled == true)
            {
                slcCatType.SelectedItem = null;
                slcCatType.IsEnabled = false;
                slcCatType.BackgroundColor = Color.LightGray;
                lblFeedback.Text = "Cat type no longer available";
                lblFeedback.BackgroundColor = Color.OrangeRed;
                frmFeedback.BackgroundColor = Color.OrangeRed;
            }

            else if (chkGif.IsToggled == false)
            {
                slcCatType.IsEnabled = true;
                slcCatType.BackgroundColor = Color.FromHex("#F7DB69");
                lblFeedback.Text = "";
                lblFeedback.BackgroundColor = Color.Transparent;
                frmFeedback.BackgroundColor = Color.Transparent;

            }
        }

        #region Feedback Frame+Label
        public void LoadingCat()
        {
            lblFeedback.Text = "Loading Cat";
            lblFeedback.BackgroundColor = Color.DarkBlue;
            frmFeedback.BackgroundColor = Color.DarkBlue;
        }

        public void LoadedCat()
        {
            lblFeedback.Text = "Cat Loaded";
            lblFeedback.BackgroundColor = Color.DarkGreen;
            frmFeedback.BackgroundColor = Color.DarkGreen;
        }

        public void BadRequest()
        {
            lblFeedback.Text = "Bad Request";
            lblFeedback.BackgroundColor = Color.OrangeRed;
            frmFeedback.BackgroundColor = Color.OrangeRed;
        }
        #endregion


        private async void GetCat_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var catUrl = new Uri("https://cataas.com/cat?filter=&color=");
            var NotFound404 = new Uri("https://cataas.com/cat/wtf/says/404%20Not%20Found?size=50&color=red");
            var catLodaingUrl = new Uri("https://mk0supsystic186fa3rj.kinstacdn.com/wp-content/uploads/2016/04/cat-loader.gif");
            var randomGifCatUrl = catUrl;

            //Is Gif
            //if => gif with text
            if (chkGif.IsToggled == true && slcCatType.SelectedItem == null && txtCatText.Text == null)
            {
                randomGifCatUrl = new Uri($"https://cataas.com/cat/gif?/filter={slcCatFilter.SelectedItem}&color={slcCatPictureTextColor.SelectedItem}");
            }// else if => gif without text
            else if (chkGif.IsToggled == true && slcCatType.SelectedItem == null && txtCatText.Text != null)
            {
                randomGifCatUrl = new Uri($"https://cataas.com/cat/gif/says/{txtCatText.Text}?filter={slcCatFilter.SelectedItem}&color={slcCatPictureTextColor.SelectedItem}");
            }

            //Is not Gif
            if (chkGif.IsToggled == false && slcCatType.SelectedItem == null && txtCatText.Text != null)
            {
                randomGifCatUrl = new Uri($"https://cataas.com/cat/says/{txtCatText.Text}?filter={slcCatFilter.SelectedItem}&color={slcCatPictureTextColor}") ;
            }
            // if => Pic without type and text
            if (chkGif.IsToggled == false && slcCatType.SelectedItem == null && txtCatText.Text == null)
            {
                randomGifCatUrl = catUrl;
            }// if => Pic with type and witohut text
            else if (chkGif.IsToggled == false && slcCatType.SelectedItem != null && slcCatType.SelectedItem.ToString() != "" && txtCatText.Text == null)
            {
                randomGifCatUrl = new Uri($"https://cataas.com/cat/{slcCatType.SelectedItem}?filter={slcCatFilter.SelectedItem}&color={slcCatPictureTextColor.SelectedItem}");
            }// else if => Pic with type and text
            else if (chkGif.IsToggled == false && slcCatType.SelectedItem != null && slcCatType.SelectedItem.ToString() != ""  && txtCatText.Text != null)
            {
                randomGifCatUrl = new Uri($"https://cataas.com/cat/{slcCatType.SelectedItem}/says/{txtCatText.Text}?filter={slcCatFilter.SelectedItem}&color={slcCatPictureTextColor.SelectedItem}");
            }

            var response = await client.GetAsync(randomGifCatUrl);
            if (response.IsSuccessStatusCode)    
            {
                ReturnedCatPhoto.Source = ImageSource.FromUri(catLodaingUrl);
                LoadingCat();
                await Task.Delay(2000);
                ReturnedCatPhoto.Source = ImageSource.FromUri(randomGifCatUrl);
                LoadedCat();
            }
            else 
            {
                BadRequest();
                ReturnedCatPhoto.Source = ImageSource.FromUri(NotFound404);
            }

        }

        private async void GetCatTypes()
        {
            HttpClient client = new HttpClient();
            var url = "https://cataas.com/api/tags";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var catTypes = JsonConvert.DeserializeObject<List<string>>(responseContent);
                foreach (var type in catTypes)
                {
                    slcCatType.Items.Add(type);
                }
            }

        }
    }
}