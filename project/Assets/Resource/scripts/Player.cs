using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

namespace SpecialMove
{
    public class Player : MonoBehaviourPun
    {
        public GameObject GM;
        int a;
        public InputStr Input;
        public struct InputStr
        {
            public float x;
            public float y;
        }
        public int i;
        void Awake()
        {
            if (!photonView.IsMine && GetComponent<Touch>() != null && GetComponent<Player>() != null)
            {
                Destroy(GetComponent<Touch>());
                Destroy(GetComponent<Player>());
            }
        }
        void Start()
        {
            GM = GameObject.Find("GameManager");
        }
        public static void RefreshInstance(ref Player player, Player Prefab, int playerCount)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;
            var color = Color.clear;
            switch (playerCount)
            {
                case 1:
                    position = new Vector3(1.5f, 2.5f);
                    rotation = Quaternion.Euler(0, 0, 315);
                    break;
                case 2:
                    position = new Vector3(6.5f, -2.5f);
                    rotation = Quaternion.Euler(0, 0, 135);
                    break;
                default:
                    position = Vector3.zero;
                    rotation = Quaternion.identity;
                    break;
            }
            color = Color.cyan;
            if (player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                color = player.transform.GetComponent<SpriteRenderer>().color;
                PhotonNetwork.Destroy(player.gameObject);
            }
            player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<Player>();
            player.gameObject.name = "Player0";
            player.transform.GetComponent<SpriteRenderer>().color = color;
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("wall") && this.GetComponent<Touch>().gameStart)
            {
                GM.GetComponent<GameManager>().OnClickDisconnectRoom();
            }
        }
    }
}