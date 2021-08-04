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

        public GameObject currentAttackPlant;
        //速度 決定我幾秒走幾格 ex 0.5 為兩秒走完一格
        [SerializeField]protected float speed;
        // Start is called before the first frame update
       protected virtual void Start()
        {
            animator = GetComponent<Animator>();
           
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
      

        protected virtual void StartAttack()
        {
            
        }
    }
}
