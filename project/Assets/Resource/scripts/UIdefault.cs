using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpecialMove
{
    public class UIdefault : MonoBehaviour
    {
        public GameObject BgmManager;
        public GameObject SfxManager;
        public GameObject optionA;
        public GameObject optionB;
        public GameObject optionC;
        public GameObject optionD;
        public GameObject s1;
        public GameObject s2;
        public GameObject s3;
        public GameObject s4;
        public Button BgmBtn;
        public Button SfxBtn;
        public int BGM;
        public int SFX;

        void Start()
        {
            optionA.SetActive(true);
            optionB.SetActive(true);
            optionC.SetActive(false);
            optionD.SetActive(false);
            SoundBtn();
        }
        public void openC()
        {
            SfxManager.GetComponent<SoundManager>().SfxClick();
            optionA.SetActive(false);
            optionB.SetActive(false);
            optionC.SetActive(true);
            optionD.SetActive(false);
        }
        public void closeC()
        {
            SfxManager.GetComponent<SoundManager>().SfxClick();
            optionA.SetActive(true);
            optionB.SetActive(true);
            optionC.SetActive(false);
            optionD.SetActive(false);
        }
        public void closeAPP()
        {
            SfxManager.GetComponent<SoundManager>().SfxClick();
            Application.Quit();
        }
        public void openNET()
        {
            SfxManager.GetComponent<SoundManager>().SfxClick();
            optionA.SetActive(false);
            optionB.SetActive(false);
            optionC.SetActive(false);
            optionD.SetActive(false);
        }
        public void BgmOnOff()
        {
            BGM = BGM == 0  ? 1 : 0;
            PlayerPrefs.SetInt("BGM", BGM);
            PlayerPrefs.Save();
            SoundBtn();
        }
        public void SfxOnOff()
        {
            SFX = SFX == 0 ? 1 : 0;
            PlayerPrefs.SetInt("SFX", SFX);
            PlayerPrefs.Save();
            SoundBtn();
        }
        public void Flush()
        {
            SfxManager.GetComponent<SoundManager>().SfxClick();
            s1.GetComponent<PushSkill>().Rs();
            PlayerPrefs.SetInt("SkillCommand1", 999);
            PlayerPrefs.SetInt("SkillCommand2", 999);
            PlayerPrefs.SetInt("SkillCommand3", 999);
            PlayerPrefs.SetInt("SkillCommand4", 999);
        }
        public void SoundBtn()
        {
            BgmManager.GetComponent<AudioSource>().volume = BGM = PlayerPrefs.GetInt("BGM");
            SfxManager.GetComponent<AudioSource>().volume = SFX = PlayerPrefs.GetInt("SFX");
            BgmBtn.GetComponent<Image>().color = new Color(255, 255, 255 - (BGM * 255));
            SfxBtn.GetComponent<Image>().color = new Color(255, 255, 255 - (SFX * 255));
            SfxManager.GetComponent<SoundManager>().SfxClick();
        }
    }
}