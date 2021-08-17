using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class WallNut : PlantBase
    {
        public enum BehaviourState {  Normal, MinorInjury, SeverelyInjury }
        public BehaviourState behaviourState;
        private int minorInjuryThresholdHp;
        private int severelyInjuryThresholdHp;
        private void Start()
        {
            Init();
            minorInjuryThresholdHp = Mathf.FloorToInt(maxHp * 2 / 3);
            severelyInjuryThresholdHp = Mathf.FloorToInt(maxHp * 1 / 3);
            behaviourState = BehaviourState.Normal;
            
        }
        public override void InitPlantData()
        {
            plantData = Resources.Load<PlantData>("ScriptObject/WallNut");
        }
        public override void Hurt(int _hurtValue)
        {
            base.Hurt(_hurtValue);
            if(behaviourState!= BehaviourState.MinorInjury && hp <= minorInjuryThresholdHp && hp > severelyInjuryThresholdHp)
            {
                behaviourState = BehaviourState.MinorInjury;
                animator.SetTrigger("hurtStateChange");
            }
            else if(behaviourState != BehaviourState.SeverelyInjury && hp <= severelyInjuryThresholdHp)
            {
                behaviourState = BehaviourState.SeverelyInjury;
                animator.SetTrigger("hurtStateChange");
            }
        }
    }
}
