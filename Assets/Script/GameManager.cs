using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PvZBattleSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //擁有陽光的數量
        private int sunOwnsNum;

        public event Action OnSunOwnsNumChange;
        public event Action<int> OnSunOwnsNumChangeByInt;
        public int SunOwnsNum { 
            get => sunOwnsNum;
            set {
                sunOwnsNum = value;

                if (OnSunOwnsNumChangeByInt != null)
                    OnSunOwnsNumChangeByInt(sunOwnsNum);
                OnSunOwnsNumChange?.Invoke();
                } 
        }


        private void Awake()
        {
            if(instance !=null)
            {
                Destroy(instance);
                return;
            }
            instance = this;
            sunOwnsNum = 200;
        }
        // Start is called before the first frame update
        void Start()
        {
          
        }
        #region -- 有關陽光 --
        // 消費已經擁有的陽光
        public void ConsumeOwnsSun(int _consumeSunAmount)
        {
            if(sunOwnsNum >= _consumeSunAmount)
            {
                SunOwnsNum -= _consumeSunAmount;
            }
        }
        #endregion
        

        // Update is called once per frame
        void Update()
        {
          
        }
    }
}
