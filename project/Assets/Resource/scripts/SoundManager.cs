using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Click;
    public AudioClip Chalk;
    public void SfxChalk()
    {
        Source.clip = Chalk;
        Source.Play();
    }
    public void SfxClick()
    {
        Source.clip = Click;
        Source.Play();
    }
}
