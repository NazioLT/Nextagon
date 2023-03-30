using UnityEngine;
using UnityEngine.Audio;
using Nazio_LT.Tools.Core;
using System.Collections.Generic;

namespace Nazio_LT.Tools.UI
{
    [System.Serializable]
    public class OptionsSettings : Child<Menu>
    {
        #region Sub Classes

        [System.Serializable]
        private class AudioChannelController : Child<OptionsSettings>
        {
            [SerializeField] private NSlider slider;
            [SerializeField] private AudioMixerGroup audioGroup;

            public override void Init(OptionsSettings _parent)
            {
                base.Init(_parent);

                slider.Init(audioGroup.name, (_value) => SetVolume(_value), Mathf.InverseLerp(-80f, 0f, ChannelValue));
            }

            private void SetVolume(float _value) => Parent.Mixer.SetFloat(audioGroup.name, -80 + 80 * _value);//Pour pas baisser trop de volume au début
            private void SetSliderValue(float _value) => slider.value = _value;
            public void SetAllValue(float _value)
            {
                SetVolume(_value);
                SetSliderValue(_value);
            }

            public float ChannelValue
            {
                get
                {
                    Parent.Mixer.GetFloat(audioGroup.name, out float _channelLevel);
                    return _channelLevel;
                }
            }
        }

        #endregion

        [Header("Sound Settings")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioChannelController[] channels;
        [SerializeField] private NToggle fullscreenToggle;
        [SerializeField] private NDropdown resolutionDropdown;

        private Resolution[] resolutions;

        public override void Init(Menu _parent)
        {
            base.Init(_parent);

            foreach (var _channel in channels)
            {
                _channel.Init(this);
            }

            if (fullscreenToggle != null) fullscreenToggle.Init((_bool) => SetFullscreen(_bool), "FullScreen", Screen.fullScreen);
            if (resolutionDropdown != null) InitResolutionsDropdown();
        }

        private void InitResolutionsDropdown()
        {
            resolutions = Screen.resolutions;

            List<string> _labels = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string _option = resolutions[i].width + " x " + resolutions[i].height;
                _labels.Add(_option);
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.Init((_v) => SetResolution(_v), "Resolutions", _labels, currentResolutionIndex);
        }

        public void SetFullscreen(bool _value) => Screen.fullScreen = _value;
        public void SetResolution(int _value) => Screen.SetResolution(resolutions[_value].width, resolutions[_value].height, Screen.fullScreen);

        public AudioMixer Mixer => audioMixer;
    }
}