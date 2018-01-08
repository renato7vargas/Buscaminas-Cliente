using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class EnviarUsuario : MonoBehaviour{
    public Text nombreUsuario;
    public Text contrasena;
    private bool usuarioExiste;

    public void EnviarDatosUsuario(int escena)
    {
        Peticion nuevoUsuario= new Peticion();
        Peticion nuevaLista = new Peticion();
        nuevaLista.TipoPeticion = "Enviar Lista";
        nuevoUsuario.TipoPeticion="Comprobar usuario";
        nuevoUsuario.NombreUsuario= nombreUsuario.text.ToString();
        nuevoUsuario.Contrasena = contrasena.text.ToString();
        ClienteObjeto cliente;
        cliente = GameObject.Find("Cliente").GetComponent<ClienteObjeto>();

        usuarioExiste=cliente.EnviarUsuario(nuevoUsuario);
        if (usuarioExiste == true) {
            cliente.AdquirirLista(nuevaLista);
            CambiarEscena(escena);

        }
        



    }
    public void CambiarEscena(int cambioEscena)
    {
        SceneManager.LoadScene(cambioEscena);
    }
}
