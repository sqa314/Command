using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace SpecialMove
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public Player PlayerPrefab;
        public Player Localplayer;
        public Button GameStartBtn;
        public Button ReadyBtn;
        public Button VictoryBtn;
        public GameObject Empty;
        public GameObject SfxManager;
        public GameObject Network;
        public GameObject Gong;
        public int playerCount;
        public bool gameStart;
        void Awake()
        {
            Screen.SetResolution(1920, 1080, true);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
            gameStart = false;
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("default");
                return;
            }
            else
            {
                playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            }
            Gong = GameObject.Find("GONG");
        }
        void Start()
        {
            Player.RefreshInstance(ref Localplayer, PlayerPrefab, playerCount);
            Network = GameObject.Find("NetworkConnectionManager");
            Empty = GameObject.Find("Empty");
            if (Empty != null)
            {
                OnClickDisconnectRoom();
            }
        }
        void Update()
        {
            if(Gong == null)
            {
                gameStart = true;
            }
            if (GameStartBtn != null && PhotonNetwork.IsConnected)
            {
                GameStartBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient && !gameStart);
                if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                {
                    GameStartBtn.transform.GetChild(0).GetComponent<Text>().text = "You need an opponent";
                    GameStartBtn.transform.GetChild(0).GetComponent<Text>().fontSize = 70;
                    GameStartBtn.GetComponent<Button>().interactable = false;
                }
                else
                {
                    GameStartBtn.transform.GetChild(0).GetComponent<Text>().text = "Game Start";
                    GameStartBtn.transform.GetChild(0).GetComponent<Text>().fontSize = 100;
                    GameStartBtn.GetComponent<Button>().interactable = true;
                }
            }
            if (ReadyBtn != null && PhotonNetwork.IsConnected)
            {
                ReadyBtn.gameObject.SetActive(!PhotonNetwork.IsMasterClient && !gameStart);
            }
            if (VictoryBtn != null && PhotonNetwork.IsConnected)
            {
                VictoryBtn.gameObject.SetActive(PhotonNetwork.CurrentRoom.PlayerCount == 1 && gameStart);
            }
        }
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Player.RefreshInstance(ref Localplayer, PlayerPrefab, playerCount);
        }
        public void OnClickDisconnectRoom()
        {
            PhotonNetwork.Disconnect();
            Destroy(Network);
            SceneManager.LoadScene("default");
            SfxManager.GetComponent<SoundManager>().SfxClick();
        }
        public void OnClickGameStart()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.Destroy(Gong);
        }
    }
}