using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public int id;

    string[] lista_Jogadas = new string[3];
    void Start()
    {
        lista_Jogadas[0] = Jogadas.Instance.PrimeiraJogada(id);
        lista_Jogadas[1] = Jogadas.Instance.SegundoJogada(id);
        lista_Jogadas[2] = Jogadas.Instance.TerceiraJogada(id);
    }

   
}
