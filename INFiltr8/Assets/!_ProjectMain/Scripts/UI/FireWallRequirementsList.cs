using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class FireWallRequirementsList : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text amount;

        public void Init(string color, string reqAmount)
        {
            text.text = color;
            amount.text = reqAmount;
        }
    }
}