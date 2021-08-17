using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class NormalZombie : ZombieBase
    {
        public enum BehaviourState { Move , Attack,Dead}
        public BehaviourState behaviourState;
        public Collider2D collider2D;
        [SerializeField] private GameObject deadDropHeadPrefab;
        [SerializeField]private Transform  dropHeadPoint;
        public int attackDamage = 60;
        public float attackInterval = 0.8f;
      
        protected override void Start()
        {
            collider2D = GetComponent<Collider2D>();
            base.Start();
            Init();
           

        }
        protected override void Init()
        {
            base.Init();
            behaviourState = BehaviourState.Move;
        }
        protected override void Update()
        {
           if(behaviourState== BehaviourState.Move)
            {
                Move();
            }
        }
        #region -- 有關移動 或 走路 --
        public virtual void Move()
        {
            nextGrid = GridManager.instance.GetGridByWorldPos(transform.position);
            transform.Translate(new Vector2(-1.33f, 0) * Time.deltaTime * speed);
        }
        void AnimatorInit()
        {
            int _randomWalk = Random.Range(1,4);
            switch (_randomWalk)
            {
                case 1:
                    animator.Play("NormalZombie-Walk1");
                    break;
                case 2:
                    animator.Play("NormalZombie-Walk2");
                    break;
                case 3:
                    animator.Play("NormalZombie-Walk3");
                    break;
            }
        }
        void SwitchToWalkState()
        {
            behaviourState = BehaviourState.Move;
            currentAttackPlant = null;
            animator.SetTrigger("walk");
        }
        #endregion
        #region -- 有關攻擊 --
        protected override void StartAttack()
        {

            StartCoroutine(AttckPlantCO());
        }
        IEnumerator AttckPlantCO()
        {
            bool _breakFlag = false;  
            while (!EndAttackStateJudge())
            {
               
                float _attackTimer = 0f;
                while(_attackTimer< attackInterval && !_breakFlag)
                {
                    _attackTimer += Time.deltaTime;
                    if (EndAttackStateJudge())
                    {
                        _breakFlag = true;
                        break;
                    }
                    yield return null;
                }
                if (_breakFlag)
                    break;
                currentAttackPlant.GetComponent<PlantBase>().Hurt(attackDamage);
            }
            if(!isDead)
            SwitchToWalkState();
        }
        #endregion
        private bool EndAttackStateJudge()
        {
            if (behaviourState != BehaviourState.Attack || currentAttackPlant == null || isDead)
                return true;
            else
                return false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
           
            if (collision.CompareTag("Plant") && collision.gameObject.GetComponent<PlantBase>() !=null && behaviourState==BehaviourState.Move)
            {
                Debug.Log("找到植物");
                behaviourState = BehaviourState.Attack;
               
                animator.SetTrigger("attack");
                currentAttackPlant = collision.gameObject;
                StartAttack();
            }
        }
        protected override void Dead()
        {
           
            base.Dead();
            behaviourState = BehaviourState.Dead;
            collider2D.enabled = false;
            Debug.Log("Dead");
            animator.SetTrigger("dead");
        }
        private void DropHead()
        {
            Instantiate(deadDropHeadPrefab, dropHeadPoint.position, Quaternion.identity);
        }
         public void DestorySelf()
        {
            Destroy(gameObject);
        }
       
    }
}