using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;

    [Header("Status")]
    public float maxLife;
    float currentLife;
    public float enemyDamage;

    [Header("Fight Variables")]
    public int[] patter;
    public int[] attack;
    public bool[] hiddenAttack;
    public SpriteRenderer bubbleSprite;

    [Header("Audios")]
    public AudioClip hurtAudio;
    public AudioClip atkAudio;

    private void Start() {
        currentLife = maxLife;
        FightManager.instance.enemyHpSlider.maxValue = maxLife;
        FightManager.instance.enemyHpSlider.value = maxLife;
        SelectEnemyAttacks();
        anim.GetComponent<Animator>();
    }

    //Play the attack animation accordingly to the current attack
    public void AttackAnimation(int atk) {
        switch (atk) {
            case 1:
                anim.Play("Rock");
                bubbleSprite.sprite = GameManager.instance.rockG;
                break;
            case 2:
                anim.Play("Paper");
                bubbleSprite.sprite = GameManager.instance.paperG;
                break;
            case 3:
                anim.Play("Scissors");
                bubbleSprite.sprite = GameManager.instance.scissorsG;
                break;
        }
    }

    //Randomize order from attack order
    public void SelectEnemyAttacks() {
        int change = Random.Range(1,90);
        if(change > 0 && change < 31) {
            SequencyA();
        } else if (change > 31 && change < 61) {
            SequencyB();
        } else {
            SequencyC();
        }
    }

    //Attack order 1
    void SequencyA() {
        attack[0] = patter[0];
        attack[1] = patter[1];
        attack[2] = patter[2];

        SetupUIAttack();
    }

    //Attack order 2
    void SequencyB() {
        attack[0] = patter[1];
        attack[1] = patter[2];
        attack[2] = patter[0];

        SetupUIAttack();
    }

    //Attack order 3
    void SequencyC() {
        attack[0] = patter[2];
        attack[1] = patter[0];
        attack[2] = patter[1];

        SetupUIAttack();
    }

    //Setup attack order in UI
    void SetupUIAttack() {

        //Set first Attack
        if (!hiddenAttack[0]) {
            if (attack[0] == 1)
                FightManager.instance.enemyAttackSlot[0].sprite = GameManager.instance.rock;
            else if (attack[0] == 2)
                FightManager.instance.enemyAttackSlot[0].sprite = GameManager.instance.paper;
            else if (attack[0] == 3)
                FightManager.instance.enemyAttackSlot[0].sprite = GameManager.instance.scissors;
        } else FightManager.instance.enemyAttackSlot[0].sprite = GameManager.instance.mistery;

        //Set second Attack
        if (!hiddenAttack[1]) {
            if (attack[1] == 1)
                FightManager.instance.enemyAttackSlot[1].sprite = GameManager.instance.rock;
            else if (attack[1] == 2)
                FightManager.instance.enemyAttackSlot[1].sprite = GameManager.instance.paper;
            else if (attack[1] == 3)
                FightManager.instance.enemyAttackSlot[1].sprite = GameManager.instance.scissors;
        } else FightManager.instance.enemyAttackSlot[1].sprite = GameManager.instance.mistery;

        //Set third Attack
        if (!hiddenAttack[2]) {
            if (attack[2] == 1)
                FightManager.instance.enemyAttackSlot[2].sprite = GameManager.instance.rock;
            else if (attack[2] == 2)
                FightManager.instance.enemyAttackSlot[2].sprite = GameManager.instance.paper;
            else if (attack[2] == 3)
                FightManager.instance.enemyAttackSlot[2].sprite = GameManager.instance.scissors;
        } else FightManager.instance.enemyAttackSlot[2].sprite = GameManager.instance.mistery;
    }

    //Called whem enemy take damage
    public void TakeDamage() {
        //Play take damage animation and sound
        anim.Play("Damage");
        AudioManager.instance.PlaySound(hurtAudio);

        //Ajust current life and health bar
        currentLife -= NewPlayer.instance.playerDamage;
        if (currentLife >= 0)
            FightManager.instance.enemyHpSlider.value = currentLife;
        
        if (currentLife <= 0) {
            currentLife = 0;
            Death();
        }
    }

    //Stop fighting and Destroy current enemy
    public void Death() {
        FightManager.instance.fighting = false;
        Destroy(this.gameObject);
    }

    //Reset Attack Order
    public void ResetAttackUI() {
        FightManager.instance.enemyAttackSlot[0].sprite = GameManager.instance.mistery;
        FightManager.instance.enemyAttackSlot[1].sprite = GameManager.instance.mistery;
        FightManager.instance.enemyAttackSlot[2].sprite = GameManager.instance.mistery;
    }

}
