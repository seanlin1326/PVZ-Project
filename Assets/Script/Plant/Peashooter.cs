using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Peashooter : PlantBase
    {
        //開射子彈點
        [SerializeField]Transform firePoint;
        [SerializeField]Transform detactEnemyStartPoint;
        [SerializeField] GameObject bulletPrefab;
        
        public float detectEnemyMaxPosX = 5;
        public float attackInterval = 1f;
        //攻擊間隔過了
        public bool canAttack;
        // Start is called before the first frame update
        void Start()
        {
           
            Init();
            Physics2D.queriesStartInColliders = false;
            InvokeRepeating(nameof(AttackEnemy), 1.5f, attackInterval);
            detectEnemyMaxPosX = 5;
        }

        // Update is called once per frame
        void Update()
        {
            DetactEnemy();
        }
        private void AttackEnemy()
        {
            //如果偵測到敵人就攻擊
            if(DetactEnemy())
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            }
        }
        
        private bool DetactEnemy()
        {
            float _detactDistance = new Vector2(detectEnemyMaxPosX - detactEnemyStartPoint.position.x, 0).magnitude;
            //Debug.Log(_detactDistance);
            RaycastHit2D _hitInfo = Physics2D.Raycast(firePoint.position,Vector2.right, _detactDistance, enemyLayer);
            if(_hitInfo.collider !=null)
            {
                //Debug.Log("打到了殭屍 " + _hitInfo.collider.gameObject.name);
                Debug.DrawLine(firePoint.position, _hitInfo.point, Color.red);
                return true;
            }
            else
            {
                Debug.DrawRay(firePoint.position, new Vector2(detectEnemyMaxPosX - firePoint.position.x, 0), Color.green);
                return false;
            }
        }

        public override void InitPlantData()
        {
            plantData = Resources.Load<PlantData>("ScriptObject/peaShooter");
        }
    }
}