using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip avatarSelect;
    public AudioClip background2;
    public AudioClip goodRep;
    public AudioClip badRep;
    public AudioClip dice;
    public AudioClip destination;
    public AudioClip background3;

    public float _currentTime;

    private void start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Pause music if audio icon is pressed
    public void ToggleMusic()
    {
        //musicSource.mute = !musicSource.mute;
        if(musicSource.isPlaying)
        {
            _currentTime = musicSource.time;
            musicSource.Pause();
        }
        else
        {
            musicSource.time = _currentTime;
            musicSource.UnPause();
        }
    }
}
