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
        // sunOwnsNumText是否在演出動畫中
        bool sunOwnsNumTextAnimationBusy;
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
        #region -- 有關陽光的函式 --
        public void UpdateSunUIText(int _sunOwns)
        {
            //Debug.Log("update");
            sunOwnsNumText.text = _sunOwns.ToString();
        }
        public Vector3 GetSunOwnsNumTextPos()
        {
            return sunOwnsNumText.transform.position;
        }
        public void SunShortageAnimation()
        {
            if(!sunOwnsNumTextAnimationBusy)
            StartCoroutine(SunShortageAnimationCO());
        }
        IEnumerator SunShortageAnimationCO()
        {
            sunOwnsNumTextAnimationBusy = true;
            yield return UITextColorChangeLerpCO(sunOwnsNumText,0.5f,Color.red);
            sunOwnsNumTextAnimationBusy = false;
        }
        #endregion
        protected IEnumerator UITextColorChangeLerpCO(Text _text, float _wantTime, Color _targetColor)
        {
            Color _originalColor = _text.color;
            float _currentTime = 0;
            float _lerp;
            while (_currentTime < (_wantTime / 2))
            {
                _lerp = _currentTime / (_wantTime / 2);
                _currentTime += Time.deltaTime;
                _text.color = Color.Lerp(_originalColor, _targetColor, _lerp);
                yield return null;

            }
            _currentTime = 0;
            _lerp = 0;
            while (_currentTime < (_wantTime / 2))
            {
                _lerp = _currentTime / (_wantTime / 2);
                _currentTime += Time.deltaTime;

                _text.color = Color.Lerp(_targetColor, _originalColor, _lerp);
                yield return null;
            }

        }
    }
}
