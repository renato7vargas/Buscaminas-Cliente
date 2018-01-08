using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorListas {
    public static int[] PosicionesMinasX = new int[8];
    public static int[] PosicionesMinasY = new int[8];
    //Funcion que le asgina datos a ambas listas del generador
    public static void GenerarLista() {
        System.Random aleatorio = new System.Random();
        do
        {
            for (int i = 0; i < 8; i++) {
                PosicionesMinasX[i] = aleatorio.Next(0, 8);
                PosicionesMinasY[i] = aleatorio.Next(0, 8);
            }
        } while (ParesRepetidos()==true);   
    }
    //Funcion que comprueba que no haya pares repetidos en la lista generada aleatoriamente
    public static bool ParesRepetidos() {
        bool repetidos = true;
        int xAcomparar=0;
        int yAcomparar=0;
        int posicionAcomparar = 0;
        for (int c=0;c<8;c++) {
            posicionAcomparar = c;
            xAcomparar = PosicionesMinasX[posicionAcomparar];
            yAcomparar = PosicionesMinasY[posicionAcomparar];
            for (int i = 0; i < 8; i++) {
                if (i != posicionAcomparar)
                {
                    if ((PosicionesMinasX[i] == xAcomparar) && (PosicionesMinasY[i] == yAcomparar))
                    {
                        repetidos = true;
                        i = 8; //El for anidado se romperá en el mmoto que haya elementos respetidos
                        c = 8;// El for contenedor se romperá en el momento que haya elementos repetidos
                    }
                    else
                    {
                        repetidos = false;
                    }
                }
            }
        }
        return repetidos;
    }

}
