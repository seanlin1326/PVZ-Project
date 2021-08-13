using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PvZBattleSystem
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { None,Planting,Shoveling }
        public static GameManager instance;
        //擁有陽光的數量
        private int sunOwnsNum;
        public GameState gamePlayState;
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

        public GameState GamePlayState
        {
            get { return gamePlayState; }
        }
        private void Awake()
        {
            if(instance !=null)
            {
                Destroy(instance);
                return;
            }
            instance = this;
            sunOwnsNum = 500;
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
        #region -- About GamePlayState --
        public void SwitchGamePlayState(GameState _newGameState)
        {
            if(gamePlayState==GameState.Shoveling)
            {
                ShovelPlantManager.instance.EndShovel();
            }
            else if(gamePlayState == GameState.Planting)
            {
                PlantManager.instance.EndPlant();
            }
            gamePlayState = _newGameState;
        }
        
        #endregion

        // Update is called once per frame
        void Update()
        {
          
        }
    }
}
