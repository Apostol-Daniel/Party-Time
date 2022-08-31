//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Mde.Project.Mobile.Domain.Interfaces;
//using Plugin.CurrentActivity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//namespace Mde.Project.Mobile.Droid
//{
//    public class StatusbarService : IStatusBarPlatformSpecific
//    {
//        public StatusbarService()
//        {

//        }

//        public void SetStatusBarColor(Color color)
//        {
//            // The SetStatusBarcolor is new since API 21
//            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
//            {
//                var androidColor = color.AddLuminosity(-0.1).ToAndroid();
//                //Just use the plugin
//                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
//            }
//            else
//            {
//                // Manually set colors in styles.xml            
//            }
//        }
//    }
//}