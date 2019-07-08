using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpecialMove
{
    public class PushSkill : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject OptionC;
        public GameObject OptionD;
        public int i, j;
        public int sk;
        public int check;
        public int[] mod = new int[3];
        static public bool deliver;
        // Start is called before the first frame update
        // Update is called once per frame
        void Start()
        {
            switch (this.tag)
            {
                case "skill1":
                    SkillSetter(PlayerPrefs.GetInt("SkillCommand1"),1);
                    CommandTouch.rawCommand[1] = PlayerPrefs.GetInt("SkillCommand1");
                    break;
                case "skill2":
                    SkillSetter(PlayerPrefs.GetInt("SkillCommand2"), 2);
                    CommandTouch.rawCommand[2] = PlayerPrefs.GetInt("SkillCommand2");
                    break;
                case "skill3":
                    SkillSetter(PlayerPrefs.GetInt("SkillCommand3"), 3);
                    CommandTouch.rawCommand[3] = PlayerPrefs.GetInt("SkillCommand3");
                    break;
                case "skill4":
                    SkillSetter(PlayerPrefs.GetInt("SkillCommand4"), 4);
                    CommandTouch.rawCommand[4] = PlayerPrefs.GetInt("SkillCommand4");
                    break;
            }
            Initialize();
            deliver = false;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            this.gameObject.GetComponent<Image>().color = Color.yellow;
            switch (this.tag)
            {
                case "skill1":
                    CommandTouch.select = 1;
                    OptionD.SetActive(true);
                    break;
                case "skill2":
                    CommandTouch.select = 2;
                    OptionD.SetActive(true);
                    break;
                case "skill3":
                    CommandTouch.select = 3;
                    OptionD.SetActive(true);
                    break;
                case "skill4":
                    CommandTouch.select = 4;
                    OptionD.SetActive(true);
                    break;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            CommandTouch.select = -1;
            OptionD.SetActive(false);
            Imagechange();
        }
        public void Rs()
        {
            CommandTouch.rawCommand[1] = 999;
            CommandTouch.rawCommand[2] = 999;
            CommandTouch.rawCommand[3] = 999;
            CommandTouch.rawCommand[4] = 999;
            Imagechange();
        }
        void Initialize()
        {
            for (i = 1; i < 5; i++)
            {
                mod[2] = CommandTouch.rawCommand[i] % 10;
                mod[1] = (CommandTouch.rawCommand[i] - mod[2]) % 100 / 10;
                mod[0] = (CommandTouch.rawCommand[i] - mod[2] - mod[1]) / 100;
                if (mod[0] == 0)
                {
                    mod[0] = mod[1] = mod[2] = 9;
                }
                sk = mod[0] * 100 + mod[1] * 10 + mod[2];
                SkillSetter(sk, i);
                for (j = 0; j < 3; j++)
                {
                    switch (mod[j])
                    {
                        case 1:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;
                        case 2:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 180);
                            break;
                        case 3:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 90);
                            break;
                        case 4:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 270);
                            break;
                        default:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 90, 0);
                            break;
                    }
                }
            }
        }
        void Imagechange()
        {
            for (i = 1; i < 5; i++)
            {
                mod[2] = CommandTouch.rawCommand[i] % 10;
                mod[1] = (CommandTouch.rawCommand[i] - mod[2]) % 100 / 10;
                mod[0] = (CommandTouch.rawCommand[i] - mod[2] - mod[1]) / 100;
                if (mod[0] == 0)
                {
                    mod[0] = mod[1] = mod[2] = 9;
                }
                sk = mod[0] * 100 + mod[1] * 10 + mod[2];
                SkillSetter(sk, i);
                switch (i)
                {
                    case 1:
                        PlayerPrefs.SetInt("SkillCommand1", sk);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("SkillCommand2", sk);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("SkillCommand3", sk);
                        break;
                    case 4:
                        PlayerPrefs.SetInt("SkillCommand4", sk);
                        break;
                }
                PlayerPrefs.Save();
                for (j = 0; j < 3; j++)
                {
                    switch (mod[j])
                    {
                        case 1:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;
                        case 2:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 180);
                            break;
                        case 3:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 90);
                            break;
                        case 4:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 0, 270);
                            break;
                        default:
                            OptionC.transform.GetChild(i).transform.GetChild(j).transform.rotation = Quaternion.Euler(0, 90, 0);
                            break;
                    }
                }
            }
        }
        void SkillSetter(int a, int b)
        {
            Delivery.joyStick[b] = a;
        }
    }
}