using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorAudio : MonoBehaviour {
    public static ManejadorAudio Instance = null;
    private AudioSource soundEffectAudio;
    void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        AudioSource theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;

         
	}
    public void PlayOneShot(AudioClip clip) {
        soundEffectAudio.PlayOneShot(clip);
    }
   
    public void CambiarTema()
    {
        AudioSource audio = new AudioSource();
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audio.Play();

    }

}
