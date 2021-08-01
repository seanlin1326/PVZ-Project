using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    [CreateAssetMenu(fileName = "new plant data", menuName = "PvZBattleSystem/PlantData")]
    public class PlantData : ScriptableObject
    {
        public string plantName;
        public Sprite plantSprite;
        public GameObject plantPrefab;
        //用於提示種植植物時，放在哪個格子，以半透明表示
        public GameObject plantPlacePreviewPrefab;
        [TextArea]
        public string plantDescription;

    }
}
