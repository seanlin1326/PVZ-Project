using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class NormalZombie : ZombieBase
    {
        public enum BehaviourState { Move , Attack}
        public BehaviourState behaviourState;
        public float attackInterval = 0.8f;

        protected override void Start()
        {
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
                currentAttackPlant.GetComponent<PlantBase>().Hurt(100);
            }
            SwitchToWalkState();
        }
        private bool EndAttackStateJudge()
        {
            if (behaviourState != BehaviourState.Attack || currentAttackPlant == null)
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
    }
}