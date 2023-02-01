using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
