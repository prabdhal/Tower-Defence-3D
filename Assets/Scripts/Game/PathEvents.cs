using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class PathEvents : MonoBehaviour
    {
        GameItemManager gameItemManager;
        public GameObject bombPrefab;

        public void Start()
        {
            gameItemManager = GameItemManager.instance;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Utils.Utilities.IsPointerOverUIObject())
                return;
            
            if (GameItemManager.activeItem)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    gameItemManager.UsedBomb();
                    PlayerDataManager.BombItemAmount -= 1;
                    GameItemManager.activeItem = false;

                    Instantiate(bombPrefab, hit.point, bombPrefab.transform.rotation);
                }
            }
        }
    }
}
