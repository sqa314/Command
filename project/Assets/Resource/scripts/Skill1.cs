using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SpecialMove
{
    public class Skill1 : MonoBehaviourPun
    {
        public int own;
        public Vector3 pos;
        public Quaternion rot;
        public double time;
        void Start()
        {
            this.transform.rotation = rot;
            this.GetComponent<SpriteRenderer>().color = new Color(255, own, own);
        }
        void Update()
        {
            transform.position = new Vector2(pos.x + Mathf.Cos(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 12, pos.y + Mathf.Sin(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 12);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var hit = collision.gameObject.GetComponent<Hit>();
            if (hit != null && own == 0 && collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitSkill1();
            }
            else if(hit != null && own != 0 && !collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitSkill1();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "wall")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}