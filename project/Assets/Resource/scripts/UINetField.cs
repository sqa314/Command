using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpecialMove
{
    public class UINetField : MonoBehaviour
    {
        public GameObject optionD;
        public GameObject BgmManager;
        public GameObject SfxManager;
        // Start is called before the first frame update
        // Update is called once per frame
        void Start()
        {
            BgmManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("BGM");
            SfxManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("SFX");
            optionD.SetActive(true);
        }
    }
}