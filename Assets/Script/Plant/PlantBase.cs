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

    }
}
