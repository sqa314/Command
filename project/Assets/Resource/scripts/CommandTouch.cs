using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpecialMove
{
    public class CommandTouch : MonoBehaviour
    {
        //Startiscalledbeforethefirstframeupdate
        public GameObject[] arrow = new GameObject[3];
        public Vector2 leftstart;
        public Vector2 left1;
        public Vector2 leftend;
        public Vector2 arrowPosition;
        public Color color;
        public float distanceCommand;
        public float directionCommand;
        public float time;
        public float unit;
        public int i;
        public int j;
        public int finger1;
        public int finger2;
        static public int select;
        public int skillCommand;
        public int[] command = new int[3];
        static public int[] rawCommand = new int[5];
        public bool adjust;
        public bool set;
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            unit = Screen.width * 0.001f;
            CommandDisplay();
            j = 20;
            select = -1;
            InitializeSkill();
            color = arrow[0].GetComponent<Image>().color;
        }
        void FixedUpdate()
        {
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
            CommandColor();
            if (Input.touchCount < 2)
                finger1 = finger2 = 2;
            else
            {
                if (Input.GetTouch(0).position.x < Input.GetTouch(1).position.x)
                    finger1 = 0;
                else
                    finger1 = 1;
            }
            switch (select != -1)
            {
                case false:
                    skillCommand = 0;
                    if (Input.touchCount > finger1)
                    {
                        switch (Input.GetTouch(finger1).phase)
                        {
                            case TouchPhase.Began:
                                adjust = false;
                                break;
                            case TouchPhase.Moved:
                                adjust = false;
                                break;
                            case TouchPhase.Stationary:
                                adjust = false;
                                break;
                            case TouchPhase.Ended:
                                adjust = false;
                                break;
                        }
                    }
                    break;
                case true:
                    if (Input.touchCount > finger1)
                    {
                        left1 = Input.GetTouch(finger1).position;
                        switch (Input.GetTouch(finger1).phase)
                        {
                            case TouchPhase.Began:
                                arrowPosition = left1;
                                adjust = true;
                                break;
                            case TouchPhase.Moved:
                                distanceCommand = Vector2.Distance(arrowPosition, left1);
                                Commands();
                                break;
                            case TouchPhase.Stationary:
                                distanceCommand = Vector2.Distance(arrowPosition, left1);
                                Commands();
                                break;
                            case TouchPhase.Ended:
                                if (adjust)
                                {
                                    set = true;
                                    adjust = false;
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        void Update()
        {
            if (set && select != -1)
            {
                skillCommand = command[2] * 100 + command[1] * 10 + command[0];
                rawCommand[select] = skillCommand;
                Clear(select);
                set = false;
            }
        }
        void Commands()
        {
            if (directionCommand < 30 || directionCommand > 330)
            {
                switch (command[0])
                {
                    case 1:
                        if (j == 5)
                        {
                            arrowPosition = left1;
                        }
                        break;
                    default:
                        if (distanceCommand > unit * 50)
                        {
                            command[2] = command[1];
                            command[1] = command[0];
                            command[0] = 1;
                            CommandDisplay();
                        }
                        break;
                }
            }
            else if (directionCommand < 210 && directionCommand > 150)
            {
                switch (command[0])
                {
                    case 2:
                        if (j == 5)
                        {
                            arrowPosition = left1;
                        }
                        break;
                    default:
                        if (distanceCommand > unit * 50)
                        {
                            command[2] = command[1];
                            command[1] = command[0];
                            command[0] = 2;
                            CommandDisplay();
                        }
                        break;
                }
            }
            else if (directionCommand < 120 && directionCommand > 60)
            {
                switch (command[0])
                {
                    case 3:
                        if (j == 5)
                        {
                            arrowPosition = left1;
                        }
                        break;
                    default:
                        if (distanceCommand > unit * 50)
                        {
                            command[2] = command[1];
                            command[1] = command[0];
                            command[0] = 3;
                            CommandDisplay();
                        }
                        break;
                }
            }
            else if (directionCommand < 300 && directionCommand > 240)
            {
                switch (command[0])
                {
                    case 4:
                        if (j == 5)
                        {
                            arrowPosition = left1;
                        }
                        break;
                    default:
                        if (distanceCommand > unit * 50)
                        {
                            command[2] = command[1];
                            command[1] = command[0];
                            command[0] = 4;
                            CommandDisplay();
                        }
                        break;
                }
            }
        }
        void CommandDisplay()
        {
            for (i = 0; i < 3; i++)
            {
                switch (command[i])
                {
                    case 0:
                        arrow[i].transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
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
                }
            }
            time = 100;
        }
        void CommandColor()
        {
            arrow[0].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);
            arrow[1].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);
            arrow[2].GetComponent<Image>().color = new Color(color.r, color.g, color.b, time / 30f);

        }
        void Clear(int a)
        {
            for (i = 1; i < 5; i++)
            {
                if (a != i && rawCommand[a] == rawCommand[i])
                {
                    rawCommand[i] = 999;
                }
            }
        }
        void InitializeSkill()
        {
            Delivery.skillName[0] = "0";
            Delivery.skillName[1] = "1";
            Delivery.skillName[2] = "2";
            Delivery.skillName[3] = "3";
            Delivery.skillName[4] = "4";
        }
    }
}