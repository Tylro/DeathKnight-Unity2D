using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundOnClick()
    {
        audioSource.PlayOneShot(click);
    }
    public void SoundOnHover()
    {
        audioSource.PlayOneShot(hover);
    }
}
