using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace PvZBattleSystem
{
    public class Shovel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Image shovelImage;
        public void OnPointerClick(PointerEventData eventData)
        {
            ShovelPlantManager.instance.StartShovel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = new Vector2(1.05f, 1.05f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            if (GameManager.instance.GamePlayState == GameManager.GameState.Shoveling)
            {
                shovelImage.enabled = false;
            }
            else
                shovelImage.enabled = true;
        }

    }
}
