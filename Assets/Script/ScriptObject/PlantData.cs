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
        public Sprite plantCardSprite;
        public GameObject plantPrefab;
        public GameObject plantFollowMousePreviewPrefab;
        //用於提示種植植物時，放在哪個格子，以半透明表示
        public GameObject plantPlacePreviewPrefab;
        [TextArea]
        public string plantDescription;
        public float plantCD;
        public bool zombieCanNotEat;
        public int maxHp;
        //種植需要花費的陽光
        public int sunCost;
        public List<Vector2Int> allOccupyPoint;
    }
}
