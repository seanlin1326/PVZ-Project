using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Grid : MonoBehaviour
    {
        public Grid(Vector2 point, Vector2 position, bool havePlant)
        {
            this.point = point;
            this.position = position;
            this.havePlant = havePlant;
        }
        //座標點 (0,1) (1,1)
        public Vector2 point;

        //世界座標
        public Vector2 position;

        //是否有植物佔據這個格子
        public bool havePlant;

      
    }
}
