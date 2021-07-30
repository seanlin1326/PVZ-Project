using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //下落的目標PosY
    private float downTargetPosY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //當陽光從天空初始化的方法
    public void InitForSky(float _downTargetPosY,float _createPosX,float _createPosY)
    {
        downTargetPosY = _downTargetPosY;
        transform.position = new Vector2(_createPosX, _createPosY);
    }
}
