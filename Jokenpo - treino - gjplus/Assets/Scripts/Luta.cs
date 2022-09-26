using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luta : MonoBehaviour
{
    string[] jogadas_player = new string[3];
    string[] jogadas_inimigo = new string[3];
    int contador = 0;
    int idInimigo;
    float dano1 = 10, dano2 = 15, dano3 = 25;
    float multiplicador;
    public Slider slider_vida_player;
    public Slider slider_vida_inimigo;
    public Button fightButton;
    public Image jogada1, jogada2, jogada3;
    public Sprite interrogacao;

    private void Awake()
    {
        
    }
    private void Start()
    {

        idInimigo = Inimigos.Instance.id;
        Inimigos.Instance.Sequencia();
        EsconderJogada.Instance.PegarJogadas();

        if (idInimigo == 0 || idInimigo == 1 || idInimigo == 2) multiplicador = 1;
        if (idInimigo == 3 || idInimigo == 4 || idInimigo == 5) multiplicador = 1.5f;
        if (idInimigo == 6 || idInimigo == 7 || idInimigo == 8) multiplicador = 1.8f;
        if (idInimigo == 9) multiplicador = 2;

        dano1 *= multiplicador;
        dano2 *= multiplicador;
        dano3 *= multiplicador;
        // ANIMA��O COME�AR PARTIDA
    }
    public void Fight() {

        StartCoroutine(FightCorroutine());
        
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
        EsconderJogada.Instance.PegarJogadas();
        // ANIMA��O PROXIMO ROUD / COME�AR DE NOVO
    }

    private void DanoNoInimigo() {
        // ANIMA��O INIMIGO TOMANDO DANO
        contador++;
        if(contador == 1) InimigosVida.Instance.vida -= dano1;
        if(contador == 2) InimigosVida.Instance.vida -= dano2;
        if (contador == 3) InimigosVida.Instance.vida -= dano3;
        slider_vida_inimigo.value = InimigosVida.Instance.vida;
    }
    private void DanoNoPlayer() {
        // ANIMA��O PLAYER TOMANDO DANO
        contador++;
        if (contador == 1) PlayerVida.Instance.vida -= dano1;
        if (contador == 2) PlayerVida.Instance.vida -= dano2;
        if (contador == 3) PlayerVida.Instance.vida -= dano3;
        slider_vida_player.value = PlayerVida.Instance.vida;
    }
    private void Empate() {
        // ANIMA��O EMPATE
    }

    public IEnumerator FightCorroutine() {

        fightButton.interactable = false;

        jogadas_player[0] = Player.Instance.PrimeiraPosicao();
        jogadas_player[1] = Player.Instance.SegundaPosicao();
        jogadas_player[2] = Player.Instance.TerceiraPosicao();

        jogadas_inimigo[0] = Inimigos.Instance.PrimeiraPosicao();
        jogadas_inimigo[1] = Inimigos.Instance.SegundaPosicao();
        jogadas_inimigo[2] = Inimigos.Instance.TerceiraPosicao();

        // ANIMA��O DANO 1
        yield return new WaitForSeconds(1);
        if (jogadas_player[0] == "pedra") Pedra(0);
        if (jogadas_player[0] == "papel") Papel(0);
        if (jogadas_player[0] == "tesoura") Tesoura(0);
        // ANIMA��O DANO 2
        yield return new WaitForSeconds(1);
        if (jogadas_player[1] == "pedra") Pedra(1);
        if (jogadas_player[1] == "papel") Papel(1);
        if (jogadas_player[1] == "tesoura") Tesoura(1);
        // ANIMA��O DANO 3
        yield return new WaitForSeconds(1);
        if (jogadas_player[2] == "pedra") Pedra(2);
        if (jogadas_player[2] == "papel") Papel(2);
        if (jogadas_player[2] == "tesoura") Tesoura(2);
        
        yield return new WaitForSeconds(2);
        
        ResetUI();
        fightButton.interactable = true;
    }
    
    public void ResetUI() {
        ProximoRound();
        jogada1.sprite = interrogacao;
        jogada2.sprite = interrogacao;
        jogada3.sprite = interrogacao;
        Player.Instance.lista_Jogadas[0] = null;
        Player.Instance.lista_Jogadas[1] = null;
        Player.Instance.lista_Jogadas[2] = null;
    }
}
