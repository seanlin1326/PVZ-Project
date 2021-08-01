using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PvZBattleSystem
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public Text sunOwnsNumText;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            UpdateSunUIText(GameManager.instance.SunOwnsNum);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void UpdateSunUIText(int _sunOwns)
        {
            //Debug.Log("update");
            sunOwnsNumText.text = _sunOwns.ToString();
        }
        public Vector3 GetSunOwnsNumTextPos()
        {
            return sunOwnsNumText.transform.position;
        }
    }
}
