using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ThirdPeaShooter_P800 : PlantBase
    {

       [SerializeField] List<Transform> firePoints;
        [SerializeField] List<Transform> detectEnemyStartPoints;
       [SerializeField] GameObject bulletPrefab;
       
        public float detectEnemyMaxPosX = 5;
        public float attackInterval = 1f;
        private void Start()
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
            if (DetactEnemy())
            {
                foreach (var _point in firePoints)
                {
                    Instantiate(bulletPrefab, _point.position, Quaternion.identity);
                }
            }
        }
        private bool DetactEnemy()
        {
         
            for(int i=0;i < detectEnemyStartPoints.Count ;i++)
            {
                
                float _detactDistance = new Vector2(detectEnemyMaxPosX - detectEnemyStartPoints[i].position.x, 0).magnitude;
                RaycastHit2D _hitInfo = Physics2D.Raycast(detectEnemyStartPoints[i].position, Vector2.right, _detactDistance, enemyLayer);
                if (_hitInfo.collider != null)
                {
                    //Debug.Log("打到了殭屍 " + _hitInfo.collider.gameObject.name);
                    Debug.DrawLine(detectEnemyStartPoints[i].position, _hitInfo.point, Color.red);
                    return true;
                }
                else
                {
                    Debug.DrawRay(detectEnemyStartPoints[i].position, new Vector2(detectEnemyMaxPosX - detectEnemyStartPoints[i].position.x, 0), Color.green);
                   
                }
            }
            return false;
          
        }
    }
}
