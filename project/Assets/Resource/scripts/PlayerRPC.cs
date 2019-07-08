using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SpecialMove
{
    public class PlayerRPC : MonoBehaviour
    {
        public GameObject Auto;
        public GameObject AutoPrefab;
        public GameObject Skill1;
        public GameObject Skill1Prefab;
        private GameObject[] Skill2 = new GameObject[7];
        public GameObject Skill2Prefab;
        public GameObject Skill3;
        public GameObject Skill3Prefab;
        public GameObject Skill4;
        public GameObject Skill4Prefab;
        public GameObject SfxManager;
        void Start()
        {
            SfxManager = GameObject.Find("SfxManager");
        }
        void Update()
        {
            if (this.gameObject.GetComponent<Player>() == null)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        public void AutoAttack(Vector2 pos, Quaternion rot, double time)
        {
            GetComponent<PhotonView>().RPC("AutoAttackLaunch", RpcTarget.Others, pos, rot, time, 0);
            SfxManager.GetComponent<SoundManager>().SfxChalk();
            AutoAttackLaunch(pos, rot, time, 255);
        }
        public void Skill11(Vector2 pos, Quaternion rot, double time)
        {
            GetComponent<PhotonView>().RPC("Skill1Launch", RpcTarget.Others, pos, rot, time, 0);
            SfxManager.GetComponent<SoundManager>().SfxChalk();
            Skill1Launch(pos, rot, time, 255);
        }
        public void Skill22(Vector2 pos, Quaternion rot, double time)
        {
            GetComponent<PhotonView>().RPC("Skill2Launch", RpcTarget.Others, pos, rot, time, 0);
            SfxManager.GetComponent<SoundManager>().SfxChalk();
            Skill2Launch(pos, rot, time, 255);
        }
        public void Skill33(Vector2 pos, Quaternion rot, double time)
        {
            GetComponent<PhotonView>().RPC("Skill3Launch", RpcTarget.Others, pos, rot, time, 0);
            SfxManager.GetComponent<SoundManager>().SfxChalk();
            Skill3Launch(pos, rot, time, 255);

        }
        public void Skill44(Vector2 pos, Quaternion rot, double time)
        {
            GetComponent<PhotonView>().RPC("Skill4Launch", RpcTarget.Others, pos, rot, time, 0);
            SfxManager.GetComponent<SoundManager>().SfxChalk();
            Skill4Launch(pos, rot, time, 255);
        }
        [PunRPC]
        void AutoAttackLaunch(Vector2 pos, Quaternion rot, double time, int Own)
        {
            Auto = Instantiate(AutoPrefab, pos, rot);
            Auto.SetActive(true);
            Auto.GetComponent<AutoAttack>().pos = pos;
            Auto.GetComponent<AutoAttack>().rot = rot;
            Auto.GetComponent<AutoAttack>().time = time;
            Auto.GetComponent<AutoAttack>().own = Own;
            SfxManager.GetComponent<SoundManager>().SfxChalk();
        }
        [PunRPC]
        void Skill1Launch(Vector2 pos, Quaternion rot, double time, int Own)
        {
            Skill1 = Instantiate(Skill1Prefab, pos, rot);
            Skill1.SetActive(true);
            Skill1.GetComponent<Skill1>().pos = pos;
            Skill1.GetComponent<Skill1>().rot = rot;
            Skill1.GetComponent<Skill1>().time = time;
            Skill1.GetComponent<Skill1>().own = Own;
            SfxManager.GetComponent<SoundManager>().SfxChalk();
        }
        [PunRPC]
        void Skill2Launch(Vector2 pos, Quaternion rot, double time, int Own)
        {
            for (int i = 0; i < 7; i++)
            {
                Skill2[i] = Instantiate(Skill2Prefab, pos, Quaternion.Euler(0, 0, rot.eulerAngles.z - 30 + i * 10));
                Skill2[i].SetActive(true);
                Skill2[i].GetComponent<Skill2>().pos = pos;
                Skill2[i].GetComponent<Skill2>().rot = Quaternion.Euler(0, 0, rot.eulerAngles.z - 30 + i * 10);
                Skill2[i].GetComponent<Skill2>().time = time;
                Skill2[i].GetComponent<Skill2>().own = Own;
                SfxManager.GetComponent<SoundManager>().SfxChalk();
            }
        }
        [PunRPC]
        void Skill3Launch(Vector2 pos, Quaternion rot, double time, int Own)
        {
            Skill3 = Instantiate(Skill3Prefab, pos, rot);
            Skill3.SetActive(true);
            Skill3.GetComponent<Skill3>().pos = pos;
            Skill3.GetComponent<Skill3>().rot = rot;
            Skill3.GetComponent<Skill3>().time = time;
            Skill3.GetComponent<Skill3>().own = Own;
            SfxManager.GetComponent<SoundManager>().SfxChalk();
        }
        [PunRPC]
        void Skill4Launch(Vector2 pos, Quaternion rot, double time, int Own)
        {
            Skill4 = Instantiate(Skill4Prefab, pos, rot);
            Skill4.SetActive(true);
            Skill4.GetComponent<Skill4>().pos = pos;
            Skill4.GetComponent<Skill4>().rot = rot;
            Skill4.GetComponent<Skill4>().time = time;
            Skill4.GetComponent<Skill4>().own = Own;
            SfxManager.GetComponent<SoundManager>().SfxChalk();
        }
    }
}