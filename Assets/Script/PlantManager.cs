using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{

    public class PlantManager : MonoBehaviour
    {
        public static PlantManager instance;
        [SerializeField] Transform allPlantContaier;
       [SerializeField] private  PlantUICard currentPlantedCard;
        public bool planting;

        
       [SerializeField] GameObject followMousePlantPreviewPrefab;
       GameObject followMousePlantPreviewObj;

       
        GameObject plantPlacePreviewObj;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandlePlantInUpdate();
        }
        #region -- 種植相關邏輯 --
        public void HandlePlantInUpdate()
        {
            if (planting)
            {

                Vector3 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                followMousePlantPreviewObj.transform.position = new Vector3(_mousePoint.x, _mousePoint.y, 0);


                //如果鼠標距離網格比較近，需要在網格上生成一個透明的植物
                if (Vector2.Distance(_mousePoint, GridManager.instance.GetGridPointByMouse()) < 1.5f)
                {
                    if (followMousePlantPreviewObj.activeSelf)
                        followMousePlantPreviewObj.SetActive(false);
                    if (plantPlacePreviewObj == null)
                        plantPlacePreviewObj = Instantiate(currentPlantedCard.plantData.plantPlacePreviewPrefab, GridManager.instance.GetGridPointByMouse(), Quaternion.identity, PlantManager.instance.transform);
                    else
                        plantPlacePreviewObj.transform.position = GridManager.instance.GetGridPointByMouse();


                    //點擊滑鼠左鍵 以種植植物
                    if (Input.GetMouseButtonDown(0))
                    {
                        Plant();
                        EndPlant();
                    }  
                }
                //如果鼠標距離網格比較遠
                else
                {
                    if (plantPlacePreviewObj != null)
                    {
                        Destroy(plantPlacePreviewObj);
                        plantPlacePreviewObj = null;
                    }
                    if (!followMousePlantPreviewObj.activeSelf)
                        followMousePlantPreviewObj.SetActive(true);
                }
                //點擊滑鼠右鍵 以取消種植植物
                if (Input.GetMouseButtonDown(1))
                {
                    EndPlant();
                }
            }
        }

        //選擇一個新的植物卡片 來種植
    public void PickANewPlantCard(PlantUICard _plantCard)
        {
            planting = true;
            currentPlantedCard = _plantCard;
            DestoryFollowMousePlantPreview();
            DestroyPlantPlacePreview();
            followMousePlantPreviewObj = Instantiate(followMousePlantPreviewPrefab,transform);
            followMousePlantPreviewObj.GetComponent<SpriteRenderer>().sprite = _plantCard.plantData.plantSprite;
        }
        //種植行為
   public void Plant()
        {

            Instantiate(currentPlantedCard.plantData.plantPrefab, GridManager.instance.GetGridPointByMouse(), Quaternion.identity, allPlantContaier);
            currentPlantedCard.InCD = false;
        }
        //結束種植
    public void EndPlant()
        {
            currentPlantedCard = null;

            DestoryFollowMousePlantPreview();
            DestroyPlantPlacePreview();
            planting = false;

        }
        public void DestoryFollowMousePlantPreview()
        {
            if (followMousePlantPreviewObj != null)
            {
                Destroy(followMousePlantPreviewObj);
                followMousePlantPreviewObj = null;
            }
        }
        public void DestroyPlantPlacePreview()
        {
            if (plantPlacePreviewObj != null)
            {
                Destroy(plantPlacePreviewObj);
                plantPlacePreviewObj = null;
            }
        }
        #endregion
    }
}
