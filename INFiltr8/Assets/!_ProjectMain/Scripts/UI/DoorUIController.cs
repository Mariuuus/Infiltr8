using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class DoorUIController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject redGameObject;
        [SerializeField] private Slider redSlider;
        [SerializeField] private TMP_Text redCounter;
        
        [SerializeField] private GameObject greenGameObject;
        [SerializeField] private Slider greenSlider;
        [SerializeField] private TMP_Text greenCounter;

        [SerializeField] private GameObject blueGameObject;
        [SerializeField] private Slider blueSlider;
        [SerializeField] private TMP_Text blueCounter;

        [SerializeField] private GameObject yellowGameObject;
        [SerializeField] private Slider yellowSlider;
        [SerializeField] private TMP_Text yellowCounter;

        /*private void Start()
        {
            //example/test
            UpdateUI(
                new Dictionary<HackStatus, int>()
                {
                    { HackStatus.BlueHacked, 1 },
                    { HackStatus.GreenHacked, 2 },
                    { HackStatus.RedHacked, 0 },
                },
                new List<HackStatusAmount>()
                {
                    new HackStatusAmount(HackStatus.GreenHacked, 3),
                    new HackStatusAmount(HackStatus.RedHacked, 2),
                    new HackStatusAmount(HackStatus.BlueHacked, 1),
                });
        }*/

        public void UpdateUI(Dictionary<HackStatus, int> amounts, List<HackStatusAmount> inNeed) 
        {
            redGameObject.SetActive(false);
            greenGameObject.SetActive(false);
            blueGameObject.SetActive(false);
            yellowGameObject.SetActive(false);
            
            foreach (var element in inNeed)
            {
                switch (element.hackStatus)
                {
                    case HackStatus.BlueHacked:
                        blueGameObject.SetActive(true);
                        blueSlider.maxValue = element.amount;
                        blueCounter.text = element.amount.ToString();
                        break;
                    case HackStatus.GreenHacked:
                        greenGameObject.SetActive(true);
                        greenSlider.maxValue = element.amount;
                        greenCounter.text = element.amount.ToString();
                        break;
                    case HackStatus.RedHacked:
                        redGameObject.SetActive(true);
                        redSlider.maxValue = element.amount;
                        redCounter.text = element.amount.ToString();

                        break;
                    case HackStatus.YellowHacked:
                        yellowGameObject.SetActive(true);
                        yellowSlider.maxValue = element.amount;
                        yellowCounter.text = element.amount.ToString();
                        break;
                }
            }

            foreach (var element in amounts)
            {
                switch (element.Key)
                {
                    case HackStatus.BlueHacked:
                        if(blueGameObject.activeSelf)
                        {
                            blueSlider.value = element.Value;
                            blueCounter.text = (blueSlider.maxValue - element.Value).ToString();
                        }
                        break;
                    case HackStatus.GreenHacked:
                        if(greenGameObject.activeSelf)
                        {
                            greenSlider.value = element.Value;
                            greenCounter.text =  (greenSlider.maxValue - element.Value).ToString();
                        }
                        break;
                    case HackStatus.RedHacked:
                        if(redGameObject.activeSelf)
                        {
                            redSlider.value = element.Value;
                            redCounter.text =  (redSlider.maxValue - element.Value).ToString();
                        }
                        break;
                    case HackStatus.YellowHacked:
                        if(yellowGameObject.activeSelf)
                        {
                            yellowSlider.value = element.Value;
                            yellowCounter.text =  (yellowSlider.maxValue - element.Value).ToString();
                        }
                        break;
                }
            }
        }
    }
}