using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSkyManager : MonoBehaviour
{
    [SerializeField] private GameObject sunPrefab;
    //創建陽光時候的座標Y
   [SerializeField]private float createSunPosY=6f;

    //創建陽光時 最左和最右的X軸座標，在這個範圍內隨機
   [SerializeField]private float createSunMinPosX = -4f;
   [SerializeField]private float createSunMaxPosX = 6.6f;

    //陽光下落時 最高和最低的目的地Y軸
    [SerializeField]private float sunDownMinPosY = -3.7f;
    [SerializeField]private float sunDownMaxPosY = 3f;

    // Start is called before the first frame update
    void Start()
    {
        sunPrefab = Resources.Load<GameObject>("Prefab/Sun");
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
    }
}
