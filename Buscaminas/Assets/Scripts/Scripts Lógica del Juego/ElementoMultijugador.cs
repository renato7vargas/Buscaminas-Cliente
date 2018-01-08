using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoMultijugador : MonoBehaviour
{
    public bool capturado;
    //Indica si el elemento es una mina 
    public bool mina;
    public GameObject TextoPerdida;
    public GameObject Panel;
    //public Cliente miCliente = new Cliente();
    //Diferentes sprites
    public Sprite[] texturasVacias;
    public Sprite texturaMina;
    public Sprite banderita;
    public static ListasMinas listas = new ListasMinas();


    void Start()
    {
        print("Posición: "+transform.position.x+", "+transform.position.y);
        if (((int)transform.position.x == 3) && ((int)transform.position.y == 7))
        {
            ClienteObjeto miCliente;
            miCliente = GameObject.Find("Cliente").GetComponent<ClienteObjeto>();

            GeneradorListaEstatico generador = miCliente.GetGenerador();
            generador = miCliente.GetGenerador();
            listas.PosicionesMinasX = generador.PosicionesMinasX;
            listas.PosicionesMinasY = generador.PosicionesMinasY;
            //listas.PosicionesMinasX = miCliente.generadorGlobal.PosicionesMinasY;

        }


        mina = TableroMultijugador.EsMina((int)transform.position.x, (int)transform.position.y, listas.PosicionesMinasX, listas.PosicionesMinasY);

        //Asignación de un 15 por ciento de probabilidad de que el elemento esté minado
        //mina = Random.value < 0.25;

        //Registrar en el tablero
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        TableroMultijugador.elementos[x, y] = this;
    }

    /////////////////////////
    //Capturar una Mina
    /////////////////////////
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!TableroMultijugador.juegoTerminado)
            {
                if (mina == true)
                {
                    GetComponent<SpriteRenderer>().sprite = banderita;
                    capturado = true;
                    if (TableroMultijugador.EstaTerminado())
                    {
                        Panel.gameObject.SetActive(true);
                        TableroMultijugador.juegoTerminado = true;
                    }
                }
                else
                {
                    TableroMultijugador.DescubrirMinas();
                    TextoPerdida.gameObject.SetActive(true);
                    TableroMultijugador.juegoTerminado = true;
                }
            }
        }
    }


    public void CargarTextura(int casillasAdyacentes)
    {
        if (mina)
        {
            GetComponent<SpriteRenderer>().sprite = texturaMina;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = texturasVacias[casillasAdyacentes];
        }
    }
    //Indica si no se le hadado click a una casilla o si sigue "cubierta"
    public bool EstaCubierto()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "Casilla";
    }
    void OnMouseUpAsButton()
    {
        if (!TableroMultijugador.juegoTerminado)
        {
            if (mina)
            {
                TableroMultijugador.DescubrirMinas();
                TextoPerdida.gameObject.SetActive(true);
                TableroMultijugador.juegoTerminado = true;
            }
            else
            {
                //Mostrar numero de mina adyacente
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                CargarTextura(TableroMultijugador.MinasAdyacentes(x, y));

                //Descubrir areas sin minas
                TableroMultijugador.DescubrirFloodFill(x, y, new bool[TableroMultijugador.ancho, TableroMultijugador.largo]);
            }
        }
    }
}
