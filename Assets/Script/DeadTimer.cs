using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class DeadTimer : MonoBehaviour
    {
        [SerializeField]float liveTime = 2;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, liveTime);
        }

        
    }
}
