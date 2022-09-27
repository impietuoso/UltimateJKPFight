using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;

    public NewEnemy currentEnemy;

    public Slider enemyHpSlider;

    public Transform enemySpawnPoint;

    public List<GameObject> listOfEnemies = new List<GameObject>();

    public Image[] enemyAttackSlot;

    public bool fighting;

    public TextMeshProUGUI remainingEnemiesText;

    public CanvasGroup GameOverPanel;
    public TextMeshProUGUI GameOverText;

    public Animator goAni;
    public AudioClip goAudio;
    public AudioClip laughAudio;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Invoke("Go", 1f);
        currentEnemy = listOfEnemies.First().GetComponent<NewEnemy>();
        listOfEnemies.Remove(listOfEnemies.First());
        remainingEnemiesText.text = listOfEnemies.Count + " Enemies Left";
    }

    public void Go() {
        if (fighting) {
            AudioManager.instance.PlaySound(goAudio);
            goAni.Play("Go");
        }
    }

    public void GenerateEnemy() {
        if(listOfEnemies.Count > 0) {
            fighting = true;
            GameObject e = Instantiate(listOfEnemies.First(), enemySpawnPoint.position, Quaternion.identity, enemySpawnPoint);
            currentEnemy = e.GetComponent<NewEnemy>();
            listOfEnemies.Remove(listOfEnemies.First());
            remainingEnemiesText.text = listOfEnemies.Count + " Enemies Left";
            if (NewPlayer.instance.currentLife + 20 <= NewPlayer.instance.maxLife)
                NewPlayer.instance.currentLife += 20;
            NewPlayer.instance.hpSlider.value = NewPlayer.instance.currentLife;
        } else {
            GameManager.instance.ShowCanvasGroup(GameOverPanel);
            GameOverText.text = "You have Won!";
        }
    }

    public void Fight() {
        if (fighting)
            StartCoroutine(FightCoroutine());
    }

    public IEnumerator FightCoroutine() {

        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        NewPlayer.instance.FightButton.interactable = false;
        NewPlayer.instance.canCancelAttack = false;

        int playerAttack = NewPlayer.instance.attack[0];
        int enemyAttack = currentEnemy.attack[0];
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Calculando quem leva dano
        if (playerAttack == 1 && enemyAttack == 1) Tie();
        else if (playerAttack == 1 && enemyAttack == 2) Defeat();
        else if (playerAttack == 1 && enemyAttack == 3) Win();
        else if (playerAttack == 2 && enemyAttack == 2) Tie();
        else if (playerAttack == 2 && enemyAttack == 3) Defeat();
        else if (playerAttack == 2 && enemyAttack == 1) Win();
        else if (playerAttack == 3 && enemyAttack == 3) Tie();
        else if (playerAttack == 3 && enemyAttack == 1) Defeat();
        else if (playerAttack == 3 && enemyAttack == 2) Win();
        yield return new WaitForSeconds(1f);

        //Verificar se ganhei
        if (!fighting) {
            GenerateEnemy();
            NewPlayer.instance.canCancelAttack = true;
            NewPlayer.instance.ResetAttack();
            yield break;
        }

        //Animações de Ataque
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        playerAttack = NewPlayer.instance.attack[1];
        enemyAttack = currentEnemy.attack[1];
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Calculando quem leva dano
        if (playerAttack == 1 && enemyAttack == 1) Tie();
        else if (playerAttack == 1 && enemyAttack == 2) Defeat();
        else if (playerAttack == 1 && enemyAttack == 3) Win();
        else if (playerAttack == 2 && enemyAttack == 2) Tie();
        else if (playerAttack == 2 && enemyAttack == 3) Defeat();
        else if (playerAttack == 2 && enemyAttack == 1) Win();
        else if (playerAttack == 3 && enemyAttack == 3) Tie();
        else if (playerAttack == 3 && enemyAttack == 1) Defeat();
        else if (playerAttack == 3 && enemyAttack == 2) Win();
        yield return new WaitForSeconds(1f);

        //Verificar se ganhei
        if (!fighting) {
            GenerateEnemy();
            NewPlayer.instance.canCancelAttack = true;
            NewPlayer.instance.ResetAttack();
            yield break;
        }

        //Animações de Ataque
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        playerAttack = NewPlayer.instance.attack[2];
        enemyAttack = currentEnemy.attack[2];
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Calculando quem leva dano
        if (playerAttack == 1 && enemyAttack == 1) Tie();
        else if (playerAttack == 1 && enemyAttack == 2) Defeat();
        else if (playerAttack == 1 && enemyAttack == 3) Win();
        else if (playerAttack == 2 && enemyAttack == 2) Tie();
        else if (playerAttack == 2 && enemyAttack == 3) Defeat();
        else if (playerAttack == 2 && enemyAttack == 1) Win();
        else if (playerAttack == 3 && enemyAttack == 3) Tie();
        else if (playerAttack == 3 && enemyAttack == 1) Defeat();
        else if (playerAttack == 3 && enemyAttack == 2) Win();
        yield return new WaitForSeconds(1f);

        //Verificar se ganhei
        if (!fighting) {
            GenerateEnemy();
        }

        currentEnemy.SelectEnemyAttacks();
        NewPlayer.instance.canCancelAttack = true;
        NewPlayer.instance.ResetAttack();
        Go();
    }

    void Win() {
        currentEnemy.TakeDamage();
    }

    void Defeat() {
        NewPlayer.instance.TakeDamage();
        AudioManager.instance.PlaySound(laughAudio);
    }

    void Tie() {
        AudioManager.instance.PlaySound(laughAudio);
    }
}
