using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string[] lista_Jogadas = new string[3];
    public static Player Instance;
    public Animator anim;
    public AudioClip attackAudio;
    public AudioClip takeDamageAudio;

    public void PlayTakeDamageAudio() {
        AudioManager.instance.PlaySound(takeDamageAudio);
    }

    public void PlayAttackAudio() {
        AudioManager.instance.PlaySound(attackAudio);
    }

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start() {

        lista_Jogadas[0] = "";
        lista_Jogadas[1] = "";
        lista_Jogadas[2] = "";
    }

    private void Update()
    {
        // pedra -> R (rock)
        // papel -> P (paper)
        // tesoura -> S (scissors)
        if (Input.GetKeyDown(KeyCode.R)) PressR();
        if (Input.GetKeyDown(KeyCode.P)) PressP();
        if (Input.GetKeyDown(KeyCode.S)) PressS();
    }
    public void PressR() {
        for (int a = 0; a < 3; a++) {
            if (lista_Jogadas[a] == "") {
                lista_Jogadas[a] = "pedra";
                break;
            }
        }
    }
    public void PressP()
    {
        for (int a = 0; a < 3; a++)
        {
            if (lista_Jogadas[a] == "")
            {
                lista_Jogadas[a] = "papel";
                break;
            }
        }
    }

    public void PressS()
    {
        for (int a = 0; a < 3; a++)
        {
            if (lista_Jogadas[a] == "")
            {
                lista_Jogadas[a] = "tesoura";
                break;
            }
        }
    }

    public string PrimeiraPosicao()
    {
        return lista_Jogadas[0];
    }
    public string SegundaPosicao()
    {
        return lista_Jogadas[1];
    }
    public string TerceiraPosicao()
    {
        return lista_Jogadas[2];
    }
}
