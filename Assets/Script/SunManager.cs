using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class SunManager : MonoBehaviour
    {

        public static SunManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
         private GameObject sunPrefab;
        //創建陽光時候的座標Y
        [SerializeField] private float spawnSunPosY = 6f;

        //創建陽光時 最左和最右的X軸座標，在這個範圍內隨機
        [SerializeField] private float spawnSunMinPosX = -4f;
        [SerializeField] private float spawnSunMaxPosX = 6.6f;

        //陽光下落時 最高和最低的目的地Y軸
        [SerializeField] private float sunDropMinPosY = -3.7f;
        [SerializeField] private float sunDropMaxPosY = 3f;

         public Transform sunCollectedMoveTo;
        // Start is called before the first frame update
        void Start()
        {
            sunPrefab = Resources.Load<GameObject>("Prefab/GameObj/Sun");
            InvokeRepeating(nameof(CreateSun), 3f, 3f);
        }

        // Update is called once per frame
        void Update()
        {

        }
        //在空中生成陽光
        void CreateSun()
        {
            GameObject _spawnSunObj = Instantiate(sunPrefab, Vector3.zero, Quaternion.identity, transform);
            float _sunSpawnPosX = Random.Range(spawnSunMinPosX, spawnSunMaxPosX);
            float _sunDropDestinationPosY = Random.Range(sunDropMinPosY, sunDropMaxPosY);

            Sun _sunScript = _spawnSunObj.GetComponent<Sun>();
            _sunScript.InitForSky(_sunDropDestinationPosY, _sunSpawnPosX, spawnSunPosY);
        }
    }
}