using Game.Managers;

namespace Game.Interfaces
{
    public interface ICameraManager
    {
        void SetCamera(CameraType state);
        void ShakeCamera(float amplitude, float frequency, float duration);
    }
}