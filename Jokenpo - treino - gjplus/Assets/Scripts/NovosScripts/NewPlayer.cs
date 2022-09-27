using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    public static NewPlayer instance;

    public Animator anim;

    public float maxLife;
    public float currentLife;
    public Slider hpSlider;
    public float playerDamage;

    public int[] attack;

    public Image[] attackSlot;

    public SpriteRenderer bubbleSprite;

    public bool canCancelAttack;

    public bool resetHpAfterBattle;

    public Button FightButton;

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

    public void TakeDamage() {
        anim.Play("Damage");
        AudioManager.instance.PlaySound(hurtAudio);
        currentLife -= FightManager.instance.currentEnemy.enemyDamage;
        if (currentLife >= 0)
            hpSlider.value = currentLife;
        if (currentLife <= 0) {
            currentLife = 0;
            Death();
        }
    }

    public void Death() {
        FightManager.instance.fighting = false;
        GameManager.instance.ShowCanvasGroup(FightManager.instance.GameOverPanel);
        FightManager.instance.GameOverText.text = "You Loosed!";
    }

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

    public void SelectNextPlayerAttack(int atkType) {
        if(attack[0] == 0 || attack[1] == 0 || attack[2] == 0) {

            int currentAttack = 0;
            if (attack[0] == 0) {
                currentAttack = 0;
            } else if (attack[1] == 0) {
                currentAttack = 1;
            } else {
                currentAttack = 2;
            }

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

            if (attack[0] != 0 && attack[1] != 0 && attack[2] != 0) {
                FightButton.interactable = true;
            }

        } else if (attack[0] != 0 && attack[1] != 0 && attack[2] != 0) {

        }     
    }

    public void RockAttack(int atk) {
        attack[atk] = 1;
            attackSlot[atk].sprite = GameManager.instance.rock;
    }

    public void PaperAttack(int atk) {
        attack[atk] = 2;
            attackSlot[atk].sprite = GameManager.instance.paper;
    }

    public void ScissorsAttack(int atk) {
        attack[atk] = 3;
            attackSlot[atk].sprite = GameManager.instance.scissors;
    }

    public void CancelAttack(int atk) {
        if (canCancelAttack) {
            FightButton.interactable = false;
            attack[atk] = 0;
            attackSlot[atk].sprite = GameManager.instance.mistery;
        }
    }

    public void ResetAttack() {
        attack[0] = 0;
        attack[1] = 0;
        attack[2] = 0;

        attackSlot[0].sprite = GameManager.instance.mistery;
        attackSlot[1].sprite = GameManager.instance.mistery;
        attackSlot[2].sprite = GameManager.instance.mistery;

        if (resetHpAfterBattle && FightManager.instance.fighting) {
            currentLife = maxLife;
            hpSlider.value = maxLife;
        }
    }
}
