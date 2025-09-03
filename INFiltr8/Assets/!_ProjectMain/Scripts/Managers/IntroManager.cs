using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class IntroManager : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private MoveObject usbStickObject;

        [SerializeField] private MoveObject schlappyObject;

        [Header("Positions")] [SerializeField] private Transform startPositionUsb;
        [SerializeField] private Transform endPositionUsb;
        [SerializeField] private Transform startPositionSchlappy;
        [SerializeField] private Transform endPositionSchlappy;
        [SerializeField] private float duration = 1.5f;

        private void Start()
        {
            StartLerp();
            StartCoroutine(DelayDialog());
        }

        IEnumerator DelayDialog()
        {
            yield return new WaitForSeconds(duration);
            StartDialog();
        }

        public void StartDialog()
        {
            usbStickObject.Init(endPositionUsb);
            schlappyObject.Init(endPositionSchlappy);
            usbStickObject.OnSpeak(true);
            var lines = new List<string>();
            lines.AddRange(
                new string[]
                {
                    "HELLOOO!!!",
                    "Welcome to INFILTR8, rookie!",
                    "so... you don’t know what this is yet...",
                    "you’re here to learn, right?!",
                    "ABOUT THE DARK AND SHINY ART OF HACKING!",
                    "breaking laptops, infiltrating networks, stealing DATA!",
                    "and it all starts... right here.",
                }
            );

            DialogManager.Instance.StartDialoque(new DialogData(
                characters.USBStick.ToString(),
                DialoqType.NPC,
                characters.USBStick,
                lines), () =>
            {
                usbStickObject.OnSpeak(false);
                schlappyObject.OnSpeak(true);
                var lines = new List<string>();
                lines.AddRange(
                    new string[]
                    {
                        "heh, calm down, USB.",
                        "you should give our student a second to breathe.",
                        "...",
                        "...",
                        "alright, you seem ready now.",
                        "this is the start of your journey to become the greatest hacker out there.",
                        "don’t worry, we’ll guide you.",
                        ".",
                    }
                );
                DialogManager.Instance.StartDialoque(new DialogData(
                    characters.Schlappy.ToString(),
                    DialoqType.NPC,
                    characters.Schlappy,
                    lines), () =>
                {
                    usbStickObject.OnSpeak(true);
                    schlappyObject.OnSpeak(false);
                    var lines = new List<string>();
                    lines.AddRange(
                        new string[]
                        {
                            "YES, YES, YES!",
                            "the first battlefield is not some skyscraper or secret bunker...",
                            "it’s something WAY scarier...",
                            "A CHILD’S ROOM!",
                            "messy cables, sticky keyboards, forgotten computers—",
                            "the perfect cover for infiltration!",
                        }
                    );
                    DialogManager.Instance.StartDialoque(new DialogData(
                        characters.USBStick.ToString(),
                        DialoqType.NPC,
                        characters.USBStick,
                        lines), () =>
                    {
                        usbStickObject.OnSpeak(false);
                        schlappyObject.OnSpeak(true);
                        var lines = new List<string>();
                        lines.AddRange(
                            new string[]
                            {
                                "don’t be too dramatic, USB.",
                                "it’s just a normal room... with toys, paintings, and a dusty old computer waiting for us.",
                                "but behind that laptop hides a whole network of secrets.",
                                "you’ll learn how to sneak in, stay hidden, and extract data like a ghost.",
                            }
                        );
                        DialogManager.Instance.StartDialoque(new DialogData(
                            characters.Schlappy.ToString(),
                            DialoqType.NPC,
                            characters.Schlappy,
                            lines), () =>
                        {
                            usbStickObject.OnSpeak(true);
                            schlappyObject.OnSpeak(false);
                            var lines = new List<string>();
                            lines.AddRange(
                                new string[]
                                {
                                    "YES! A GHOST WITH WIRES!",
                                    "you won’t just be clicking buttons...!",
                                    "you’ll be breaking through firewalls like they’re made of paper!",
                                    "and if you mess up... BOOM—",
                                    "...okay, maybe not boom, but you get the point!",
                                }
                            );
                            DialogManager.Instance.StartDialoque(new DialogData(
                                characters.USBStick.ToString(),
                                DialoqType.NPC,
                                characters.USBStick,
                                lines), () =>
                            {
                                usbStickObject.OnSpeak(false);
                                schlappyObject.OnSpeak(true);
                                var lines = new List<string>();
                                lines.AddRange(
                                    new string[]
                                    {
                                        "what USB means is: mistakes happen.",
                                        "but that’s why we’re here.",
                                        "we’ll support you, teach you the tricks, and make sure you grow stronger with each hack.",
                                        "remember, patience and focus are just as powerful as speed and noise.",
                                    }
                                );
                                DialogManager.Instance.StartDialoque(new DialogData(
                                    characters.Schlappy.ToString(),
                                    DialoqType.NPC,
                                    characters.Schlappy,
                                    lines), () =>
                                {
                                    usbStickObject.OnSpeak(true);
                                    schlappyObject.OnSpeak(false);
                                    var lines = new List<string>();
                                    lines.AddRange(
                                        new string[]
                                        {
                                            "fine, fine, patience... blah blah blah.",
                                            "BUT ALSO ENERGY!!!",
                                            "alright, rookie—take a deep breath.",
                                            "we’re stepping into the child’s room next.",
                                            "ready to see where your first hack begins?",
                                        }
                                    );
                                    DialogManager.Instance.StartDialoque(new DialogData(
                                        characters.USBStick.ToString(),
                                        DialoqType.NPC,
                                        characters.USBStick,
                                        lines), () =>
                                    {
                                        usbStickObject.OnSpeak(true);
                                        schlappyObject.OnSpeak(false);
                                        var lines = new List<string>();
                                        lines.AddRange(
                                            new string[]
                                            {
                                                "BTW...",
                                                "We strongly recommend using a controller for this game.",
                                                "We think the controls make much more sense then!",
                                            }
                                        );
                                        DialogManager.Instance.StartDialoque(new DialogData(
                                            characters.Schlappy.ToString(),
                                            DialoqType.NPC,
                                            characters.Schlappy,
                                            lines), () =>
                                        {
                                            if(GameDataManager.Instance) GameDataManager.Instance.WatchedIntro();
                                            SceneManager.LoadScene("!_ProjectMain/Scenes/LevelSelection");
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }

        float elapsed;
        bool moving;

        void Update()
        {
            if (moving)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                usbStickObject.transform.position =
                    Vector3.Lerp(startPositionUsb.position, endPositionUsb.position, t);
                schlappyObject.transform.position =
                    Vector3.Lerp(startPositionSchlappy.position, endPositionSchlappy.position, t);

                if (t >= 1f) moving = false;
            }
        }

        void StartLerp()
        {
            elapsed = 0f;
            moving = true;
        }
    }
}