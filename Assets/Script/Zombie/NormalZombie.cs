using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class NormalZombie : ZombieBase
    {
       

        protected override void Start()
        {
            base.Start();
            AnimatorInit();

        }
        protected override void Update()
        {
            base.Update();
        }
        void AnimatorInit()
        {
            int _randomWalk = Random.Range(1,4);
            switch (_randomWalk)
            {
                case 1:
                    animator.Play("NormalZombie-Walk1");
                    break;
                case 2:
                    animator.Play("NormalZombie-Walk2");
                    break;
                case 3:
                    animator.Play("NormalZombie-Walk3");
                    break;
            }
        }
      
    }
}