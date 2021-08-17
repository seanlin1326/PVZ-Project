using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Peashooter_Bullet : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        [SerializeField] private GameObject bulletHitEffectPrefab; 
        [SerializeField] float speed = 300;
        [SerializeField] int attackDamage = 50;
        //子彈已經不能打人了
        bool bulletNoThreat;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Vector3.right * 300);
        }

        // Update is called once per frame
        void Update()
        {
            DestroyJudge();
        }

        void DestroyJudge()
        {
            if (transform.position.x > 6) {

                BulletDestroy();
                    }
        }
        private void BulletDestroy()
        {
            bulletNoThreat = true;
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Zombie") && !bulletNoThreat)
            {
                Debug.Log("打到殭屍了");
                rigidbody2D.velocity = Vector2.zero;
                ZombieBase _zombieScript = collision.gameObject.GetComponent<ZombieBase>();
                if (_zombieScript != null)
                    _zombieScript.Hurt(attackDamage);
                Instantiate(bulletHitEffectPrefab, transform.position, Quaternion.identity);
                BulletDestroy();
            }
        }
    }
}
