using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarAudio : MonoBehaviour {

    public void Pausar() {
        Destroy(GameObject.Find("Audio Source"));
    }
}
