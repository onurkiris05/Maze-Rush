using System.Collections;
using Cinemachine;
using Game.Interfaces;
using UnityEngine;

namespace Game.Managers
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [Header("Components")]
        [SerializeField] private CinemachineVirtualCamera runningCam;
        [SerializeField] private CinemachineVirtualCamera closeLookUpCam;

        private CinemachineVirtualCamera _currentCam;
        private CinemachineBasicMultiChannelPerlin _noise;
        private Coroutine _shakeCoroutine;

        public void SetCamera(CameraType state)
        {
            runningCam.Priority = state == CameraType.Follow ? 10 : 0;
            closeLookUpCam.Priority = state == CameraType.CloseLookUp ? 10 : 0;

            _currentCam = state == CameraType.Follow ? runningCam : closeLookUpCam;
        }

        public void ShakeCamera(float amplitude, float frequency, float duration)
        {
            if (_currentCam != null)
                _noise = _currentCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (_noise == null)
            {
                Debug.LogError($"Noise component is null! " +
                               $"Setup 'BasicMultiChannelPerlin' from virtual camera settings");
                return;
            }

            if (_shakeCoroutine != null)
                StopCoroutine(_shakeCoroutine);

            _shakeCoroutine = StartCoroutine(ProcessCameraShake(amplitude, frequency, duration));
        }

        private IEnumerator ProcessCameraShake(float amplitude, float frequency, float duration)
        {
            _noise.m_AmplitudeGain = amplitude;
            _noise.m_FrequencyGain = frequency;

            yield return Helpers.BetterWaitForSeconds(duration);
            StopShaking();
        }

        private void StopShaking()
        {
            _noise.m_AmplitudeGain = 0f;
            _noise.m_FrequencyGain = 0f;
        }
    }

    public enum CameraType
    {
        Follow,
        CloseLookUp,
    }
}