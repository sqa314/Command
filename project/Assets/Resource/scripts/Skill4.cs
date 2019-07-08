using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SpecialMove
{
    public class Skill4 : MonoBehaviourPun
    {
        public int own;
        public GameObject C;
        public Vector3 pos;
        public Quaternion rot;
        public double time;
        void Start()
        {
            this.transform.rotation = rot;
            C = this.transform.GetChild(0).gameObject;
            transform.localScale = new Vector3(1, 1, 1);
            C.GetComponent<SpriteRenderer>().color = new Color(255, own, own);
        }
        void Update()
        {
            transform.position = new Vector2(pos.x + Mathf.Cos(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 2, pos.y + Mathf.Sin(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 2);
            C.transform.rotation = Quaternion.Euler(0, 0, C.transform.rotation.eulerAngles.z + 3);
            transform.localScale += new Vector3(0.005f, 0.005f, 0);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            var hit = collision.gameObject.GetComponent<Hit>();
            if (hit != null && own == 0 && collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitSkill4(this.transform.position);
            }
            else if (hit != null && own != 0 && !collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitSkill4(this.transform.position);
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