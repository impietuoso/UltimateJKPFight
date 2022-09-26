using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosVida : MonoBehaviour
{
    public float vida = 100;

    public static InimigosVida Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (vida <= 0) Ganhou();
    }

    private void Ganhou() {
        // GANHOU A PARTIDA
        // CHAMAR TELA DE GANHOU E PERGUNTAR SE QUER IR PARA A PRÓXIMA LUTA
        Inimigos.Instance.id += 1;
        vida = 100 + Inimigos.Instance.id * 10;
    }
}
