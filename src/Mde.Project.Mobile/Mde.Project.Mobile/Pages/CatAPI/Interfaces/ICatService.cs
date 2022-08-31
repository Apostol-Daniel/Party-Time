using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Project.Mobile.Cat_API.Interfaces
{
    public interface ICatService
    {
        Task GetCustomCat(string CatUrl);
    }
}
