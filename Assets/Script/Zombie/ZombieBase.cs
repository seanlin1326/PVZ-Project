using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ZombieBase : MonoBehaviour
    {
        protected Animator animator;
         protected Vector2 spawnPointOffset=new Vector2(3,0);
        //protected int currentColumn;
        //protected int currentRow;
        //下個移動目標的格子
        protected Grid nextGrid;
        protected bool hurtAnimationBusy;
        public GameObject currentAttackPlant;
        private SpriteRenderer[] spriteRenderers;
        //速度 決定我幾秒走幾格 ex 0.5 為兩秒走完一格
        [SerializeField]protected float speed;
        [SerializeField] protected int hp;
        // Start is called before the first frame update
       protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            hp = 300;
        }
        protected virtual void Init()
        {
            int _spawnInRandomRow = Random.Range(0, GridManager.instance.Gridheight);
            //currentRow = _spawnInRandomRow;
            //currentColumn = GridManager.instance.Gridwidth - 1;
            nextGrid = GridManager.instance.GetGridByVector2IntCoordinate(_spawnInRandomRow, GridManager.instance.Gridwidth - 1);
            transform.position = nextGrid.position+spawnPointOffset;
        }



        // Update is called once per frame
        protected virtual void Update()
        {
           
        }

        #region -- 有關受傷 --
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
        protected IEnumerator HurtColorChangeLerpCO(float _wantTime, Color _targetColor)
        {
            hurtAnimationBusy = true;
            float _currentTime = 0;
            float _lerp;
            while (_currentTime < (_wantTime / 2))
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
        #endregion
        #region
        protected virtual void Dead()
        {
            Destroy(gameObject);
        }
        #endregion
        protected virtual void StartAttack()
        {
            
        }
    }
}
