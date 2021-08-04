using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    //植物基類
    public abstract class PlantBase :MonoBehaviour
    {
        //當前植物所在的網格組
        protected List<Grid> currentOccupyGrids=new List<Grid>();
        [SerializeField]protected int hp;

        protected virtual void Init()
        {
            hp = 400;
        }
        public List<Grid> AllOccupyingGrid
        {
            get
            {
                List<Grid> _gridTempContiner = new List<Grid>();
                foreach (var _grid in currentOccupyGrids)
                {
                    _gridTempContiner.Add(_grid);
                }
                return _gridTempContiner;
            }
        }
        //將當前佔據的格子賦值
        public virtual void AsignAllOccupyGrids(List<Grid> _asignGrid)
        {
            currentOccupyGrids.Clear();
            foreach (var _grid in _asignGrid)
            {
                currentOccupyGrids.Add(_grid);
            }
        }
        //被殭屍攻擊時 受傷的方法
        public void Hurt(int _hurtValue) 
        {
            Debug.Log("受傷");
            hp -= _hurtValue;
            if (hp <= 0)
            {
                Dead();
            }
        }
        public void Dead()
        {
            foreach (var _grid in currentOccupyGrids)
            {
                _grid.RemovePlantOnThisGrid();
            }
            Destroy(gameObject);
        }

    }
}
