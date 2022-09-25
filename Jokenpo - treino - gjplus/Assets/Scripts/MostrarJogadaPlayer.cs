using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarJogadaPlayer : MonoBehaviour
{
    public Image jogada1, jogada2, jogada3;
    public Sprite pedra, papel, tesoura, interrogacao;

    public void PegarJogadas()
    {
        switch (Player.Instance.PrimeiraPosicao())
        {
            case "pedra":
                jogada1.sprite = pedra;
                break;
            case "tesoura":
                jogada1.sprite = tesoura;
                break;
            case "papel":
                jogada1.sprite = papel;
                break;
        }

        switch (Player.Instance.SegundaPosicao())
        {
            case "pedra":
                jogada2.sprite = pedra;
                break;
            case "tesoura":
                jogada2.sprite = tesoura;
                break;
            case "papel":
                jogada2.sprite = papel;
                break;
        }

        switch (Player.Instance.TerceiraPosicao())
        {
            case "pedra":
                jogada3.sprite = pedra;
                break;
            case "tesoura":
                jogada3.sprite = tesoura;
                break;
            case "papel":
                jogada3.sprite = papel;
                break;
        }
    }
}
