using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerCombat : MonoBehaviour
{
    public static MultiplayerCombat instance;

    public Transform player1Spawn;
    public Transform player2Spawn;

    public GameObject player1;
    public GameObject player2;

    public Slider Player1HpSlider;
    public Slider Player2HpSlider;

    public Image[] Player1AttackSlot;
    public Image[] Player2AttackSlot;

    public bool fighting;

    public CanvasGroup GameOverPanel;
    public TextMeshProUGUI GameOverText;

    public Animator goAni;
    public AudioClip goAudio;
    public AudioClip laughAudio;

    public Button FightButton;
    public bool canCancelAttack;

    public TextMeshProUGUI textName1;
    public TextMeshProUGUI textName2;
    public string player1Name;
    public string player2Name;
    public GameObject actions1;
    public GameObject actions2;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Invoke("Go", 1f);
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate("Player1", player1Spawn.position, Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate("Player2", player2Spawn.position, Quaternion.identity);
        }
        //Player1HpSlider.maxValue = player1.GetComponent<MultiplayerPlayer>().maxLife;
        //Player1HpSlider.value = player1.GetComponent<MultiplayerPlayer>().currentLife;
        //Player2HpSlider.maxValue = player2.GetComponent<MultiplayerPlayer>().maxLife;
        //Player2HpSlider.value = player2.GetComponent<MultiplayerPlayer>().currentLife;
        //Invoke("UpdateNames", 2f);
    }

    public void Player1Attack(int atk) {
        player1.GetComponent<MainPlayer>().SelectNextPlayerAttack(atk);
    }

    public void Player2Attack(int atk) {
        player2.GetComponent<OtherPlayer>().SelectNextPlayerAttack(atk);
    }

    //Call popup GO with sound
    public void Go() {
        if (fighting) {
            AudioManager.instance.PlaySound(goAudio);
            goAni.Play("Go");
        }
    }

    [PunRPC]
    public void Ready() {
        if (fighting) {
            //Disable Buttons
            FightButton.interactable = false;
            canCancelAttack = false;
            if (PhotonNetwork.IsMasterClient && player1.GetComponent<MainPlayer>().ready == true) {
                player1.GetComponent<MainPlayer>().pv.RPC("ReadyToFight", RpcTarget.All, true);
            } else if (!PhotonNetwork.IsMasterClient && player2.GetComponent<OtherPlayer>().ready == true){
                player2.GetComponent<OtherPlayer>().pv.RPC("ReadyToFight", RpcTarget.All, true);
            }
            
            Fight();
        }
        
    }

    //Call fight comparison
    [PunRPC]
    public void Fight() {
        if (fighting)
            StartCoroutine(FightCoroutine());
    }

    [PunRPC]
    public IEnumerator FightCoroutine() {

        yield return new WaitUntil(()=>player1.GetComponent<MainPlayer>().canStartFight && player2.GetComponent<OtherPlayer>().canStartFight);

        yield return new WaitForSeconds(0.2f);
        //Sound
        AudioManager.instance.PlaySound(player1.GetComponent<MainPlayer>().atkAudio);
        AudioManager.instance.PlaySound(player2.GetComponent<OtherPlayer>().atkAudio);
        //Get Attacks
        int player1Attack = player1.GetComponent<MainPlayer>().attack[0];
        int player2Attack = player2.GetComponent<OtherPlayer>().attack[0];
        //Attack Animations
        player1.GetComponent<MainPlayer>().AttackAnimation(player1Attack);
        player2.GetComponent<OtherPlayer>().AttackAnimation(player2Attack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
        if (player1Attack == 1 && player2Attack == 1) Tie();
        else if (player1Attack == 1 && player2Attack == 2) Defeat();
        else if (player1Attack == 1 && player2Attack == 3) Win();
        else if (player1Attack == 2 && player2Attack == 2) Tie();
        else if (player1Attack == 2 && player2Attack == 3) Defeat();
        else if (player1Attack == 2 && player2Attack == 1) Win();
        else if (player1Attack == 3 && player2Attack == 3) Tie();
        else if (player1Attack == 3 && player2Attack == 1) Defeat();
        else if (player1Attack == 3 && player2Attack == 2) Win();
        yield return new WaitForSeconds(1f);

        //If Enemy is dead Stop Fight
        if (!fighting) {
            canCancelAttack = true;
            //ResetAttack();
            yield break;
        }

        //Sound
        AudioManager.instance.PlaySound(player1.GetComponent<MainPlayer>().atkAudio);
        AudioManager.instance.PlaySound(player2.GetComponent<OtherPlayer>().atkAudio);
        //Get Attacks
        player1Attack = player1.GetComponent<MainPlayer>().attack[1];
        player2Attack = player2.GetComponent<OtherPlayer>().attack[1];
        //Attack Animations
        player1.GetComponent<MainPlayer>().AttackAnimation(player1Attack);
        player2.GetComponent<OtherPlayer>().AttackAnimation(player2Attack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
        if (player1Attack == 1 && player2Attack == 1) Tie();
        else if (player1Attack == 1 && player2Attack == 2) Defeat();
        else if (player1Attack == 1 && player2Attack == 3) Win();
        else if (player1Attack == 2 && player2Attack == 2) Tie();
        else if (player1Attack == 2 && player2Attack == 3) Defeat();
        else if (player1Attack == 2 && player2Attack == 1) Win();
        else if (player1Attack == 3 && player2Attack == 3) Tie();
        else if (player1Attack == 3 && player2Attack == 1) Defeat();
        else if (player1Attack == 3 && player2Attack == 2) Win();
        yield return new WaitForSeconds(1f);

        //If Enemy is dead Stop Fight
        if (!fighting) {
            NewPlayer.instance.canCancelAttack = true;
            NewPlayer.instance.ResetAttack();
            yield break;
        }

        //Sound
        AudioManager.instance.PlaySound(player1.GetComponent<MainPlayer>().atkAudio);
        AudioManager.instance.PlaySound(player2.GetComponent<OtherPlayer>().atkAudio);
        //Get Attacks
        player1Attack = player1.GetComponent<MainPlayer>().attack[2];
        player2Attack = player2.GetComponent<OtherPlayer>().attack[2];
        //Attack Animations
        player1.GetComponent<MainPlayer>().AttackAnimation(player1Attack);
        player2.GetComponent<OtherPlayer>().AttackAnimation(player2Attack);
        yield return new WaitForSeconds(0.5f);
        //Compare Attacks
        if (player1Attack == 1 && player2Attack == 1) Tie();
        else if (player1Attack == 1 && player2Attack == 2) Defeat();
        else if (player1Attack == 1 && player2Attack == 3) Win();
        else if (player1Attack == 2 && player2Attack == 2) Tie();
        else if (player1Attack == 2 && player2Attack == 3) Defeat();
        else if (player1Attack == 2 && player2Attack == 1) Win();
        else if (player1Attack == 3 && player2Attack == 3) Tie();
        else if (player1Attack == 3 && player2Attack == 1) Defeat();
        else if (player1Attack == 3 && player2Attack == 2) Win();
        yield return new WaitForSeconds(1f);

        //Ending Turns Setup next Turns Begin
        //player2.SelectEnemyAttacks();
        MultiplayerCombat.instance.canCancelAttack = true;
        player1.GetComponent<MainPlayer>().pv.RPC("ResetAttack", RpcTarget.All);
        player2.GetComponent<OtherPlayer>().pv.RPC("ResetAttack", RpcTarget.All);
        Go();
    }

    //If win an combat do damage in enemy
    void Win() {
        player2.GetComponent<OtherPlayer>().TakeDamage();
        AudioManager.instance.PlaySound(laughAudio);
    }

    //If lose an combat take damage from enemy and play sound
    void Defeat() {
        player1.GetComponent<MainPlayer>().TakeDamage();
        AudioManager.instance.PlaySound(laughAudio);
    }

    //If the result is an tie play sound
    void Tie() {

    }

    public void UpdateUI() {
        Player1HpSlider.value = player1.GetComponent<MainPlayer>().currentLife;
        Player2HpSlider.value = player2.GetComponent<OtherPlayer>().currentLife;
    }

    public void TurnEnding() {
        if (player1.GetComponent<MainPlayer>().currentLife <= 0) {
            player1.GetComponent<MainPlayer>().currentLife = 0;

            fighting = false;
            GameManager.instance.ShowCanvasGroup(MultiplayerCombat.instance.GameOverPanel);
            string winner = player2Name;
            GameOverText.text = winner + " had Won";
        }

        if(player2.GetComponent<OtherPlayer>().currentLife <= 0) {
            player2.GetComponent<OtherPlayer>().currentLife = 0;

            fighting = false;
            GameManager.instance.ShowCanvasGroup(MultiplayerCombat.instance.GameOverPanel);
            string winner = player1Name;
            GameOverText.text = winner + " had Won";
        }
    }

    //Remove attack and icon from an attack slot
    public void CancelAttackPlayer1(int atk) {
        if (canCancelAttack) {
            FightButton.interactable = false;
            player1.GetComponent<MainPlayer>().CancelAttack(atk);
            //player1.GetComponent<MainPlayer>().attack[atk] = 0;
            //player1.GetComponent<MainPlayer>().attackSlot[atk].sprite = GameManager.instance.mistery;
        }
    }

    //Remove attack and icon from an attack slot
    public void CancelAttackPlayer2(int atk) {
        if (canCancelAttack) {
            FightButton.interactable = false;
            player2.GetComponent<OtherPlayer>().CancelAttack(atk);
            //player2.GetComponent<OtherPlayer>().attack[atk] = 0;
            //player2.GetComponent<OtherPlayer>().attackSlot[atk].sprite = GameManager.instance.mistery;
        }
    }

    public void ExitCurrentRoom() {
        PhotonNetwork.LeaveRoom();
    }

}
