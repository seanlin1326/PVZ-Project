using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Grid : MonoBehaviour
    {
        public Grid(Vector2Int _point, Vector2 _position, bool _plantOccupying)
        {
            coordinate = _point;
            position = _position;
            plantOccupying = _plantOccupying;
        }
        //座標點 (0,1) (1,1)
        public Vector2Int coordinate;

        //世界座標
        public Vector2 position;

        //是否有植物佔據這個格子
        public bool plantOccupying;
        public bool CanPlant
        {
            get
            {
                return !plantOccupying;
            }
        }
        public GameObject occupyingPlantObj;
        public void PlantOnThisGrid(GameObject _plantedPlant)
        {
            if (!CanPlant)
            {
                Debug.Log("不能種歐");
                return;
            }
            occupyingPlantObj = _plantedPlant;
            plantOccupying = true;
        }
        public void RemovePlantOnThisGrid()
        {
            if (!plantOccupying || occupyingPlantObj==null)
            {
                Debug.Log("已經清空了 或有錯誤");
                return;
            }
            occupyingPlantObj = null;
            plantOccupying = false;

        }
      
    }
}
