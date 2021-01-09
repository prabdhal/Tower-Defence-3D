using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class GameItemManager : MonoBehaviour
    {
        #region Singleton

        public static GameItemManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of GameItemManager found!");
                return;
            }

            instance = this;
        }
        #endregion

        public static bool activeItem;
        public static bool freezeEnemies;

        [HideInInspector]
        public float startTimer;
        private float timer;
        public bool usedBomb;

        private void Update()
        {
            if (freezeEnemies)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = startTimer;
                    freezeEnemies = false;
                }
            }
        }

        public void FreezeDuration(int duration)
        {
            startTimer = duration;
            timer = startTimer;
        }

        public void UsedBomb()
        {
            usedBomb = true;
        }
    }
}
