using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SpecialMove
{
    public class AutoAttack : MonoBehaviour
    {
        public Vector2 pos;
        public Quaternion rot;
        public double time;
        public Vector2 past;
        public int own;
        void Start()
        {
            transform.rotation = rot;
            this.GetComponent<SpriteRenderer>().color = new Color(255, own, own);
        }
        void Update()
        {
            transform.position = new Vector2(pos.x + Mathf.Cos(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 4, pos.y + Mathf.Sin(Mathf.Deg2Rad * rot.eulerAngles.z) * (float)(PhotonNetwork.Time - time) * 4);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var hit = collision.gameObject.GetComponent<Hit>();
            if (hit != null && own == 0 && collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitAutoAttack(this.transform.rotation);
                this.gameObject.SetActive(false);
            }
            else if (hit != null && own != 0 && !collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.OnHitAutoAttack(this.transform.rotation);
                this.gameObject.SetActive(false);
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