using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace PvZBattleSystem
{
    public class PlantUICard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
    {
        public PlantData plantData;
        //遮罩圖片的img組件
        [SerializeField]private Image maskImg;
        [SerializeField] private Image cardImage;
        //UI Text組件
        [SerializeField] private Text sunCostText;
        //冷卻時間 幾秒可以放一次植物
        public float CDTime;
        //當前冷卻計時 用於冷卻時間的計算
        public float currentCDTimer;
        //這個植物的卡牌消耗
        public int sunCost;
        //當前植物是否處於cd中
        private bool inCD;
      
        //用來創建的植物
        private GameObject plant;
        //提示當前放置地點的透明植物
        private GameObject plantInGrid;
        public bool InCD
        {
            get => inCD;
            set
            {
                inCD = value;
                //如果不能放置
                if (!inCD)
                {
                    //完全遮罩來表示不可以種植
                    maskImg.fillAmount = 1;
                    //開始冷卻
                    CDEnter();
                }
                else
                {
                    maskImg.fillAmount = 0;
                }
            }
        }
      
        // Start is called before the first frame update
        void Start()
        {
            InCD = true;
            Initial();
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        void Initial()
        {
            CDTime = plantData.plantCD;
            cardImage.sprite = plantData.plantCardSprite;
            sunCost = plantData.sunCost;
            sunCostText.text = sunCost.ToString();

        }
        #region -- 鼠標與卡片互動代碼 --
        //鼠標點擊的效果放置植物
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!InCD)
                return;
            if(GameManager.instance.SunOwnsNum < sunCost)
            {
                UIManager.instance.SunShortageAnimation();
                Debug.Log("陽光不夠");
                return;
            }
           
            //Debug.Log("放置植物");
            PlantManager.instance.PickANewPlantCard(this);
        }
        //鼠標移入卡片區域
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!InCD)
                return;
            transform.localScale = new Vector2(1.05f, 1.05f);
        }
        //鼠標移出卡片區域
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        #endregion
        //進入CD
        private void CDEnter()
        {
            
            //遮罩後開始計算冷卻
            StartCoroutine(CalculateCDCO());
        }
        //計算冷卻時間
       IEnumerator CalculateCDCO()
        {
            float _fillAmountModify = (1 / CDTime) * 0.1f;
            currentCDTimer = CDTime;
            while(currentCDTimer >= 0)
            {
                yield return new WaitForSeconds(0.1f);
                maskImg.fillAmount -= _fillAmountModify;
                currentCDTimer -= 0.1f;
            }
            //冷卻結束
            maskImg.fillAmount = 0;
            InCD = true;
        }

        
    }
}