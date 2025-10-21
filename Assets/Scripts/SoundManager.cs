using System;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
 
 [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {

        [SerializeField] private AudioSource _sfxAudioSource;
        public AudioClip hitClip;
        public AudioClip showCatClip;
        [SerializeField] private AudioSource _bgmAudioSource;
        
        private void Start()
        {   
            Cat.OnCatHit+=PlayHitShot; 
            _bgmAudioSource.Play();
        }
        private void PlayHitShot(Object o, EventArgs e)
        {
            _sfxAudioSource.PlayOneShot(hitClip);
        }
        public void PlayShowCat()
        {
            _sfxAudioSource.PlayOneShot(showCatClip);
        }
        private void OnDestroy()
        {
            Cat.OnCatHit-=PlayHitShot; 
        }

    }
}