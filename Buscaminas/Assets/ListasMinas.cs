using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListasMinas /*: MonoBehaviour*/
{
    public int[] PosicionesMinasX = new int[8];
    public int[] PosicionesMinasY = new int[8];
    public void SetPosicionesX(int[] posiciones)
    {
        PosicionesMinasX = posiciones;
    }
    public void SetPosicionesY(int[] posiciones)
    {
        PosicionesMinasY = posiciones;
    }
}
