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
    }
}
