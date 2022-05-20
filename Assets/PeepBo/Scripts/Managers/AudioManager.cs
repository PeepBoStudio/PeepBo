using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeepBo.Managers
{
    public class AudioManager
    {
        private AudioClip clickSound;
        private AudioSource mainAudioSource;
        private AudioSource clickAudioSource;

        public void InitAudioManager()
        {
            mainAudioSource = GameObject.Find("@Scene").GetComponent<AudioSource>();

            clickAudioSource = GameManager.Instance.gameObject.AddComponent<AudioSource>();

            clickSound = GameManager.Resource.Load<AudioClip>("Audios/Å¬¸¯");
        }

        public void PlayClickSound()
		{
            clickAudioSource.PlayOneShot(clickSound);
		}
    }
}
