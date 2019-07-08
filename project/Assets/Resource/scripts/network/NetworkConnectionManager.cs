using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace SpecialMove
{
    public class NetworkConnectionManager : MonoBehaviourPunCallbacks
    {
        public Button BtnConnectNetwork;
        public Text Welcome;
        public bool TriesToConnectToMaster;
        public bool TriesToConnectToRoom;
        public bool ready;
        public GameObject Empty;
        // Start is called before the first frame update
        void Awake()
        {
            Screen.SetResolution(1920, 1080, true);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }
        void Start()
        {
            Empty = GameObject.Find("Empty");
            if(Empty != null)
            {
                Destroy(Empty);
            }
            SkillSetter(PlayerPrefs.GetInt("SkillCommand1"), 1);
            CommandTouch.rawCommand[1] = PlayerPrefs.GetInt("SkillCommand1");
            SkillSetter(PlayerPrefs.GetInt("SkillCommand2"), 2);
            CommandTouch.rawCommand[2] = PlayerPrefs.GetInt("SkillCommand2");
            SkillSetter(PlayerPrefs.GetInt("SkillCommand3"), 3);
            CommandTouch.rawCommand[3] = PlayerPrefs.GetInt("SkillCommand3");
            SkillSetter(PlayerPrefs.GetInt("SkillCommand4"), 4);
            CommandTouch.rawCommand[4] = PlayerPrefs.GetInt("SkillCommand4");
            DontDestroyOnLoad(gameObject);
            TriesToConnectToMaster = false;
            TriesToConnectToRoom = false;
            ready = false;            
        }
        // Update is called once per frame
        void Update()
        {
            if (BtnConnectNetwork != null)
                BtnConnectNetwork.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
            if (PhotonNetwork.IsConnected && !TriesToConnectToRoom && !TriesToConnectToMaster && ready)
            {
                ready = false;
                OnClickConnectToRoom();
            }
        }
        public void OnClickConnectToMaster()
        {
            PhotonNetwork.OfflineMode = false;
            TriesToConnectToMaster = true;
            PhotonNetwork.ConnectUsingSettings();
            Welcome.text = "Loading";
        }
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            TriesToConnectToMaster = false;
            ready = true;
        }
        public void OnClickConnectToRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }
            TriesToConnectToRoom = true;
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            TriesToConnectToRoom = false;
            SceneManager.LoadScene("NetworkField");
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            TriesToConnectToRoom = false;
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            ready = false;
            Welcome.text = "Welcome";
        }
        void SkillSetter(int a, int b)
        {
            Delivery.joyStick[b] = a;
        }
    }
}