using UniRx;
using UnityEngine;

namespace Common.Audio
{
    public class Sfx : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        public void Init(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
            
            Observable
                .EveryUpdate()
                .Where(_ => !_audioSource.isPlaying)
                .Subscribe(_ => OnStopAudioClip())
                .AddTo(this);
        }

        private void OnStopAudioClip()
        {
            Destroy(gameObject);
        }
    }
}