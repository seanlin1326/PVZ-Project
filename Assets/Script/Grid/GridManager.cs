using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class GridManager : MonoBehaviour
    { 
        public static GridManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        public int gridheight=5;
        public int gridwidth=9;
        private List<Vector2> pointList = new List<Vector2>();
        private List<Grid> gridList = new List<Grid>();
        // Start is called before the first frame update
        void Start()
        {
            CreateGridScriptBaseList();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //GetGridPointByMouse();
            }
        } 
        #region -- 創建相關 --
        //創建碰撞體的形式創建網格
        private void CreateGridsBaseColl()
        {
            //創建一個預製體網格
            GameObject _prefabGrid = new GameObject();
            _prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1, 1.5f);
            _prefabGrid.transform.SetParent(transform);
            _prefabGrid.transform.position = transform.position;
            _prefabGrid.name = 0 + "-" + 0;
            for (int i = 0; i < gridheight; i++)
            {
                for (int j = 0; j < gridwidth; j++)
                {
                    GameObject _spawnGrid = Instantiate(_prefabGrid,
                                                        transform.position + new Vector3(1.33f * j, 1.63f * i), Quaternion.identity);
                    _spawnGrid.name = i + "-" + j;
                    _spawnGrid.transform.SetParent(transform);
                }
            }
            Destroy(_prefabGrid);
        }
      
        //基於座標List的形式創建網格
        private void CreateGridsBasePointList()
        {
            for (int i = 0; i < gridheight; i++)
            {
                for (int j = 0; j < gridwidth; j++)
                {
                    pointList.Add(transform.position + new Vector3(1.33f * j, 1.63f * i, 0));
                }
            }
        }
        //基於Grid腳本的形式創建網格
        private void CreateGridScriptBaseList()
        {
            for (int i = 0; i < gridheight; i++)
            {
                for (int j = 0; j < gridwidth; j++)
                {
                    gridList.Add(new Grid(new Vector2Int(i, j),
                                 transform.position + new Vector3(1.33f * j, 1.63f * i, 0),
                                 false));
                }
            }
        }
        #endregion
        #region  -- 計算網格座標 或是  單元網格 --
        //通過鼠標獲取網格座標點
        public Vector2 GetGridPointByMouse()
        {
            return GetGridPointByWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        public Vector2 GetGridPointByWorldPos(Vector2 _worldPos)
        {
            return GetGridByWorldPos(_worldPos).position;
        }
        public Grid GetGridByWorldPos(Vector2 _worldPos)
        {
            float _dis = 1000000;
            Grid _selectedGrid = null;
            for (int i = 0; i < gridList.Count; i++)
            {
                if (Vector2.Distance(_worldPos, gridList[i].position) < _dis)
                {
                    _dis = Vector2.Distance(_worldPos, gridList[i].position);
                    _selectedGrid = gridList[i];
                }
            }
            
            //Debug.Log(_selectedGrid.point);
            return _selectedGrid;
        }
        #endregion
        #region -- 植物種植相關 --
        //判斷當前的格子能不能種植植物 利用種植該植物需要格子來判斷
        public bool CanPlantJudgeByVector2IntList(Grid _selectedGrid, List<Vector2Int> _allNeedGridVectorIntList)
        {
            Vector2Int _selectedGridPoint = _selectedGrid.point;
            List<Grid> _allNeedGrid=new List<Grid>();
            //若_selectedGrid 不包含在gridList return false
            if (!gridList.Contains(_selectedGrid))
                return false;
            //將所有判斷需要的Grid，加入_allNeedGrid中
            foreach (var _vector2Int in _allNeedGridVectorIntList)
            {
                Vector2Int _needGridVector2Int = _selectedGridPoint + _vector2Int;
                //若需要的格子不合法直接回傳false
                if (_needGridVector2Int.x < 0 || _needGridVector2Int.x >= gridheight || _needGridVector2Int.y < 0 || _needGridVector2Int.y >= gridwidth)
                    return false;
                foreach (var _grid in gridList)
                {
                   
                    if (_grid.point== _needGridVector2Int)
                    {

                        _allNeedGrid.Add(_grid);
                    }
                }
            }
            foreach (var _grid in _allNeedGrid)
            {
                if (!_grid.CanPlant)
                    return false;
            }
            return true;
        } 
        public List<Grid> AllNeedGridsToPlant(Grid _selectedGrid, List<Vector2Int> _allNeedGridVectorIntList)
        {
            Vector2Int _selectedGridPoint = _selectedGrid.point;
            List<Grid> _allNeedGrid = new List<Grid>();
            //若_selectedGrid 不包含在gridList return false
            if (!gridList.Contains(_selectedGrid))
            {
          
                return null;
            }
            //將所有判斷需要的Grid，加入_allNeedGrid中
            foreach (var _vector2Int in _allNeedGridVectorIntList)
            {
           
                Vector2Int _needGridVector2Int = _selectedGridPoint + _vector2Int;
                Debug.Log(_needGridVector2Int+" ##");
                //若需要的格子不合法直接回傳false
                if (_needGridVector2Int.x < 0 || _needGridVector2Int.x >= gridheight || _needGridVector2Int.y < 0 || _needGridVector2Int.y >= gridwidth)
                {
                    Debug.Log("需要的格子不存在");
                    return null;
                }
                   
                foreach (var _grid in gridList)
                {

                    if (_grid.point == _needGridVector2Int)
                    {
                        _allNeedGrid.Add(_grid);
                    }
                }
            }
            foreach (var _grid in _allNeedGrid)
            {
                if (!_grid.CanPlant)
                {
                    Debug.Log("有需要的格子無法種植");
                    return null;
                }
            }
            foreach (var _grid in _allNeedGrid)
            {
                Debug.Log("haha " +_grid.point);
            }
            return _allNeedGrid;
            
        }
        #endregion
    }
}