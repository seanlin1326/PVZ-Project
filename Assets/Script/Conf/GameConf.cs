using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    [CreateAssetMenu(fileName ="GameConf",menuName = "GameConf")]
    public class GameConf : MonoBehaviour
    {
        [Tooltip("陽光")]
        public GameObject sun;
        [Tooltip("向日葵")]
        public GameObject sunFlower;
    }
}
