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
        if (Inimigos.Instance.id == 9) { 
            // TELA DE GANHAR O JOGO COMPLETO
            // VOLTAR PARA O MENU INICIAL
        }
        Inimigos.Instance.id += 1;
        // GANHOU A PARTIDA
        // CHAMAR TELA DE GANHOU E PERGUNTAR SE QUER IR PARA A PRÓXIMA LUTA
        // TER UM BOTÃO QUE CHAMA O RESET UI NO LUTA.CS
        vida = 100 + Inimigos.Instance.id * 10;
        PlayerVida.Instance.vida = 100;
    }
}
