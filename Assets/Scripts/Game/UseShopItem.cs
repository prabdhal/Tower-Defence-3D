using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefence
{
    public class UseShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        PlayerStats playerStats;
        GameManager gameManager;
        GameItemManager gameItemManager;
        public Tooltip tooltip;

        public bool isGoldItem;
        public int rewardGoldRatio = 5;
        private bool usedGold;

        public bool isBombItem;

        public bool isHealthItem;
        public int rewardHealthRatio = 1;
        private bool usedHealth;

        public bool isFreezeItem;
        public int freezeDuration = 3;
        private bool usedFreeze;

        public Image image;
        public Sprite icon;
        public TextMeshProUGUI itemAmountText;

        private string tooltipInformation;

        private bool onMouseOver;
        private bool onMouseExit;


        private void Start()
        {
            playerStats = PlayerStats.instance;
            gameManager = GameManager.instance;
            gameItemManager = GameItemManager.instance;
            gameItemManager.FreezeDuration(freezeDuration);

            image.sprite = icon;

            if (isGoldItem)
            {
                itemAmountText.text = PlayerDataManager.GoldItemAmount.ToString();
                tooltip = GameObject.Find("GoldTooltip").GetComponent<Tooltip>();
            }
            if (isBombItem)
            {
                itemAmountText.text = PlayerDataManager.BombItemAmount.ToString();
                tooltip = GameObject.Find("BombTooltip").GetComponent<Tooltip>();
            }
            if (isHealthItem)
            {
                itemAmountText.text = PlayerDataManager.HealthItemAmount.ToString();
                tooltip = GameObject.Find("HealthTooltip").GetComponent<Tooltip>();
            }
            if (isFreezeItem)
            {
                itemAmountText.text = PlayerDataManager.FreezeTimeItemAmount.ToString();
                tooltip = GameObject.Find("FreezeTooltip").GetComponent<Tooltip>();
            }

            gameItemManager.usedBomb = false;
            usedGold = false;
            usedHealth = false;
            usedFreeze = false;
            if (tooltip != null)
            {
                TooltipInformation();
                tooltip.HideTooltip();
            }
        }

        private void Update()
        {
            if (isBombItem)
                itemAmountText.text = PlayerDataManager.BombItemAmount.ToString();

            if (onMouseOver)
            {
                if (tooltip != null) tooltip.ShowTooltip(tooltipInformation);
            }
            else
            {
                if (tooltip != null) tooltip.HideTooltip();
            }
        }

        public void GoldItem()
        {
            if (usedGold)
                return;

            usedGold = true;

            if (PlayerDataManager.GoldItemAmount <= 0)
                return;

            PlayerDataManager.GoldItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.GoldItemAmount.ToString();
            PlayerStats.Gold += rewardGoldRatio * (gameManager.levelToUnlock - 1);
        }

        public void BombItem()
        {
            if (gameItemManager.usedBomb)
                return;
          
            if (PlayerDataManager.BombItemAmount <= 0)
                return;

            GameItemManager.activeItem = true;
        }

        public void HealthItem()
        {
            if (usedHealth)
                return;

            usedHealth = true;

            if (PlayerDataManager.HealthItemAmount <= 0)
                return;

            PlayerDataManager.HealthItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.HealthItemAmount.ToString();
            playerStats.UpdateHealth(-rewardHealthRatio * (gameManager.levelToUnlock - 1));
        }

        public void FreezeItem()
        {
            if (usedFreeze)
                return;

            usedFreeze = true;

            if (PlayerDataManager.FreezeTimeItemAmount <= 0)
                return;

            PlayerDataManager.FreezeTimeItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.FreezeTimeItemAmount.ToString();
            GameItemManager.freezeEnemies = true;
        }
        
        private void TooltipInformation()
        {
            if (isGoldItem)
            {
                tooltipInformation = "Click to gain " + (gameManager.levelToUnlock - 1) * 10 + " gold. [Once per level]";
            }
            else if (isHealthItem)
            {
                tooltipInformation = "Click to gain " + (gameManager.levelToUnlock - 1) * 1 + " health. [Once per level]";
            }
            else if (isBombItem)
            {
                tooltipInformation = "Click to activate, then point and click anywhere on the enemy path to detonate. Click anywhere else to cancel. [Once per level]";
            }
            else if (isFreezeItem)
            {
                tooltipInformation = "Click to slow enemy units for " + gameItemManager.startTimer + " seconds. [Once per level]";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseOver = false;
        }
    }
}
