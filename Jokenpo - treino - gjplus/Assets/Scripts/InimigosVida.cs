using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InimigosVida : MonoBehaviour
{
    public float vida = 100;

    public CanvasGroup nextEnemyPanel;

    public bool activeEnemy;

    public TextMeshProUGUI enemyNumbers;
    public int enemyCount;

    public TextMeshProUGUI gameOverText;
    public CanvasGroup gameOverPanel;

    public static InimigosVida Instance;
    private void Awake()
    {
        Instance = this;
        enemyCount = 9;
    }

    private void Update()
    {
        if (vida <= 0 && activeEnemy) Ganhou();
        enemyNumbers.text = "Remaining " + enemyCount;
    }

    private void Ganhou() {
        activeEnemy = false;
        enemyCount--;
        SpriteRenderer spt = GetComponent<SpriteRenderer>();
        //spt.color = new Vector4(1,1,1,0);
        if (Inimigos.Instance.id == 9) {
            GameManager.instance.ShowCanvasGroup(gameOverPanel);
            gameOverText.text = "You have WON";
        } else {
            Inimigos.Instance.id += 1;
            GameManager.instance.ShowCanvasGroup(nextEnemyPanel);
        }
    }

    public void PlayerLose() {
        GameManager.instance.ShowCanvasGroup(gameOverPanel);
        gameOverText.text = "You have LOSE";

    }

    public void NewEnemy() {
        vida = 100 + Inimigos.Instance.id * 10;
        PlayerVida.Instance.vida = 100;
        activeEnemy = true;
        SpriteRenderer spt = GetComponent<SpriteRenderer>();
        //spt.color = new Vector4(1, 1, 1, 1);
    }
}
