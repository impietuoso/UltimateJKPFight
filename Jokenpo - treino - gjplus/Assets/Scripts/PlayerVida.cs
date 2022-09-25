using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    public int vida = 100;

    public static PlayerVida Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (vida <= 0) Perdeu();
    }

    private void Perdeu() { }
}