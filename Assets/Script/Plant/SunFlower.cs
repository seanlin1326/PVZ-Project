using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class SunFlower : MonoBehaviour
    {
        [SerializeField] private GameObject sunPrefab;
        // Start is called before the first frame update
        void Start()
        {
        sunPrefab = Resources.Load<GameObject>("Prefab/GameObj/Sun");
            InvokeRepeating(nameof(CreateSun), 3, 3);
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