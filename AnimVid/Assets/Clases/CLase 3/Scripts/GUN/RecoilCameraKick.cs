using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class RecoilCameraKick : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] cameras;

        CinemachineBasicMultiChannelPerlin[] _perlins;
        float[] _baseAmplitude;

        void Awake()
        {
            _perlins = new CinemachineBasicMultiChannelPerlin[cameras.Length];
            _baseAmplitude = new float[cameras.Length];
            for (int i = 0; i < cameras.Length; i++)
            {
                if (!cameras[i]) continue;
                _perlins[i] = cameras[i].GetComponent<CinemachineBasicMultiChannelPerlin>();
                if (_perlins[i]) _baseAmplitude[i] = _perlins[i].AmplitudeGain;
            }
        }

        public void Kick(float strength, float peakDuration, float recoverDuration)
        {
            StopAllCoroutines();
            StartCoroutine(KickRoutine(strength, peakDuration, recoverDuration));
        }

        IEnumerator KickRoutine(float strength, float peak, float recover)
        {
            // Pico
            float t = 0f;
            while (t < peak)
            {
                t += Time.deltaTime;
                float k = t / Mathf.Max(0.0001f, peak);
                for (int i = 0; i < _perlins.Length; i++)
                    if (_perlins[i]) _perlins[i].AmplitudeGain = Mathf.Lerp(_baseAmplitude[i], _baseAmplitude[i] + strength, k);
                yield return null;
            }

            t = 0f;
            while (t < recover)
            {
                t += Time.deltaTime;
                float k = t / Mathf.Max(0.0001f, recover);
                for (int i = 0; i < _perlins.Length; i++)
                    if (_perlins[i]) _perlins[i].AmplitudeGain = Mathf.Lerp(_baseAmplitude[i] + strength, _baseAmplitude[i], k);
                yield return null;
            }

            for (int i = 0; i < _perlins.Length; i++)
                if (_perlins[i]) _perlins[i].AmplitudeGain = _baseAmplitude[i];
        }
}
