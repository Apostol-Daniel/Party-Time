using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Interfaces
{
    public interface IFlashLightService
    {
        void TurnOnFlashLight();
        void TurnOffFlashLight();
        void DisharmoniousFlash();
    }
}
