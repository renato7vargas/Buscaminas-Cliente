using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SiguienteEscena : MonoBehaviour {

    public void cambiarEscena(int cambioEscena)
    {
        SceneManager.LoadScene(cambioEscena);
    }
    public void RegresarAudio() {
        AudioSource audio = new AudioSource();
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audio.Play();
    }
}