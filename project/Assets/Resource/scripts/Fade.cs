using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Fade : MonoBehaviourPun
{
    int a;
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        a++;
        if (a > 2)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
