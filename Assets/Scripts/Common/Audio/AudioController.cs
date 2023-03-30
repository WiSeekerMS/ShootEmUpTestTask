using UnityEngine;

namespace Common.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private SFX _sfxPrefab;

        public void PlayClip(AudioClip clip)
        {
            var sfx = Instantiate(_sfxPrefab);
            sfx.Init(clip);
        }
    }
}