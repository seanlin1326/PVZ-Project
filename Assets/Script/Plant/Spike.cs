using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Spike : PlantBase
    {
        public Transform detactEnemyPoint;
        public Vector2 detactEnemyBoxSize;
        public float attackInterval = 1f;
        public int attackDamage;
        private void Start()
        {
            Init();
            InvokeRepeating(nameof(AttackEnemy), 1.5f, attackInterval);
        }
        public override void InitPlantData()
        {
            plantData = Resources.Load<PlantData>("ScriptObject/Spike");
        }
        private void AttackEnemy()
        {
            //如果偵測到敵人就攻擊
            if (DetactEnemy())
            {
                Collider2D[] _enemyDetacted = Physics2D.OverlapBoxAll(detactEnemyPoint.transform.position, detactEnemyBoxSize, 0, enemyLayer);
                foreach (var _enemyCollider in _enemyDetacted)
                {
                    ZombieBase _zombieScript = _enemyCollider.gameObject.GetComponentInChildren<ZombieBase>();
                    if (_zombieScript == null)
                        continue;
                    _zombieScript.Hurt(attackDamage);
                }
            }
        }
        private bool DetactEnemy()
        {
            Collider2D[] _enemyDetacted = Physics2D.OverlapBoxAll(detactEnemyPoint.transform.position, detactEnemyBoxSize, 0, enemyLayer);
            if (_enemyDetacted.Length != 0)
            {
                Debug.Log("偵測到了");
                return true;
            }
            else
            {
                Debug.Log("沒偵測到");
                return false;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(detactEnemyPoint.position, detactEnemyBoxSize);
        }
    }
}
