using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public int id;
    private int modo;

    string[] lista_Jogadas = new string[3];
    string[] jogada = new string[3];

    void Start()
    {
        lista_Jogadas[0] = Jogadas.Instance.PrimeiraJogada(id);
        lista_Jogadas[1] = Jogadas.Instance.SegundoJogada(id);
        lista_Jogadas[2] = Jogadas.Instance.TerceiraJogada(id);
    }

    public void Sequencia() {

        modo = Random.Range(1, 4); // 3 modos diferentes 123 , 231 , 312

        if (modo == 1) {
            for (int a = 0; a < 3; a++) {
                jogada[a] = lista_Jogadas[a];   // 012 012
            }
        }
        if (modo == 2) {
            jogada[0] = lista_Jogadas[1];   // 120 012
            jogada[1] = lista_Jogadas[2];
            jogada[2] = lista_Jogadas[0];
        }
        if (modo == 3) {
            jogada[0] = lista_Jogadas[2];   // 201 012
            jogada[1] = lista_Jogadas[0];
            jogada[2] = lista_Jogadas[1];
        }
    }

    string PrimeiraPosicao() {
        return jogada[0];
    }
    string SegundaPosicao()
    {
        return jogada[1];
    }
    string TerceiraPosicao()
    {
        return jogada[3];
    }

}
