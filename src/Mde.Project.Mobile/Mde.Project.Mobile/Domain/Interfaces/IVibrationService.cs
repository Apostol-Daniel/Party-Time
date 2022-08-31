namespace Mde.Project.Mobile.Domain.Services
{
    public interface IVibrationService
    {
        void TurnOffVibration();
        void TurnOnVibration();
        void DisharmoniousVibrate();
    }
}