using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ZombieManager : MonoBehaviour
    {

        public static ZombieManager instance;
        private List<ZombieBase> zombies;
        [SerializeField] private GameObject zombiePrefab;
        [SerializeField] private List<Transform> spawnZombiePoints=new List<Transform>();
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            SpawnZombie(0);
            SpawnZombie(1);
            SpawnZombie(2);
            SpawnZombie(3);
            SpawnZombie(4);
            
            
        }

        public void SpawnZombie(int _spawnLine)
        {
            if (_spawnLine < 0 || _spawnLine >= spawnZombiePoints.Count)
                return;
            Instantiate(zombiePrefab,spawnZombiePoints[_spawnLine].position,Quaternion.identity);
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void AddZombie(ZombieBase _zombie)
        {
            zombies.Add(_zombie);
        }
    }
}
