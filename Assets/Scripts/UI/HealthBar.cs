using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestTask.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Animator ownAnim;
        [SerializeField] float defaultTransitionTime;
        [SerializeField] float stayShownTime;
        [SerializeField] Image healthIndicator;

        bool _isShown;
        Sequence _fillSequence;

        private void Awake()
        {
            gameObject.SetActive(false);
        }


        private void Update()
        {
            if (_isShown)
            {
                transform.forward = -1f * Camera.main.transform.forward;
            }
        }

        //Would be nice to separate animation events into a specific script dedicated to them; I'm only using animation events once here in the prototype, however, so I keep it as-is
        private void AnimEventHideFinish()
        {
            gameObject.SetActive(false);
        }

        public void Hide()
        {
            _isShown = false;
            ownAnim.SetBool("Show", false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _isShown = true;
            ownAnim.SetBool("Show", true);
        }

        public void SetValue(float value, float animTime = -1f)
        {
            Show();
            if (_fillSequence != null)
                _fillSequence.Kill();
            if (animTime == -1f)
                animTime = defaultTransitionTime;
            if (animTime > 0f)
            {
                _fillSequence = DOTween.Sequence();
                _fillSequence.Append(healthIndicator.transform.DOScaleX(value, animTime));
                _fillSequence.AppendInterval(stayShownTime);
                _fillSequence.AppendCallback(Hide);
            } else
            {
                healthIndicator.fillAmount = value;
            }
        }
    }
}