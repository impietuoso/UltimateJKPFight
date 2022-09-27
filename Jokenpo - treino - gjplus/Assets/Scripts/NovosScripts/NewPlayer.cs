using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    public static NewPlayer instance;

    [Header("Components")]
    public Animator anim;

    [Header("Status")]
    public float maxLife;
    public float currentLife;
    public Slider hpSlider;
    public float playerDamage;

    [Header("Fight Variables")]
    public int[] attack;
    public Image[] attackSlot;
    public SpriteRenderer bubbleSprite;
    public bool canCancelAttack;
    public bool resetHpAfterBattle;
    public Button FightButton;

    [Header("Audios")]
    public AudioClip hurtAudio;
    public AudioClip atkAudio;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        currentLife = maxLife;
        hpSlider.maxValue = maxLife;
        hpSlider.value = maxLife;
        anim = GetComponent<Animator>();
    }

    //Called whem player take damage
    public void TakeDamage() {
        //Play take damage animation and sound
        anim.Play("Damage");
        AudioManager.instance.PlaySound(hurtAudio);

        //Ajust current life and health bar
        currentLife -= FightManager.instance.currentEnemy.enemyDamage;
        if (currentLife >= 0)
            hpSlider.value = currentLife;

        if (currentLife <= 0) {
            currentLife = 0;
            Death();
        }
    }

    //Stop fighting and call Game Over
    public void Death() {
        FightManager.instance.fighting = false;
        GameManager.instance.ShowCanvasGroup(FightManager.instance.GameOverPanel);
        FightManager.instance.GameOverText.text = "You Loosed!";
    }

    //Play the attack animation accordingly to the current attack
    public void AttackAnimation(int atk) {
        switch (atk) {
            case 1:
                anim.Play("Rock");
                bubbleSprite.sprite = GameManager.instance.rock;
                break;
            case 2:
                anim.Play("Paper");
                bubbleSprite.sprite = GameManager.instance.paper;
                break;
            case 3:
                anim.Play("Scissors");
                bubbleSprite.sprite = GameManager.instance.scissors;
                break;
        }
    }

    //Configure the player's attacks in order according to the button pressed 
    public void SelectNextPlayerAttack(int atkType) {
        //Checking for any unselected attacks
        if (attack[0] == 0 || attack[1] == 0 || attack[2] == 0) {

            //Fill attacks in slot order
            int currentAttack = 0;
            if (attack[0] == 0) {
                currentAttack = 0;
            } else if (attack[1] == 0) {
                currentAttack = 1;
            } else {
                currentAttack = 2;
            }

            //Swapping image and current attack according to the attack value of the pressed button
            switch (atkType) {
                case 1:
                    RockAttack(currentAttack);
                    break;
                case 2:
                    PaperAttack(currentAttack);
                    break;
                case 3:
                    ScissorsAttack(currentAttack);
                    break;
            }

            //If all attacks were selected turn on fight button
            if (attack[0] != 0 && attack[1] != 0 && attack[2] != 0) {
                FightButton.interactable = true;
            }

        }
        
    }

    //Change current attack and image to Rock
    public void RockAttack(int atk) {
        attack[atk] = 1;
            attackSlot[atk].sprite = GameManager.instance.rock;
    }

    //Change current attack and image to Paper
    public void PaperAttack(int atk) {
        attack[atk] = 2;
            attackSlot[atk].sprite = GameManager.instance.paper;
    }

    //Change current attack and image to Scissors
    public void ScissorsAttack(int atk) {
        attack[atk] = 3;
            attackSlot[atk].sprite = GameManager.instance.scissors;
    }

    //Remove attack and icon from an attack slot
    public void CancelAttack(int atk) {
        if (canCancelAttack) {
            FightButton.interactable = false;
            attack[atk] = 0;
            attackSlot[atk].sprite = GameManager.instance.mistery;
        }
    }

    //Resets UI from current attacks and attack images
    public void ResetAttack() {
        attack[0] = 0;
        attack[1] = 0;
        attack[2] = 0;

        attackSlot[0].sprite = GameManager.instance.mistery;
        attackSlot[1].sprite = GameManager.instance.mistery;
        attackSlot[2].sprite = GameManager.instance.mistery;

        //If boolean active, recovers all health when finishing a battle
        if (resetHpAfterBattle && FightManager.instance.fighting) {
            currentLife = maxLife;
            hpSlider.value = maxLife;
        }
    }
}
