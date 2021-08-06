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
        protected SpriteRenderer[] spriteRenderers;
        //受傷動畫執行中
        bool hurtAnimationBusy;
        [SerializeField]protected LayerMask enemyLayer;
        protected virtual void Init()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
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

    }
}
