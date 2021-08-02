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

        
      
       GameObject followMousePlantPreviewObj;
       GameObject plantPlacePreviewObj;
       [SerializeField] Color previewForbitPlantColor;
        [SerializeField] Color previewCanPlantColor;
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
                //讓植物preview 跟隨鼠標
                Vector3 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Grid _selectedGrid = GridManager.instance.GetGridByWorldPos(_mousePoint);
                followMousePlantPreviewObj.transform.position = new Vector3(_mousePoint.x, _mousePoint.y, 0);


                //如果鼠標距離網格比較近，需要在網格上生成一個透明的植物
                if (Vector2.Distance(_mousePoint, GridManager.instance.GetGridPointByMouse()) < 1f)
                {
                    if (followMousePlantPreviewObj.activeSelf)
                        followMousePlantPreviewObj.SetActive(false);
                    //若plantPlacePreviewObj未生成則生成，若已經生成則更改位置
                    if (plantPlacePreviewObj == null)
                        plantPlacePreviewObj = Instantiate(currentPlantedCard.plantData.plantPlacePreviewPrefab, GridManager.instance.GetGridPointByMouse(), Quaternion.identity, PlantManager.instance.transform);
                    else
                        plantPlacePreviewObj.transform.position = GridManager.instance.GetGridPointByMouse();
                    SpriteRenderer[] _plantPlacePreviewObjSR = plantPlacePreviewObj.GetComponentsInChildren<SpriteRenderer>();

                    //若此處可以種植植物(Grid沒有同時種植其他植物 也沒有不能種植的狀態)
                    if (GridManager.instance.CanPlantJudgeByVector2IntList(_selectedGrid,currentPlantedCard.plantData.allOccupyPoint))
                    {
                        foreach (var _sr in _plantPlacePreviewObjSR)
                        {
                            _sr.color = previewCanPlantColor;
                        }
                    }
                    else
                    {
                        foreach (var _sr in _plantPlacePreviewObjSR)
                        {
                            _sr.color = previewForbitPlantColor;
                        }
                    }
                    //點擊滑鼠左鍵 以種植植物
                    if (Input.GetMouseButtonDown(0))
                    {
                        bool _judge = GridManager.instance.CanPlantJudgeByVector2IntList(_selectedGrid, currentPlantedCard.plantData.allOccupyPoint);
                        if (_judge)
                        {
                            Debug.Log("可以種");
                        }
                        else
                            Debug.Log("不能種");
                        Plant(_selectedGrid);
                        
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
            followMousePlantPreviewObj = Instantiate(currentPlantedCard.plantData.plantFollowMousePreviewPrefab,transform);
           
        }
        //種植行為
   public void Plant(Grid _selectedGrid)
        {

            List<Grid> _allGridNeedPlant = GridManager.instance.AllNeedGridsToPlant(_selectedGrid,currentPlantedCard.plantData.allOccupyPoint);
            if (_allGridNeedPlant==null)
            {
                Debug.Log("當前格不能種歐");
                return;
            }
           GameObject _plantedPlant=  Instantiate(currentPlantedCard.plantData.plantPrefab, GridManager.instance.GetGridPointByMouse(), Quaternion.identity, allPlantContaier);
            currentPlantedCard.InCD = false;
            foreach (var _grid in _allGridNeedPlant)
            {
                _grid.PlantOnThisGrid(_plantedPlant);
            }
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
