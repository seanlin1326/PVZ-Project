using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PvZBattleSystem
{
    public class Sun : MonoBehaviour
    {
        public enum SunState { Init,Dropping, Idle, Collected}
        public SunState sunState;
        //蒐集此陽光能獲取的陽光數量
        private int collectSunValue = 50;
        //下落的目標PosY
        private float dropDestinationPosY;
        //當太陽到達目的地後，等待多久自動銷毀
        [SerializeField]private float destroyTimer=4f;
        // Start is called before the first frame update
        void Start()
        {
           
          
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        #region -- Init Function --
        //當陽光從天空生成，初始化的方法
        #region -- Sky Drop Init --
        public void InitForSky(float _downDestinationPosY, float _createPosX, float _createPosY)
        {
            sunState = SunState.Init;
            dropDestinationPosY = _downDestinationPosY;
            transform.position = new Vector2(_createPosX, _createPosY);
            StartCoroutine(SkySunDropCO());
        }
        IEnumerator SkySunDropCO()
        {
            sunState = SunState.Dropping;
            while (sunState == SunState.Dropping)
            {
                if (transform.position.y > dropDestinationPosY)
                {
                    transform.Translate(Vector3.down * Time.deltaTime);
                }
                else
                {
                    sunState = SunState.Idle;
                }
                yield return null;
            }
            if (sunState == SunState.Collected)
                yield break;
            yield return DeadTimerCO();
        }
        #endregion
        //當陽光從植物生成，初始化的方法
        #region -- Plant Spawn Init --
        public void InitForPlant()
        {
            //讓陽光進行跳躍動畫
            JumpAnimation();
        }
        public void JumpAnimation()
        {
            StartCoroutine(DoJump());
        }
        private IEnumerator DoJump()
        {
            bool _isLeft = Random.Range(0, 2) == 0;
            Vector3 _startPos = transform.position;
            float _xOffset = (_isLeft) ? -0.01f : 0.01f;
          
                while (transform.position.y <= _startPos.y + 1)
                {
                yield return null;
                    transform.Translate(new Vector3(_xOffset, 0.05f, 0));

                   
                }
            while (transform.position.y >= _startPos.y)
            {
                yield return null;
                transform.Translate(new Vector3(_xOffset, -0.05f, 0));
            }
            yield return DeadTimerCO();

        }
        #endregion

        #endregion
        #region -- About Collected --
        //鼠標點擊陽光的時候，增加遊戲管理器中的陽光數量並且銷毀自身
        private void OnMouseDown()
        {
            GameManager.instance.SunOwnsNum += collectSunValue;
         Vector3 _sunOwnsNumTextPos = Camera.main.ScreenToWorldPoint(UIManager.instance.GetSunOwnsNumTextPos());
            _sunOwnsNumTextPos = new Vector3(_sunOwnsNumTextPos.x, _sunOwnsNumTextPos.y, 0);
            FlyAnimation(_sunOwnsNumTextPos);
        }
        private void FlyAnimation(Vector3 _destinationPos)
        {
            StartCoroutine(DoFly(_destinationPos));
        }
        private IEnumerator DoFly(Vector3 _destinationPos)
        {
            Vector3 _direction = (_destinationPos - transform.position).normalized;
            while (Vector3.Distance(_destinationPos,transform.position) > 0.5f)
            {
                yield return null;
                transform.Translate(_direction);
            }
            Destroy(gameObject);
        }
        #endregion
        #region -- About Dead --
        //摧毀太陽
        private void DestroySun()
        {
            Destroy(gameObject);
        }
        IEnumerator DeadTimerCO()
        {
            yield return new WaitForSeconds(destroyTimer);
            if (sunState == SunState.Collected)
                yield break;
            DestroySun();
        }
        #endregion
    }
}
