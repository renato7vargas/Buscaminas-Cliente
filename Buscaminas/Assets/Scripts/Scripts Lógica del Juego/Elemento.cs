/***************************************************************************************
 * Programador: Renato Vargas Gómez     Fecha de Actualización:21/11/2017
 * Proyecto: Buscaminas                 Carpeta: Lógica de Juego
 * Archivo: Elemento.cs
 * Descripción: Esta clase describe las propiedades y metodos que cada uno de
 *              los elementos en el tablero tendrá. Metodos tales como, interacción 
 *              con el usuario, cambio de sprites, llamadas al tablero general, invocación
 *              de la lista de ubicación de minas, etc.
 ********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elemento : MonoBehaviour {
    public bool capturado;
    public bool mina;
    public GameObject TextoPerdida;
    public GameObject Panel;
    public Sprite[] texturasVacias;
    public Sprite texturaMina;
    public Sprite banderita;

    void Start ()
    {
        ////////////////////////////////////////////////////////////////////////
        //Comprueba si este elemento es el primer elemento dibujado en pantalla
        ///////////////////////////////////////////////////////////////////////
        if (((int)transform.position.x==3) &&((int)transform.position.y==4))
        {
            AudioSource audio = new AudioSource();
            audio= GameObject.Find("Audio Source").GetComponent<AudioSource>();
            audio.Pause();

            GeneradorListas.GenerarLista();
            Tablero.juegoTerminado = false;
        }
        mina = Tablero.EsMina((int)transform.position.x, (int)transform.position.y,GeneradorListas.PosicionesMinasX,GeneradorListas.PosicionesMinasY);
        /////////////////////////
        //Registrar en el tablero
        /////////////////////////
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Tablero.elementos[x,y]= this;
    }

    /////////////////////////
    //Capturar una Mina
    /////////////////////////
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!Tablero.juegoTerminado)
            {
                if (mina == true)
                {
                    GetComponent<SpriteRenderer>().sprite = banderita;
                    capturado = true;
                    if (Tablero.EstaTerminado())
                    {
                        Panel.gameObject.SetActive(true);
                        Tablero.juegoTerminado = true;
                    }
                }
                else
                {
                    Tablero.DescubrirMinas();
                    TextoPerdida.gameObject.SetActive(true);
                    Tablero.juegoTerminado = true;
                }
            }
        }
    }

    public void CargarTextura(int casillasAdyacentes)
    {
        if (mina)
        {
            GetComponent<SpriteRenderer>().sprite = texturaMina;
        }else
        {
            GetComponent<SpriteRenderer>().sprite = texturasVacias[casillasAdyacentes];
        }
    }

    public bool EstaCubierto()
    {      
        return GetComponent<SpriteRenderer>().sprite.texture.name == "spritesMinas_0";
    }

    void OnMouseUpAsButton()
    {
        if (!Tablero.juegoTerminado)
        {
            if (mina)
            {
                Tablero.DescubrirMinas();
                TextoPerdida.gameObject.SetActive(true);
                Tablero.juegoTerminado = true;
            }
            else
            {
                //Mostrar numero de mina adyacente
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                CargarTextura(Tablero.MinasAdyacentes(x, y));

                //Descubrir areas sin minas
                Tablero.DescubrirFloodFill(x, y, new bool[Tablero.ancho, Tablero.largo]);
            }
        }
    }
}
