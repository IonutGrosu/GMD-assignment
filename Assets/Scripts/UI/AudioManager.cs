using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace StarterAssets.UI
{
    public class AudioManager: MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] public TextMeshProUGUI musicText;
        private bool _isMusicDisabled;

        private void Start()
        {
            LoadValues();
        }

        private void Awake()
        {
            var musicObject = GameObject.FindGameObjectsWithTag("Music");
            if (musicObject.Length > 1)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void VolumeSlider()
        {
            var volume = volumeSlider.value;
            PlayerPrefs.SetFloat("Volume", volume);
            AudioListener.volume = _isMusicDisabled ? 0 : volume;
        }

        private void LoadValues()
        {
            var volume = PlayerPrefs.GetFloat("Volume");
            var musicDisabled= PlayerPrefs.GetInt("MusicDisabled");
            AudioListener.volume = musicDisabled == 1 ? 0 : volume;
            if (!_isMusicDisabled) volumeSlider.value = AudioListener.volume;
            musicText.SetText(musicDisabled == 1 ? "Off" : "On");
            musicText.color = musicDisabled == 1 ? Color.red : Color.green;
            _isMusicDisabled = musicDisabled == 1;
        }

        public void FlipMusic()
        {
            _isMusicDisabled = !_isMusicDisabled;
            musicText.SetText(_isMusicDisabled ? "Off" : "On");
            musicText.color = _isMusicDisabled ? Color.red : Color.green;
            AudioListener.volume = _isMusicDisabled ? 0 : volumeSlider.value;
            PlayerPrefs.SetInt("MusicDisabled", _isMusicDisabled ? 1 : 0);
        }
    }
}