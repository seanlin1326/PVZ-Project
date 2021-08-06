using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class ZombieManager : MonoBehaviour
    {

        public static ZombieManager instance;
        private List<ZombieBase> zombies;
       
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
