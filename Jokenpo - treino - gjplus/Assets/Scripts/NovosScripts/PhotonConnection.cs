using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    int gameType;
    public TMP_InputField nameField;
    public byte playerLimit;
    public GameObject cancelButton;
    public AudioClip fightMusic;
    public bool startLoading;
    public TextMeshProUGUI loadtext;
    RoomOptions options = new RoomOptions();

    public void ConnectToPhotonServer() {
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.ConnectUsingSettings();
            options.MaxPlayers = playerLimit;
        } else {
            OnJoinedLobby();
        }
    }

    public override void OnConnectedToMaster() {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        else {
            OnJoinedLobby();
        }
    }

    public override void OnJoinedLobby() {
        base.OnJoinedLobby();

        if (startLoading) {
            if (gameType == 0) {
                PhotonNetwork.JoinOrCreateRoom("basic", options, TypedLobby.Default);
            } else {
                JoinRoom();
            }
            loadtext.text = "Looking for a Opponent...";
        }
            
    }

    public void JoinRoom() {
        if (startLoading)
            PhotonNetwork.JoinRandomOrCreateRoom();
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        StartCoroutine(WaitPlayer());
    }

    public IEnumerator WaitPlayer() {
        Debug.Log("Waiting For Players");
        cancelButton.SetActive(true);
        AudioManager.instance.FadeMusic(fightMusic);
        if (PhotonNetwork.CurrentRoom != null)
            yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount >= playerLimit);
        PhotonNetwork.LoadLevel(4);
    }

    public void CreateGame(int MultiplayerType) {
        if (nameField.text != "") {
            startLoading = true;
            if (startLoading) {
                gameType = MultiplayerType;
                ConnectToPhotonServer();
                PlayerPrefs.SetString("playerName", nameField.text);
                GameManager.instance.ShowCanvasGroup(LoadManager.instance.loadCG);
            }
        }
    }

    public void CancelEnterGame() {
        StopCoroutine(WaitPlayer());
        PhotonNetwork.LeaveRoom();
        startLoading = false;
        loadtext.text = "Loading...";
    }

    public override void OnLeftRoom() {
        base.OnLeftRoom();
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby() {
        base.OnLeftLobby();
        PhotonNetwork.Disconnect();
    }

}
