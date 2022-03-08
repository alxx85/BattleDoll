using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsWindow : Window
{
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;
    [SerializeField] private Toggle _muteToggle;
    [SerializeField] private AudioMixer audioMixer;

    private Window _startWindow;

    private const string MusicVolume = "Music";
    private const string EffectsVolume = "SoundEffects";

    protected override void OnEnable()
    {
        base.OnEnable();
        _sensitivitySlider.onValueChanged.AddListener(OnChangedSensitivity);
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _effectSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        _muteToggle.onValueChanged.AddListener(OnChangedMute);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _sensitivitySlider.onValueChanged.RemoveListener(OnChangedSensitivity);
        _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        _effectSlider.onValueChanged.RemoveListener(OnEffectsVolumeChanged);
        _muteToggle.onValueChanged.RemoveListener(OnChangedMute);
    }

    public void Init(PlayerMover player, Window startWindow)
    {
        _player = player;
        _sensitivitySlider.value = _player.RotateSpeed;
        audioMixer.GetFloat(MusicVolume, out float musicVolume);
        audioMixer.GetFloat(EffectsVolume, out float effectsVolume);
        _musicSlider.value = musicVolume;
        _effectSlider.value = effectsVolume;
        _startWindow = startWindow;
    }

    private void OnMusicVolumeChanged(float value)
    {
        audioMixer.SetFloat(MusicVolume, value);
    }

    private void OnEffectsVolumeChanged(float value)
    {
        audioMixer.SetFloat(EffectsVolume, value);
    }

    private void OnChangedSensitivity(float value)
    {
        _player.ChangedRotateSpeed(value);
    }

    private void OnChangedMute(bool value)
    {
        AudioListener.volume = value? 1: 0;
    }

    protected override void OnExitButtonClick()
    {
        _startWindow.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
