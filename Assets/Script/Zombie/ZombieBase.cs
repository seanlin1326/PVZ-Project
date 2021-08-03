using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ZombieBase : MonoBehaviour
    {
        protected Animator animator;
         protected Vector2 spawnPointOffset=new Vector2(3,0); 
        //速度 決定我幾秒走幾格 ex 0.5 為兩秒走完一格
        [SerializeField]protected float speed;
        // Start is called before the first frame update
       protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            Init();
        }
        private void Init()
        {
            int _spawnInRandomLine = Random.Range(0, GridManager.instance.Gridheight);
            transform.position = GridManager.instance.GetGridByVector2IntCoordinate(_spawnInRandomLine, GridManager.instance.Gridwidth-1).position+spawnPointOffset;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Move();
        }
        public void Move()
        {
            transform.Translate(new Vector2(-1.33f, 0) * Time.deltaTime * speed);
        }
    }
}
