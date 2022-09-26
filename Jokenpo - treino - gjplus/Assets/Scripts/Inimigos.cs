using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public int id;
    private int modo;

    string[] lista_Jogadas = new string[3];
    string[] jogada = new string[3];

    public Animator anim;
    public AudioClip attackAudio;
    public AudioClip takeDamageAudio;

    public static Inimigos Instance;
    private void Awake()
    {
        if (Inimigos.Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(Inimigos.Instance);
        }
        lista_Jogadas[0] = Jogadas.Instance.PrimeiraJogada(id);
        lista_Jogadas[1] = Jogadas.Instance.SegundoJogada(id);
        lista_Jogadas[2] = Jogadas.Instance.TerceiraJogada(id);
    }

    public void PlayTakeDamageAudio() {
        AudioManager.instance.PlaySound(takeDamageAudio);
    }

    public void PlayAttackAudio() {
        AudioManager.instance.PlaySound(attackAudio);
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

    public string PrimeiraPosicao() {
        return jogada[0];
    }
    public string SegundaPosicao()
    {
        return jogada[1];
    }
    public string TerceiraPosicao()
    {
        return jogada[2];
    }

}
