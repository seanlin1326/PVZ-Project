using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    //植物基類
    public abstract class PlantBase :MonoBehaviour
    {
        protected Animator animator;
        //當前植物所在的網格組
        protected List<Grid> currentOccupyGrids=new List<Grid>();
        protected PlantData plantData;
        [SerializeField]protected int maxHp;
        [SerializeField]protected int hp;
        protected SpriteRenderer[] spriteRenderers;
        //受傷動畫執行中
        bool hurtAnimationBusy;
        //正常僵屍碰到此植物的碰撞體會不會開吃，
        public bool zombieCanNotEat;
        [SerializeField]protected LayerMask enemyLayer;
        protected virtual void Init()
        {
            InitPlantData();
            animator = GetComponent<Animator>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            maxHp = plantData.maxHp;
            hp = maxHp;
            zombieCanNotEat = plantData.zombieCanNotEat;
        }
        //設定指派 plantData(ScriptObj 資料)
        public abstract void InitPlantData();
        

        
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
        public virtual void Hurt(int _hurtValue) 
        {
            Debug.Log("受傷");
            hp -= _hurtValue;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            if (!hurtAnimationBusy)
            {
                StartCoroutine(HurtColorChangeLerpCO(0.1f, new Color32(255, 0, 255, 255)));
            }
        }
        protected IEnumerator HurtColorChangeLerpCO(float _wantTime,Color _targetColor)
        {
            hurtAnimationBusy = true;
            float _currentTime = 0;
            float _lerp;
            while (_currentTime <  (_wantTime/2))
            {
                _lerp = _currentTime / (_wantTime / 2);
                _currentTime += Time.deltaTime;
                foreach (var _sr in spriteRenderers)
                {
                    _sr.color = Color.Lerp(Color.white, _targetColor, _lerp);
                    yield return null;
                }
            }
            _currentTime = 0;
            _lerp = 0;
            while (_currentTime < (_wantTime / 2))
            {
                _lerp = _currentTime / (_wantTime / 2);
                _currentTime += Time.deltaTime;
                foreach (var _sr in spriteRenderers)
                {
                    _sr.color = Color.Lerp(_targetColor, Color.white, _lerp);
                    yield return null;
                }
            }
            hurtAnimationBusy = false;
        }

        public void Dead()
        {
            foreach (var _grid in currentOccupyGrids)
            {
                _grid.RemovePlantOnThisGrid();
            }
            Destroy(gameObject);
        }
        //被剷掉
        public void BeingShovel()
        {
            foreach (var _grid in currentOccupyGrids)
            {
                _grid.RemovePlantOnThisGrid();
            }
            Destroy(gameObject);
        }

    }
}
