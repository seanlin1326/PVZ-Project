using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class SunFlower : PlantBase
    {
       private GameObject sunPrefab;
        //每次創造陽光之間的間隔
        [SerializeField] private float createSunFrequency=3f;
        // Start is called before the first frame update
        void Start()
        {
        sunPrefab = Resources.Load<GameObject>("Prefab/GameObj/Sun");
            InvokeRepeating(nameof(CreateSun), 3, createSunFrequency);
            Init();
        }

        // Update is called once per frame
        void Update()
        {

        }
       void CreateSun()
        {
            GameObject _spawnSunObj = Instantiate(sunPrefab,transform.position,Quaternion.identity);
            Sun _spawnSunScript = _spawnSunObj.GetComponent<Sun>();
            _spawnSunScript.InitForPlant();

        }
    }
}