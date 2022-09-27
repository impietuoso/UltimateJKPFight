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

    //Call popup GO with sound
    public void Go() {
        if (fighting) {
            AudioManager.instance.PlaySound(goAudio);
            goAni.Play("Go");
        }
    }

    //Instantiate new enemy and begin fight
    public void GenerateEnemy() {
        if (listOfEnemies.Count > 0) {
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

    //Call fight comparison
    public void Fight() {
        if (fighting)
            StartCoroutine(FightCoroutine());
    }

    //Disable fight button and disable cancel attacks
    //Start Turns
    //Each Turn:
    //Attack Sound > Get Attacks > Attack Animations >  Wait > Compare Attacks > Wait
    //End of Each Turn: If enemy is death stop fighting
    public IEnumerator FightCoroutine() {

        yield return new WaitForSeconds(0.2f);
        //Sound
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        //Disable Buttons
        NewPlayer.instance.FightButton.interactable = false;
        NewPlayer.instance.canCancelAttack = false;
        //Get Attacks
        int playerAttack = NewPlayer.instance.attack[0];
        int enemyAttack = currentEnemy.attack[0];
        //Attack Animations
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
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

        //If Enemy is dead Stop Fight
        if (!fighting) {
            GenerateEnemy();
            NewPlayer.instance.canCancelAttack = true;
            NewPlayer.instance.ResetAttack();
            yield break;
        }

        //Sound
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        //Get Attacks
        playerAttack = NewPlayer.instance.attack[1];
        enemyAttack = currentEnemy.attack[1];
        //Attack Animations
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
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

        //If Enemy is dead Stop Fight
        if (!fighting) {
            GenerateEnemy();
            NewPlayer.instance.canCancelAttack = true;
            NewPlayer.instance.ResetAttack();
            yield break;
        }

        //Sound
        AudioManager.instance.PlaySound(NewPlayer.instance.atkAudio);
        AudioManager.instance.PlaySound(currentEnemy.atkAudio);
        //Get Attacks
        playerAttack = NewPlayer.instance.attack[2];
        enemyAttack = currentEnemy.attack[2];
        //Attack Animations
        NewPlayer.instance.AttackAnimation(playerAttack);
        currentEnemy.AttackAnimation(enemyAttack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
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

        //If Enemy is dead Call Next Enemy
        if (!fighting) {
            GenerateEnemy();
        }

        //Ending Turns Setup next Turns Begin
        currentEnemy.SelectEnemyAttacks();
        NewPlayer.instance.canCancelAttack = true;
        NewPlayer.instance.ResetAttack();
        Go();
    }

    //If win an combat do damage in enemy
    void Win() {
        currentEnemy.TakeDamage();
    }

    //If lose an combat take damage from enemy and play sound
    void Defeat() {
        NewPlayer.instance.TakeDamage();
        AudioManager.instance.PlaySound(laughAudio);
    }

    //If the result is an tie play sound
    void Tie() {
        AudioManager.instance.PlaySound(laughAudio);
    }
}
