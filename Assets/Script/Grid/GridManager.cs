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
        //創建碰撞體的形式創建網格
        private void CreateGridsBaseColl()
        {
            //創建一個預製體網格
            GameObject _prefabGrid = new GameObject();
            _prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1, 1.5f);
            _prefabGrid.transform.SetParent(transform);
            _prefabGrid.transform.position = transform.position;
            _prefabGrid.name = 0 + "-" + 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    pointList.Add(transform.position + new Vector3(1.33f * j, 1.63f * i, 0));
                }
            }
        }
        //基於Grid腳本的形式創建網格
        private void CreateGridScriptBaseList()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    gridList.Add(new Grid(new Vector2(i, j),
                                 transform.position + new Vector3(1.33f * j, 1.63f * i, 0),
                                 false));
                }
            }
        }
        //通過鼠標獲取網格座標點
        public Vector2 GetGridPointByMouse()
        {
            return GetGridPointByWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        public Vector2 GetGridPointByWorldPos(Vector2 _worldPos)
        {
            float _dis = 1000000;
            Vector2 _selectedGridPos = Vector2.zero;
            for (int i = 0; i < gridList.Count; i++)
            {
                if (Vector2.Distance(_worldPos, gridList[i].position) < _dis)
                {
                    _dis = Vector2.Distance(_worldPos, gridList[i].position);
                    _selectedGridPos = gridList[i].position;
                }
            }
            Debug.Log(_selectedGridPos);
            return _selectedGridPos;
        }
    }
}