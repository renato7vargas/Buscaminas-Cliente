using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtenerHora : MonoBehaviour {

    public void ConseguirHora() {
        ClienteObjeto cliente;
        cliente=GameObject.Find("Cliente").GetComponent<ClienteObjeto>();
        cliente.ObtenerHora();
    }
}
