using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mde.Project.Mobile.Domain.Interfaces
{
    public interface IStatusBarPlatformSpecific
    {
        void SetStatusBarColor(Color color);
    }
}
