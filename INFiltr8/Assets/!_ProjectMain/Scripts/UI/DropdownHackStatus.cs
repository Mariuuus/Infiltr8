using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.LevelEditor.Types;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class DropdownHackStatus : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<TMP_Dropdown>().options.Clear();
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var val in Enum.GetValues(typeof(HackStatus)))
            {
                options.Add(new TMP_Dropdown.OptionData(val.ToString().Replace("Hacked", "")));
            }
            GetComponent<TMP_Dropdown>().options.AddRange(options);
        }
    }
}