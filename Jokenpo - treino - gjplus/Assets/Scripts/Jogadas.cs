using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogadas : MonoBehaviour
{
    private string[,] lista_Jogadas = new string[10, 3] { 
        {"pedra", "pedra", "pedra" }, 
        { "pedra", "papel", "tesoura" },
        { "papel", "papel", "tesoura" },
        { "tesoura", "pedra", "tesoura" },
        { "papel", "pedra", "tesoura" },
        { "tesoura", "papel", "tesoura" },
        { "pedra", "pedra", "tesoura" },
        { "pedra", "papel", "tesoura" },
        { "papel", "pedra", "tesoura" },
        { "tesoura", "pedra", "papel" },
    };

    public static Jogadas Instance;

    private void Awake()
    {
        Instance = this;
    }
    private string Jogadas_inimigos(int id, int pos) {
        string[] jogada = new string[3];
        for (int a = 0; a < 3; a++) { 
            jogada[a] = lista_Jogadas[id, a];
        }
        return jogada[pos];
    }

    public string PrimeiraJogada(int id) {
        string jogada = Jogadas_inimigos(id, 0);
        return jogada;
    }
    public string SegundoJogada(int id)
    {
        string jogada = Jogadas_inimigos(id, 1);
        return jogada; 
    }

    public string TerceiraJogada(int id)
    {
        string jogada = Jogadas_inimigos(id, 2);
        return jogada;
    }


}
