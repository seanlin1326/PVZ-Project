using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        private void Awake()
        {
            if(instance != null)
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
    }
}