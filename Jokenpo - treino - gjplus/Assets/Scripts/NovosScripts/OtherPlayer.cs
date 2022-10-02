using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayer : MonoBehaviour {

    public static OtherPlayer instance;

    public PhotonView pv;

    public string playerName;

    [Header("Components")]
    public Animator anim;

    [Header("Status")]
    public float maxLife;
    public float currentLife;
    public float playerDamage;

    [Header("Fight Variables")]
    public int[] attack = { 0, 0, 0 };
    public SpriteRenderer bubbleSprite;
    public bool resetHpAfterBattle;

    [Header("Audios")]
    public AudioClip hurtAudio;
    public AudioClip atkAudio;

    [Header("Multiplayer Variables")]
    public bool ready;
    public bool canStartFight;

    private void Start() {
        pv = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        MultiplayerCombat.instance.player2 = this.gameObject;
        if (pv.IsMine) {
            MultiplayerCombat.instance.actions2.SetActive(true);
            pv.RPC("PassMainPlayerInfo2", RpcTarget.All, PlayerPrefs.GetString("playerName"));
        }            
    }

    [PunRPC]
    public void PassMainPlayerInfo2(string name) {
        playerName = PlayerPrefs.GetString("playerName");
        MultiplayerCombat.instance.player2Name = name;
        MultiplayerCombat.instance.textName2.text = name;
        //Invoke("SetUiNames2", 0.2f);
    }

    [PunRPC]
    public void ReadyToFight(bool state) {
        canStartFight = state;
    }

    public void SetUiNames2() {
        if (pv.IsMine)
            MultiplayerCombat.instance.textName2.text = MultiplayerCombat.instance.player2Name;

    }
    //Called whem player take damage
    [PunRPC]
    public void TakeDamage() {
        //Play take damage animation and sound
        anim.Play("Damage");
        AudioManager.instance.PlaySound(hurtAudio);

        //Ajust current life and health bar
        currentLife -= MultiplayerCombat.instance.player2.GetComponent<OtherPlayer>().playerDamage;

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
                    pv.RPC("RockAttack", RpcTarget.All, currentAttack);
                    RockAttack(currentAttack);
                    break;
                case 2:
                    pv.RPC("PaperAttack", RpcTarget.All, currentAttack);
                    break;
                case 3:
                    pv.RPC("ScissorsAttack", RpcTarget.All, currentAttack);
                    break;
            }

            if (attack[0] != 0 && attack[1] != 0 && attack[2] != 0) {
                if (pv.IsMine)
                    MultiplayerCombat.instance.FightButton.interactable = true;
                pv.RPC("VerifyReadyState", RpcTarget.All);
            }
        }


    }

    [PunRPC]
    public void VerifyReadyState() {
        ready = true;
    }

    //Change current attack and image to Rock
    [PunRPC]
    public void RockAttack(int atk) {
        attack[atk] = 1;
        if (pv.IsMine)
            MultiplayerCombat.instance.Player2AttackSlot[atk].sprite = GameManager.instance.rock;
    }

    //Change current attack and image to Paper
    [PunRPC]
    public void PaperAttack(int atk) {
        attack[atk] = 2;
        if (pv.IsMine)
            MultiplayerCombat.instance.Player2AttackSlot[atk].sprite = GameManager.instance.paper;
    }

    //Change current attack and image to Scissors
    [PunRPC]
    public void ScissorsAttack(int atk) {
        attack[atk] = 3;
        if (pv.IsMine)
            MultiplayerCombat.instance.Player2AttackSlot[atk].sprite = GameManager.instance.scissors;
    }

    //Remove attack and icon from an attack slot
    public void CancelAttack(int atk) {
        if (MultiplayerCombat.instance.canCancelAttack) {
            attack[atk] = 0;
            MultiplayerCombat.instance.Player2AttackSlot[atk].sprite = GameManager.instance.mistery;
            ready = false;
        }
    }

    //Resets UI from current attacks and attack images
    [PunRPC]
    public void ResetAttack() {
        attack[0] = 0;
        attack[1] = 0;
        attack[2] = 0;

        MultiplayerCombat.instance.Player2AttackSlot[0].sprite = GameManager.instance.mistery;
        MultiplayerCombat.instance.Player2AttackSlot[1].sprite = GameManager.instance.mistery;
        MultiplayerCombat.instance.Player2AttackSlot[2].sprite = GameManager.instance.mistery;

        ReadyToFight(false);

        //If boolean active, recovers all health when finishing a battle
        if (resetHpAfterBattle && FightManager.instance.fighting) {
            currentLife = maxLife;
            MultiplayerCombat.instance.UpdateUI();
        }
    }
}
