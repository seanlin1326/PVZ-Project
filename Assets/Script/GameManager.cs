using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //擁有陽光的數量
        private int sunOwnsNum;

        public int SunOwnsNum { 
            get => sunOwnsNum;
            set {
                sunOwnsNum = value;
                UIManager.instance.UpdateSunUIText(sunOwnsNum);
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
