using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;

    public AudioClip score;
    public AudioClip magnet;
    public AudioClip speed;
    public AudioClip key;
    public AudioClip scoreAllSelec;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBGM()
    {
        if(!bgm.isPlaying)
        {
            bgm.Play();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
}
