using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SiguienteEscena : MonoBehaviour {

    public void cambiarEscena(int cambioEscena)
    {
        SceneManager.LoadScene(cambioEscena);
    }
}