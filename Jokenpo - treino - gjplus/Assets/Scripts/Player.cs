using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string[] lista_Jogadas = new string[3];
    private void Update()
    {
        // pedra -> R (rock)
        // papel -> P (paper)
        // tesoura -> S (scissors)
        if (Input.GetKeyDown(KeyCode.R)) PressR();
        if (Input.GetKeyDown(KeyCode.P)) PressP();
        if (Input.GetKeyDown(KeyCode.S)) PressS();
    }
    void PressR() {
        for (int a = 0; a < 3; a++) {
            if (lista_Jogadas[a] == null) {
                lista_Jogadas[a] = "pedra";
                break;
            }
        }
    }
    void PressP()
    {
        for (int a = 0; a < 3; a++)
        {
            if (lista_Jogadas[a] == null)
            {
                lista_Jogadas[a] = "papel";
                break;
            }
        }
    }
    void PressS()
    {
        for (int a = 0; a < 3; a++)
        {
            if (lista_Jogadas[a] == null)
            {
                lista_Jogadas[a] = "tesoura";
                break;
            }
        }
    }
}
