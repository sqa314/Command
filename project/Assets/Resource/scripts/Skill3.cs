using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SpecialMove
{
    public class Skill3 : MonoBehaviourPun
    {
        public int own;
        public Vector3 pos;
        public Quaternion rot;
        public double time;
        public int j;
        void Start()
        {
            this.transform.rotation = rot;
            this.GetComponent<SpriteRenderer>().color = new Color(255, own, own);
            j = 30;
        }
        void Update()
        {
            transform.position = new Vector2(pos.x + Mathf.Cos(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 5, pos.y + Mathf.Sin(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 5);
            j--;
            if (j < 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var hit = collision.gameObject.GetComponent<Hit>();
            if (hit != null)
            {
                if (own == 0 && collision.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    hit.OnHitSkill3(this.transform.GetChild(0).position, this.transform.rotation);
                }
                else if (own == 0 && !collision.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    hit.OnHitSkill3Self(this.transform.position, this.transform.rotation);
                }
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            var hit = collision.gameObject.GetComponent<Hit>();
            if (hit != null)
            {
                if (own == 0)
                {
                    hit.OnHitSkill3(this.transform.GetChild(0).position, this.transform.rotation);
                }
                else
                {
                    hit.OnHitSkill3Self(this.transform.position, this.transform.rotation);
                }
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