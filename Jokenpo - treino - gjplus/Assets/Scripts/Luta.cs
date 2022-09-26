using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luta : MonoBehaviour {
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

    public AudioClip goAudio;
    public AudioClip selectAudio;
    public Animator goIcon;

    private void Start() {
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
        Invoke("Go", 1f);
    }

    public void ActiveFightButton() {
        if (Player.Instance.lista_Jogadas[0] != "" && Player.Instance.lista_Jogadas[1] != "" && Player.Instance.lista_Jogadas[2] != "")
            fightButton.interactable = true;
    }

    void Go() {
        if (InimigosVida.Instance.activeEnemy) {
            AudioManager.instance.PlaySound(goAudio);
            goIcon.Play("Go");
        }
    }

    public void Fight() {
        if (Player.Instance.lista_Jogadas[0] != "" && Player.Instance.lista_Jogadas[1] != "" && Player.Instance.lista_Jogadas[2] != "") {
            StartCoroutine(FightCorroutine());
        }
    }

    private void Pedra(int turno) {
        Player.Instance.ShowAttackBubble(true, 0);
        if (jogadas_inimigo[turno] == "pedra") Empate();
        else if (jogadas_inimigo[turno] == "papel") DanoNoPlayer();
        else if (jogadas_inimigo[turno] == "tesoura") DanoNoInimigo();
        AjustEnemyBubbleIcon(turno);
    }

    private void Papel(int turno) {
        Player.Instance.ShowAttackBubble(true, 1);
        if (jogadas_inimigo[turno] == "pedra") DanoNoInimigo();
        else if (jogadas_inimigo[turno] == "papel") Empate();
        else if (jogadas_inimigo[turno] == "tesoura") DanoNoPlayer();
        AjustEnemyBubbleIcon(turno);
    }

    private void Tesoura(int turno) {
        Player.Instance.ShowAttackBubble(true, 2);
        if (jogadas_inimigo[turno] == "pedra") DanoNoPlayer();
        else if (jogadas_inimigo[turno] == "papel") DanoNoInimigo();
        else if (jogadas_inimigo[turno] == "tesoura") Empate();
        AjustEnemyBubbleIcon(turno);
    }

    void AjustEnemyBubbleIcon(int turno) {
        if (jogadas_inimigo[turno] == "pedra") Inimigos.Instance.ShowAttackBubble(true, 0);
        else if (jogadas_inimigo[turno] == "papel") Inimigos.Instance.ShowAttackBubble(true, 1);
        else if (jogadas_inimigo[turno] == "tesoura") Inimigos.Instance.ShowAttackBubble(true, 2);
    }

    private void ProximoRound() {
        contador = 0;
        Inimigos.Instance.Sequencia();
        EsconderJogada.Instance.PegarJogadas();
        // ANIMAÇÃO PROXIMO ROUD / COMEÇAR DE NOVO
        Go();
    }

    private void DanoNoInimigo() {
        contador++;
        if (contador == 1) InimigosVida.Instance.vida -= dano1;
        if (contador == 2) InimigosVida.Instance.vida -= dano2;
        if (contador == 3) InimigosVida.Instance.vida -= dano3;
        slider_vida_inimigo.value = InimigosVida.Instance.vida;
        Invoke("EnemyDamageAnim", 0.2f);
    }

    void PlayerDamageAnim() {
        Player.Instance.anim.Play("Damage");
    }

    void EnemyDamageAnim() {
        Inimigos.Instance.anim.Play("Damage");
    }

    private void DanoNoPlayer() {
        contador++;
        if (contador == 1) PlayerVida.Instance.vida -= dano1;
        if (contador == 2) PlayerVida.Instance.vida -= dano2;
        if (contador == 3) PlayerVida.Instance.vida -= dano3;
        slider_vida_player.value = PlayerVida.Instance.vida;
        AudioManager.instance.PlaySound(selectAudio);
        Invoke("PlayerDamageAnim", 0.2f);
    }
    private void Empate() {
        AudioManager.instance.PlaySound(selectAudio);
    }

    public IEnumerator FightCorroutine() {

        fightButton.interactable = false;

        jogadas_player[0] = Player.Instance.PrimeiraPosicao();
        jogadas_player[1] = Player.Instance.SegundaPosicao();
        jogadas_player[2] = Player.Instance.TerceiraPosicao();

        jogadas_inimigo[0] = Inimigos.Instance.PrimeiraPosicao();
        jogadas_inimigo[1] = Inimigos.Instance.SegundaPosicao();
        jogadas_inimigo[2] = Inimigos.Instance.TerceiraPosicao();

        Player.Instance.anim.Play("Attack");
        Inimigos.Instance.anim.Play("Attack");
        yield return new WaitForSeconds(0.3f);
        if (jogadas_player[0] == "pedra") Pedra(0);
        if (jogadas_player[0] == "papel") Papel(0);
        if (jogadas_player[0] == "tesoura") Tesoura(0);
        Player.Instance.anim.Play(jogadas_player[0]);
        Inimigos.Instance.anim.Play(jogadas_inimigo[0]);
        yield return new WaitForSeconds(1f);
        Player.Instance.ShowAttackBubble(false, 0);
        Inimigos.Instance.ShowAttackBubble(false, 0);
        Player.Instance.anim.Play("Attack");
        Inimigos.Instance.anim.Play("Attack");
        yield return new WaitForSeconds(0.3f);
        if (jogadas_player[1] == "pedra") Pedra(1);
        if (jogadas_player[1] == "papel") Papel(1);
        if (jogadas_player[1] == "tesoura") Tesoura(1);
        Player.Instance.anim.Play(jogadas_player[1]);
        Inimigos.Instance.anim.Play(jogadas_inimigo[1]);
        yield return new WaitForSeconds(1f);
        Player.Instance.ShowAttackBubble(false, 0);
        Inimigos.Instance.ShowAttackBubble(false, 0);
        Player.Instance.anim.Play("Attack");
        Inimigos.Instance.anim.Play("Attack");
        yield return new WaitForSeconds(0.3f);
        if (jogadas_player[2] == "pedra") Pedra(2);
        if (jogadas_player[2] == "papel") Papel(2);
        if (jogadas_player[2] == "tesoura") Tesoura(2);
        Player.Instance.anim.Play(jogadas_player[2]);
        Inimigos.Instance.anim.Play(jogadas_inimigo[2]);
        yield return new WaitForSeconds(1f);
        Player.Instance.ShowAttackBubble(false, 0);
        Inimigos.Instance.ShowAttackBubble(false, 0);
        Player.Instance.anim.Play("Idle");
        Inimigos.Instance.anim.Play("Idle");
        ResetUI();
        ProximoRound();
    }

    public void ResetUI() {
        jogada1.sprite = interrogacao;
        jogada2.sprite = interrogacao;
        jogada3.sprite = interrogacao;
        Player.Instance.lista_Jogadas[0] = "";
        Player.Instance.lista_Jogadas[1] = "";
        Player.Instance.lista_Jogadas[2] = "";
    }

    public void NextEnemy() {
        if (InimigosVida.Instance.vida > slider_vida_inimigo.maxValue)
            slider_vida_inimigo.maxValue = InimigosVida.Instance.vida;
        slider_vida_inimigo.value = InimigosVida.Instance.vida;
        Go();
    }

}
