﻿/*****************************************************************************************************
 * Programador: Renato Vargas Gómez     Fecha de Actualización:21/11/2017
 * Proyecto: Buscaminas                 Carpeta: Lógica de Juego
 * Archivo: Tablero.cs
 * Descripción: Esta clase lleva a cabo las tareas y funciones que requieren el uso y/o participación
 *              de multiples elementos del mismo tablero.
 ****************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablero{
    public static bool juegoTerminado=false;
    public static int ancho =8;
    public static int largo = 8;
    public static Elemento[,] elementos = new Elemento[ancho, largo];

    public static void DescubrirMinas()
    {
        foreach (Elemento elem in elementos)
        {
            if (elem.mina)
            { 
                elem.CargarTextura(0);//Carga textura de mina
            }
        }
    }

    public static bool MinaEstaEn (int x, int y)
    {
        if (x >= 0 && y >= 0 && x < ancho && y < largo)
        {
            return elementos[x, y].mina;
        } else
        {
            return false;
        }  
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //Cuenta las minas adyacentes a la casilla seleccionada en sentido de las manecillas del reloj
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public static int MinasAdyacentes(int x, int y)
    {
        int contador = 0;
        if (MinaEstaEn(x,   y+1)) ++contador;//Arriba
        if (MinaEstaEn(x+1, y+1)) ++contador;//Arriba a la Derecha
        if (MinaEstaEn(x+1, y  )) ++contador;//Derecha
        if (MinaEstaEn(x+1, y-1)) ++contador;//Abajo a la Derecha
        if (MinaEstaEn(x,   y-1)) ++contador;//Abajo
        if (MinaEstaEn(x-1, y-1)) ++contador;//Abajo a la Izquierda
        if (MinaEstaEn(x-1, y  )) ++contador;//Izquierda
        if (MinaEstaEn(x-1, y+1)) ++contador;//Arriba a la Izquiera
        return contador;
    }

    ////////////////////////////////////////////////////////////////
    //Descubre  todas las casillas adyacentes que no contengan minas
    ////////////////////////////////////////////////////////////////
    public static void DescubrirFloodFill(int x, int y, bool[,] visitado)
    {
        if (x >= 0 && y >= 0 && x < ancho && y < largo)
        {
            if (visitado[x, y])
                return;
            
            elementos[x, y].CargarTextura(MinasAdyacentes(x, y));

            //Revisa si está cerca de una mina
            if (MinasAdyacentes(x, y) > 0)
                return;

            //Bandera de visitado
            visitado[x, y] = true;

            //Recursión
            DescubrirFloodFill(x-1, y, visitado);
            DescubrirFloodFill(x + 1, y, visitado);
            DescubrirFloodFill(x, y - 1, visitado);
            DescubrirFloodFill(x, y + 1, visitado);
        }
        
    }

    public static bool EstaTerminado()
    {
        int numeroMinas = 8;
        int numeroCapturas=0;

        //Busca elementos que no sean minas
        foreach (Elemento elem in elementos)
        {
            if (elem.mina && elem.capturado)
            {
                numeroCapturas++;
            }
        }
        if (numeroCapturas == numeroMinas)
        {
            juegoTerminado = true;
            return true;
        }
        else
        {
            juegoTerminado = false;
            return false;
        }
    }

    public static bool EsMina(int x, int y, int[] PosicionesMinasX, int[]PosicionesMinasY)
    {
        bool minaEnCasilla = false;
        for (int i = 0; i < 8; i++)
        {
            if ((PosicionesMinasX[i] == x) && (PosicionesMinasY[i] == y))
            {
                minaEnCasilla = true;
                break;
            }
            else
            {
                minaEnCasilla = false;
            }
        }
        return minaEnCasilla;
    }
}

