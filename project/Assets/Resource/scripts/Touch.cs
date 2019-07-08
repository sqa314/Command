using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace SpecialMove
{
    public class Touch : MonoBehaviourPun
    {
        private GameObject[] arrow = new GameObject[3];
        private GameObject[] cool = new GameObject[6];
        public GameObject Line;
        public GameObject Guide;
        public GameObject Gong;
        public GameObject UltPref;
        public Vector2 leftstart;
        public Vector2 left1;
        public Vector2 leftend;
        public Vector2 rightstart;
        public Vector2 right1;
        public Vector2 rightend;
        public Vector2 arrowPosition;
        public Color color;
        public Sprite sprite;
        public float distanceMove;
        public float distanceCommand;
        public float distanceSkill;
        public float directionMove;
        public float directionCommand;
        public float directionSkill;
        public float time;
        public float unit;
        public float roundMax;
        public float stackUlt;
        public float[] coolDown = new float[6] { 1, 1, 1, 1, 1, 1 };
        public int i;
        public int j;
        public int finger1;
        public int finger2;
        public int skillCommand;
        public int[] command = new int[3] { 0, 0, 0 };
        public bool round;
        public bool ui;
        public bool adjust;
        public bool startSkill;
        public bool gameStart;
        public Hit hit;
        public int ult;
        void Awake()
        {
            arrow[2] = GameObject.Find("commandThird");
            arrow[1] = GameObject.Find("commandSecond");
            arrow[0] = GameObject.Find("commandFirst");
            cool[0] = GameObject.Find("CoolF");
            cool[1] = GameObject.Find("Cool1");
            cool[2] = GameObject.Find("Cool2");
            cool[3] = GameObject.Find("Cool3");
            cool[4] = GameObject.Find("Cool4");
            cool[5] = GameObject.Find("CoolU");
            Gong = GameObject.Find("GONG");
            Line = GameObject.Find("Line");
            Guide = GameObject.Find("Guide");
        }
        void Start()
        {
            unit = Screen.width * 0.001f;
            gameStart = false;
            CommandDisplay();
            j = 20;
            color = arrow[0].GetComponent<Image>().color;
            Guide.SetActive(true);
            hit = this.gameObject.GetComponent<Hit>();
        }
        void FixedUpdate()
        {
            if(Gong == null && !gameStart)
            {
                gameStart = true;
                SetPosition();
            }
            j--;
            time--;
            if (j == 0)
                j = 20;
            if (time < 0)
            {
                skillCommand = 0;
                command[2] = command[1] = command[0] = 0;
                arrow[0].transform.rotation = Quaternion.Euler(0, 90, 0);
                arrow[1].transform.rotation = Quaternion.Euler(0, 90, 0);
                arrow[2].transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            directionCommand = Mathf.Rad2Deg * Mathf.Atan2(arrowPosition.y - left1.y, arrowPosition.x - left1.x) + 180;
            directionMove = Mathf.Rad2Deg * Mathf.Atan2(leftstart.y - left1.y, leftstart.x - left1.x) + 180;
            directionSkill = Mathf.Rad2Deg * Mathf.Atan2(rightstart.y - right1.y, rightstart.x - right1.x) + 180;
            CommandColor();
            if (Input.touchCount == 0)
                finger1 = finger2 = 2;
            else if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).position.x < unit * 500)
                    finger1 = 0;
                else
                    finger1 = 1;
            }
            else
            {
                if (Input.GetTouch(0).position.x < Input.GetTouch(1).position.x)
                    finger1 = 0;
                else
                    finger1 = 1;
            }
            if (Input.touchCount > finger1)
            {
                left1 = Input.GetTouch(finger1).position;
                switch (Input.GetTouch(finger1).phase)
                {
                    case TouchPhase.Began:
                        leftstart = left1;
                        arrowPosition = leftstart;
                        distanceCommand = Vector2.Distance(arrowPosition, left1);
                        distanceMove = Vector2.Distance(leftstart, left1);
                        RefreshMovePoint();
                        MoveCharacter();
                        Commands();
                        Guide.SetActive(false);
                        Line.SetActive(true);
                        break;
                    case TouchPhase.Moved:
                        distanceCommand = Vector2.Distance(arrowPosition, left1);
                        distanceMove = Vector2.Distance(leftstart, left1);
                        RefreshMovePoint();
                        MoveCharacter();
                        Commands();
                        break;
                    case TouchPhase.Stationary:
                        distanceCommand = Vector2.Distance(arrowPosition, left1);
                        distanceMove = Vector2.Distance(leftstart, left1);
                        RefreshMovePoint();
                        MoveCharacter();
                        Commands();
                        break;
                    case TouchPhase.Ended:
                        Line.SetActive(false);
                        Guide.SetActive(true);
                        leftend = left1;
                        time = 30;
                        break;
                }
            }
        }
        void Update()
        {
            if (finger1 == 0)
                finger2 = 1;
            else
                finger2 = 0;
            if (Input.touchCount > finger2)
            {
                right1 = Input.GetTouch(finger2).position;
                switch (Input.GetTouch(finger2).phase)
                {
                    case TouchPhase.Began:
                        rightstart = right1;
                        roundMax = 0;
                        round = false;
                        break;
                    case TouchPhase.Moved:
                        distanceSkill = Vector2.Distance(rightstart, right1);
                        UltCheck();
                        break;
                    case TouchPhase.Stationary:
                        distanceSkill = Vector2.Distance(rightstart, right1);
                        UltCheck();
                        break;
                    case TouchPhase.Ended:
                        rightend = right1;
                        SkillCheck();
                        break;
                }
            }
            switch (SkillGetter(skillCommand))
            {
                case "1":
                    cool[1].GetComponent<Image>().color = Color.yellow;
                    cool[2].GetComponent<Image>().color = Color.white;
                    cool[3].GetComponent<Image>().color = Color.white;
                    cool[4].GetComponent<Image>().color = Color.white;
                    break;
                case "2":
                    cool[1].GetComponent<Image>().color = Color.white;
                    cool[2].GetComponent<Image>().color = Color.yellow;
                    cool[3].GetComponent<Image>().color = Color.white;
                    cool[4].GetComponent<Image>().color = Color.white;
                    break;
                case "3":
                    cool[1].GetComponent<Image>().color = Color.white;
                    cool[2].GetComponent<Image>().color = Color.white;
                    cool[3].GetComponent<Image>().color = Color.yellow;
                    cool[4].GetComponent<Image>().color = Color.white;
                    break;
                case "4":
                    cool[1].GetComponent<Image>().color = Color.white;
                    cool[2].GetComponent<Image>().color = Color.white;
                    cool[3].GetComponent<Image>().color = Color.white;
                    cool[4].GetComponent<Image>().color = Color.yellow;
                    break;
                default:
                    cool[1].GetComponent<Image>().color = Color.white;
                    cool[2].GetComponent<Image>().color = Color.white;
                    cool[3].GetComponent<Image>().color = Color.white;
                    cool[4].GetComponent<Image>().color = Color.white;
                    break;
            }
        }
        void LateUpdate()
        {
            if (gameStart)
            {
                if (coolDown[1] < 1)
                {
                    coolDown[1] += 0.004167f;
                    cool[1].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                    cool[1].transform.GetChild(1).GetComponent<Text>().text = (4 - coolDown[1] * 4) < 1 ? $"{4 - coolDown[1] * 4:F1}" : $"{4 - coolDown[1] * 4:F0}";
                }
                else
                {
                    cool[1].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    cool[1].transform.GetChild(1).GetComponent<Text>().text = "";
                }
                if (coolDown[2] < 1)
                {
                    coolDown[2] += 0.002778f;
                    cool[2].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                    cool[2].transform.GetChild(1).GetComponent<Text>().text = (6 - coolDown[2] * 6) < 1 ? $"{6 - coolDown[2] * 6:F1}" : $"{6 - coolDown[2] * 6:F0}";
                }
                else
                {
                    cool[2].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    cool[2].transform.GetChild(1).GetComponent<Text>().text = "";
                }
                if (coolDown[3] < 1)
                {
                    coolDown[3] += 0.002083f;
                    cool[3].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                    cool[3].transform.GetChild(1).GetComponent<Text>().text = (8 - coolDown[3] * 8) < 1 ? $"{8 - coolDown[3] * 8:F1}" : $"{8 - coolDown[3] * 8:F0}";
                }
                else
                {
                    cool[3].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    cool[3].transform.GetChild(1).GetComponent<Text>().text = "";
                }
                if (coolDown[4] < 1)
                {
                    coolDown[4] += 0.001667f;
                    cool[4].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                    cool[4].transform.GetChild(1).GetComponent<Text>().text = (10 - coolDown[4] * 10) < 1 ? $"{10 - coolDown[4] * 10:F1}" : $"{10 - coolDown[4] * 10:F0}";

                }
                else
                {
                    cool[4].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                    cool[4].transform.GetChild(1).GetComponent<Text>().text = "";
                }
                coolDown[5] += 0.003333f;
                cool[1].GetComponent<Image>().fillAmount = coolDown[1];
                cool[2].GetComponent<Image>().fillAmount = coolDown[2];
                cool[3].GetComponent<Image>().fillAmount = coolDown[3];
                cool[4].GetComponent<Image>().fillAmount = coolDown[4];
                cool[0].GetComponent<Slider>().value = coolDown[5];
                cool[5].GetComponent<Slider>().value = stackUlt;
                for (i = 0; i < Input.touchCount; i++)
                {
                    if (EventSystem.current.IsPointerOverGameObject(i))
                    {
                        ui = true;
                        break;
                    }
                    else
                        ui = false;
                }
                if (startSkill)
                {
                    skillCommand = command[2] * 100 + command[1] * 10 + command[0];

                    if (coolDown[int.Parse(SkillGetter(skillCommand))] >= 1)
                    {
                        CoolDownReset(int.Parse(SkillGetter(skillCommand)));
                        stackUlt += 0.201f;
                        switch (SkillGetter(skillCommand))
                        {
                            case "1":
                                this.gameObject.GetComponent<PlayerRPC>().Skill11(this.transform.position, Quaternion.Euler(0, 0, directionSkill), PhotonNetwork.Time);
                                break;
                            case "2":
                                this.gameObject.GetComponent<PlayerRPC>().Skill22(this.transform.position, Quaternion.Euler(0, 0, directionSkill), PhotonNetwork.Time);
                                break;
                            case "3":
                                this.gameObject.GetComponent<PlayerRPC>().Skill33(this.transform.position, Quaternion.Euler(0, 0, directionSkill), PhotonNetwork.Time);
                                break;
                            case "4":
                                this.gameObject.GetComponent<PlayerRPC>().Skill44(this.transform.position, Quaternion.Euler(0, 0, directionSkill), PhotonNetwork.Time);
                                break;
                        }
                    }
                    else
                    {
                        this.gameObject.GetComponent<PlayerRPC>().AutoAttack(this.transform.position, Quaternion.Euler(0, 0, directionSkill), PhotonNetwork.Time);
                    }
                }
                startSkill = false;
            }
        }
        void SetPosition()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                this.gameObject.transform.position = new Vector2(1.5f, 2.5f);
            }
            else
            {
                this.gameObject.transform.position = new Vector2(6.5f, -2.5f);
            }
        }
        void MoveCharacter()
        {
            if (!Hit.rooted)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, directionMove);
                this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                if (distanceMove < unit * 20)
                    this.transform.Translate(transform.right * 0.002f, Space.World);
                else if (distanceMove < unit * 245)
                    this.transform.Translate(transform.right * 0.012f, Space.World);
                else
                    this.transform.Translate(transform.right * 0.015f, Space.World);
            }
        }
        void RefreshMovePoint()
        {
            Line.transform.position = leftstart;
            Line.transform.rotation = Quaternion.Euler(0, 0, directionMove);
            Line.gameObject.GetComponent<Image>().fillAmount = distanceMove * 0.001f;
            if (distanceMove > 250)
            {
                while (Vector2.Distance(leftstart, left1) > 250)
                {
                    leftstart.x += Mathf.Cos(Mathf.Deg2Rad * directionMove);
                    leftstart.y += Mathf.Sin(Mathf.Deg2Rad * directionMove);
                }
            }
        }
        void UltCheck()
        {
            if (roundMax < distanceSkill)
            {
                roundMax = distanceSkill;
            }
            else if (distanceSkill > 150 && round == false && distanceSkill < roundMax * 0.7f)
            {
                round = true;
            }
        }
        void SkillCheck()
        {
            roundMax = 0;
            if (round && stackUlt >= 1)
            {
                round = false;
                Hit.rooted = false;
         //       UltPref = PhotonNetwork.Instantiate("Ult", Vector3.zero, Quaternion.identity);
          //      UltPref.GetComponent<Ult>().ult = 300;
            }
            else
            {
                if (distanceSkill < unit * 20)
                {
                    if (coolDown[5] >= 1)
                    {
                        CoolDownReset(5);
                        if (Vector2.Distance(Camera.main.WorldToScreenPoint(this.transform.position), rightstart) < unit * 200)
                        {
                            this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(rightstart).x, Camera.main.ScreenToWorldPoint(rightstart).y, 0);
                        }
                        else
                        {
                            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(Camera.main.WorldToScreenPoint(this.transform.position).y - rightstart.y, Camera.main.WorldToScreenPoint(this.transform.position).x - rightstart.x) + 180);
                            this.transform.Translate(transform.right * 3, Space.World);
                            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                }
                else
                {
                    startSkill = true;
                }
            }
        }
        void CoolDownReset(int a)
        {
            coolDown[a] = 0;
        }
        string SkillGetter(int a)
        {
            for (i = 4; i != 0; i--)
            {
                if (Delivery.joyStick[i] == a)
                    break;
            }
            return Delivery.skillName[i];
        }
        void Commands()
        {
            if ((directionCommand < 30 && directionCommand >= 0) || (directionCommand > 330 && directionCommand <= 360))
            {
                if (command[0] == 1)
                {
                    if (j == 5)
                    {
                        arrowPosition = left1;
                    }
                }
                else
                {
                    if (distanceCommand > unit * 50)
                    {
                        command[2] = command[1];
                        command[1] = command[0];
                        command[0] = 1;
                        CommandDisplay();
                    }
                }
            }
            else if (directionCommand < 210 && directionCommand > 150)
            {
                if (command[0] == 2)
                {
                    if (j == 5)
                    {
                        arrowPosition = left1;
                    }
                }
                else
                {
                    if (distanceCommand > unit * 50)
                    {
                        command[2] = command[1];
                        command[1] = command[0];
                        command[0] = 2;
                        CommandDisplay();
                    }
                }
            }
            else if (directionCommand < 120 && directionCommand > 60)
            {
                if (command[0] == 3)
                {
                    if (j == 5)
                    {
                        arrowPosition = left1;
                    }
                }
                else
                {
                    if (distanceCommand > unit * 50)
                    {
                        command[2] = command[1];
                        command[1] = command[0];
                        command[0] = 3;
                        CommandDisplay();
                    }
                }
            }
            else if (directionCommand < 300 && directionCommand > 240)
            {
                if (command[0] == 4)
                {
                    if (j == 5)
                    {
                        arrowPosition = left1;
                    }
                }
                else
                {
                    if (distanceCommand > unit * 50)
                    {
                        command[2] = command[1];
                        command[1] = command[0];
                        command[0] = 4;
                        CommandDisplay();
                    }
                }
            }
            skillCommand = command[2] * 100 + command[1] * 10 + command[0];
        }
        void CommandDisplay()
        {
            for (i = 0; i < 3; i++)
            {
                switch (command[i])
                {
                    case 1:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 2:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case 3:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 4:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 0, 270);
                        break;
                    default:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                }
            }
            time = 300;
        }
        void CommandColor()
        {
            arrow[2].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);
            arrow[1].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);
            arrow[0].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);
        }
    }
}