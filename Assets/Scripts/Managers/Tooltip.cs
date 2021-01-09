using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TowerDefence
{
    public class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI tooltipText;
        
        public void ShowTooltip(string tooltipString)
        {
            gameObject.SetActive(true);

            tooltipText.text = tooltipString.ToString();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}