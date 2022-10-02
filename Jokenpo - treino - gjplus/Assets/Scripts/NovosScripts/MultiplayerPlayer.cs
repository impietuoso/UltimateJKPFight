using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MultiplayerPlayer : MonoBehaviour
{
    public static MultiplayerPlayer instance;

    public PhotonView pv;

    [Header("Components")]
    public Animator anim;

    [Header("Status")]
    public float maxLife;
    public float currentLife;
    public float playerDamage;

    [Header("Fight Variables")]
    public int[] attack;
    public Image[] attackSlot;
    public SpriteRenderer bubbleSprite;
    public bool resetHpAfterBattle;

    [Header("Audios")]
    public AudioClip hurtAudio;
    public AudioClip atkAudio;

    [Header("Multiplayer Variables")]
    public bool ready;

    private void Awake() {
        instance = this;
        pv = GetComponent<PhotonView>();
    }

    private void Start() {
        currentLife = maxLife;
        anim = GetComponent<Animator>();

        if (PhotonNetwork.IsMasterClient) {
            pv.RPC("Player1UI", RpcTarget.All);
        } else {
            pv.RPC("Player2UI", RpcTarget.All);
        }
    }

    [PunRPC]
    void Player1UI() {
        if (PhotonNetwork.IsMasterClient) {
            MultiplayerCombat.instance.player1Name = GameManager.instance.playerName;
            MultiplayerCombat.instance.actions1.SetActive(true);
        }
    }

    [PunRPC]
    void Player2UI() {
        if (!PhotonNetwork.IsMasterClient) {
            MultiplayerCombat.instance.player2Name = GameManager.instance.playerName;
            MultiplayerCombat.instance.actions2.SetActive(true);
        }
    }

    //Called whem player take damage
    public void TakeDamage() {
        //Play take damage animation and sound
        anim.Play("Damage");
        AudioManager.instance.PlaySound(hurtAudio);

        //Ajust current life and health bar
        currentLife -= MultiplayerCombat.instance.player1.GetComponent<MultiplayerPlayer>().playerDamage;

        MultiplayerCombat.instance.UpdateUI();

        MultiplayerCombat.instance.TurnEnding();
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

    //Configure the player's attacks in order according to the button pressed
    [PunRPC]
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
                ready = true;
                MultiplayerCombat.instance.FightButton.interactable = true;
            }

        }

    }

    //Change current attack and image to Rock
    [PunRPC]
    public void RockAttack(int atk) {
        attack[atk] = 1;
        attackSlot[atk].sprite = GameManager.instance.rock;
    }

    //Change current attack and image to Paper
    [PunRPC]
    public void PaperAttack(int atk) {
        attack[atk] = 2;
        attackSlot[atk].sprite = GameManager.instance.paper;
    }

    //Change current attack and image to Scissors
    [PunRPC]
    public void ScissorsAttack(int atk) {
        attack[atk] = 3;
        attackSlot[atk].sprite = GameManager.instance.scissors;
    }

    //Resets UI from current attacks and attack images
    [PunRPC]
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
            MultiplayerCombat.instance.UpdateUI();
        }
    }

}
