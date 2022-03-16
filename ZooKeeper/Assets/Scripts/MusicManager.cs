using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> BGM;
    public List<AudioClip> musics;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    void Start()
    {
        foreach (AudioClip clip in BGM)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayMusic(int index)
    {
        audioSource.PlayOneShot(musics[index]);
    }
}
