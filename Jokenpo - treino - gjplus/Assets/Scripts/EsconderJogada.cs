using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EsconderJogada : MonoBehaviour
{
    int idInimigo;

    public Image jogada1,jogada2,jogada3;
    public Sprite pedra,papel,tesoura,interrogacao;

    public static EsconderJogada Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        idInimigo = Inimigos.Instance.id;
    }

    void Esconder() {
        switch (idInimigo)
        {
            case 1:
                jogada3.sprite = interrogacao;
                break;
            case 2:
                jogada3.sprite = interrogacao;
                break;
            case 3:
                jogada2.sprite = interrogacao;
                break;
            case 4:
                jogada2.sprite = interrogacao;
                break;
            case 5:
                jogada1.sprite = interrogacao;
                break;
            case 6:
                jogada1.sprite = interrogacao;
                break;
            case 7:
                jogada2.sprite = interrogacao;
                jogada3.sprite = interrogacao;
                break;
            case 8:
                jogada1.sprite = interrogacao;
                jogada2.sprite = interrogacao;
                break;
            case 9:
                jogada1.sprite = interrogacao;
                jogada2.sprite = interrogacao;
                jogada3.sprite = interrogacao;
                break;
        }
    }

    public void PegarJogadas() {
        switch (Inimigos.Instance.PrimeiraPosicao())
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

        switch (Inimigos.Instance.SegundaPosicao())
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

        switch (Inimigos.Instance.TerceiraPosicao())
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

        Esconder();
    }

}
