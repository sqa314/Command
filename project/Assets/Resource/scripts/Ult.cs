using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace SpecialMove
{
    public class Ult : MonoBehaviourPun
    {
        public int ult;
        public GameObject P;
        public Sprite sprite;
        // Start is called before the first frame update
        void Start()
        {
            if (photonView.IsMine)
            {
                P = GameObject.Find("Player0");
            }
            else
            {
                P = GameObject.Find("Player(Clone)");
            }
            P.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        }
    
        // Update is called once per frame
        void Update()
        {
            ult--;
            if(ult == 0&& photonView.IsMine)
            {
                P.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}