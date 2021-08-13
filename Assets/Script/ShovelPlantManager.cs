using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ShovelPlantManager : MonoBehaviour
    {
        public static ShovelPlantManager instance;

        [SerializeField] private GameObject followMouseShovelPreviewPrefab;
        private GameObject followMouseShovelPreviewObj;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            HandlePlantInUpdate();
        }
        public void HandlePlantInUpdate()
        {
            if (GameManager.instance.GamePlayState == GameManager.GameState.Shoveling)
            {
                //讓植物preview 跟隨鼠標
                Vector3 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Grid _selectedGrid = GridManager.instance.GetGridByWorldPos(_mousePoint);
                followMouseShovelPreviewObj.transform.position = new Vector3(_mousePoint.x, _mousePoint.y, 0);
                SpriteRenderer followMouseShovelPreviewSR = followMouseShovelPreviewObj.GetComponentInChildren<SpriteRenderer>();
                if (_selectedGrid.occupyingPlantObj != null)
                    followMouseShovelPreviewSR.color = Color.red;
                else
                    followMouseShovelPreviewSR.color = Color.white;
                //若屬標在方格範圍內
                if (Vector2.Distance(_mousePoint, GridManager.instance.GetGridPointByMouse()) < 1f)
                {

                    //若plantPlacePreviewObj未生成則生成，若已經生成則更改位置
                    followMouseShovelPreviewObj.transform.position = GridManager.instance.GetGridPointByMouse();


                    //點擊滑鼠左鍵 以種植植物
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_selectedGrid.occupyingPlantObj != null)
                        {
                            followMouseShovelPreviewSR.color = Color.red;
                            PlantBase _plantScript = _selectedGrid.occupyingPlantObj.GetComponent<PlantBase>();
                            _plantScript.BeingShovel();
                            EndShovel();
                            GameManager.instance.SwitchGamePlayState(GameManager.GameState.None);
                        }
                    }
                }
                else
                {

                }
                if (Input.GetMouseButtonDown(1))
                {
                    EndShovel();
                    GameManager.instance.SwitchGamePlayState(GameManager.GameState.None);
                }
            }
        }
        public void StartShovel()
        {
            GameManager.instance.SwitchGamePlayState(GameManager.GameState.Shoveling);

            followMouseShovelPreviewObj = Instantiate(followMouseShovelPreviewPrefab, transform);

        }
        public void EndShovel()
        {
            Destroy(followMouseShovelPreviewObj);
           
        }
    }
}
