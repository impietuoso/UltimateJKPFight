using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luta : MonoBehaviour
{
    string[] jogadas_player = new string[3];
    string[] jogadas_inimigo = new string[3];
    int contador = 0;
    int idInimigo = Inimigos.Instance.id;
    float dano1 = 10, dano2 = 15, dano3 = 25;
    float multiplicador;

    private void Start()
    {
        if (idInimigo == 0 || idInimigo == 1 || idInimigo == 2) multiplicador = 1;
        if (idInimigo == 3 || idInimigo == 4 || idInimigo == 5) multiplicador = 1.5f;
        if (idInimigo == 6 || idInimigo == 7 || idInimigo == 8) multiplicador = 1.8f;
        if (idInimigo == 9) multiplicador = 2;

        dano1 *= multiplicador;
        dano2 *= multiplicador;
        dano3 *= multiplicador;
    }
    public void Fight() {
        jogadas_player[0] = Player.Instance.PrimeiraPosicao();
        jogadas_player[1] = Player.Instance.SegundaPosicao();
        jogadas_player[2] = Player.Instance.TerceiraPosicao();

        jogadas_inimigo[0] = Inimigos.Instance.PrimeiraPosicao();
        jogadas_inimigo[1] = Inimigos.Instance.SegundaPosicao();
        jogadas_inimigo[2] = Inimigos.Instance.TerceiraPosicao();

        for (int a = 0; a < 3; a++) { 
            if (jogadas_player[a] == "pedra") Pedra(a);
            else if (jogadas_player[a] == "papel") Papel(a);
            else if (jogadas_player[a] == "tesoura") Tesoura(a);
        }

        ProximoRound();
    }

    private void Pedra(int turno) {
        if (jogadas_inimigo[turno] == "pedra") Empate();
        else if (jogadas_inimigo[turno] == "papel") DanoNoPlayer();
        else if (jogadas_inimigo[turno] == "tesoura") DanoNoInimigo();
    }
    private void Papel(int turno)
    {
        if (jogadas_inimigo[turno] == "pedra") DanoNoInimigo();
        else if (jogadas_inimigo[turno] == "papel") Empate();
        else if (jogadas_inimigo[turno] == "tesoura") DanoNoPlayer();
    }
    private void Tesoura(int turno)
    {
        if (jogadas_inimigo[turno] == "pedra") DanoNoPlayer();
        else if (jogadas_inimigo[turno] == "papel") DanoNoInimigo();
        else if (jogadas_inimigo[turno] == "tesoura") Empate();
     }

    private void ProximoRound() {
        contador = 0;
        Inimigos.Instance.Sequencia();
    }

    private void DanoNoInimigo() {
        contador++;
        if(contador == 1) InimigosVida.Instance.vida -= dano1;
        if(contador == 2) InimigosVida.Instance.vida -= dano2;
        if (contador == 3) InimigosVida.Instance.vida -= dano3;
    }
    private void DanoNoPlayer() {
        contador++;
        if (contador == 1) PlayerVida.Instance.vida -= dano1;
        if (contador == 2) PlayerVida.Instance.vida -= dano2;
        if (contador == 3) PlayerVida.Instance.vida -= dano3;
    }
    private void Empate() { }
}
