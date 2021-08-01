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
        //冷卻時間 幾秒可以放一次植物
        public float CDTime;
        //當前冷卻計時 用於冷卻時間的計算
        public float currentCDTimer;
        //是否可以放置植物
        private bool canPlace;
        //是否需要放置植物
        private bool wantPlant;
        //用來創建的植物
        private GameObject plant;
        //提示當前放置地點的透明植物
        private GameObject plantInGrid;
        public bool CanPlace
        {
            get => canPlace;
            set
            {
                canPlace = value;
                //如果不能放置
                if (!canPlace)
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
        public bool WantPlant
        {
            get => wantPlant;
            set { }
        }
        // Start is called before the first frame update
        void Start()
        {
            CanPlace = false;
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        #region -- 鼠標與卡片互動代碼 --
        //鼠標點擊的效果放置植物
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanPlace)
                return;
            Debug.Log("放置植物");
            PlantManager.instance.PickANewPlantCard(this);
        }
        //鼠標移入卡片區域
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanPlace)
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
            CanPlace = true;
        }

        
    }
}