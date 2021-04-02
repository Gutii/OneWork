using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SoundManager:MonoBehaviour
{
    public static bool flagMute;

    public Sound[] sound;
    public SoundEnum soundEnum;

    public enum SoundEnum
    {
        Background,
        BackgroundShop,
        Money,
        buy,
        AcceptJob
    }

    private void Awake()
    {
        PlayBackGround(soundEnum);
    }

    private AudioSource Audio(SoundEnum _sound, GameObject gameObject)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = sound[(int)_sound].audioClip;
        audioSource.volume = sound[(int)_sound].volume;
        return audioSource;
    }

    public void PlayBackGround(SoundEnum _sound)
    {
        Audio(_sound, gameObject).Play();          
    }

    public void PlaySound(SoundEnum _sound, GameObject gameObject)
    {
        Audio(_sound, gameObject).PlayOneShot(sound[(int)_sound].audioClip);
    }

    public static void SoundMute()
    {
        if (flagMute)
        {
            
        }
    }
}
