using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public AudioSource Enviroment, player, ocean, fireplace;
    public AudioSource enemy = null;
    [SerializeField] Slider MasterAudioSlider, BGMSlider, SFXSlider;
    public AudioClip damageSFX, PickUpSFX, CrafteoSFX, VictorySFX, GameOverSFX;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        UnmuteAll();
    }
    void Update()
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(MasterAudioSlider.value) * 20);
        mixer.SetFloat("BGMVol", Mathf.Log10(BGMSlider.value) * 20);
        mixer.SetFloat("SFXVol", Mathf.Log10(SFXSlider.value) * 20);
    }

    public void UnmuteAll()
    {
        ocean.mute = false;
        fireplace.mute = false;
        Enviroment.mute = false;
        player.mute = false;
        enemy.mute = false;
    }

    public void MuteAll()
    {
        ocean.mute = true;
        fireplace.mute = true;
        Enviroment.mute = true;
        player.mute = true;
        enemy.mute = true;
    }
}