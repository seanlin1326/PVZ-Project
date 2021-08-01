using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{

    public class PlantManager : MonoBehaviour
    {
        public static PlantManager instance;
       [SerializeField] private  PlantUICard currentPlantedCard;
        public bool planting;

        
       [SerializeField] GameObject followMousePlantPreviewPrefab;
       GameObject followMousePlantPreviewObj;

        [SerializeField] GameObject plantPlacePreviewPrefab;
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
            if (planting && followMousePlantPreviewObj!=null)
            {
                Vector3 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                followMousePlantPreviewObj.transform.position = new Vector3(_mousePoint.x, _mousePoint.y, 0);

                
                //如果鼠標距離網格比較近，需要在網格上生成一個透明的植物
                if (Vector2.Distance(_mousePoint, GridManager.instance.GetGridPointByMouse()) < 2 )
                {
                    if (followMousePlantPreviewObj.activeSelf)
                        followMousePlantPreviewObj.SetActive(false);
                    if (plantPlacePreviewObj == null)
                        plantPlacePreviewObj = Instantiate(plantPlacePreviewPrefab, GridManager.instance.GetGridPointByMouse(), Quaternion.identity, PlantManager.instance.transform);
                    else 
                        plantPlacePreviewObj.transform.position = GridManager.instance.GetGridPointByMouse();
                }
                //如果鼠標距離網格比較遠
                else
                {
                    if (plantPlacePreviewObj != null)
                        Destroy(plantPlacePreviewObj);
                    if (!followMousePlantPreviewObj.activeSelf)
                        followMousePlantPreviewObj.SetActive(true);
                }
            }
        }
        //選擇一個新的植物卡片 來種植
    public void PickANewPlantCard(PlantUICard _plantCard)
        {
            planting = true;
            followMousePlantPreviewObj = Instantiate(followMousePlantPreviewPrefab,transform);
            followMousePlantPreviewObj.GetComponent<SpriteRenderer>().sprite = _plantCard.plantData.plantSprite;
        }
    }
}
