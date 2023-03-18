using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TestTask.Characters;

namespace TestTask.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Animator _ownAnim;
        [SerializeField] private float _defaultTransitionTime;
        [SerializeField] private float _stayShownTime;
        [SerializeField] private Image _healthIndicator;
        [SerializeField] private Character target;

        private bool _isShown;
        private Sequence _fillSequence;
        private static int _animShowHash;

        private void Awake()
        {
            gameObject.SetActive(false);
            target.onHealthChanged += UpdateView;
            if (_animShowHash == 0)
                _animShowHash = Animator.StringToHash("Show");
        }

        private void OnDestroy()
        {
            target.onHealthChanged -= UpdateView;
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
            _ownAnim.SetBool(_animShowHash, false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _isShown = true;
            _ownAnim.SetBool(_animShowHash, true);
        }

        public void UpdateView()
        {
            Show();
            var healthRatio = target.currentHealth / target.maxHealth;
            if (_fillSequence != null)
                _fillSequence.Kill();
            _fillSequence = DOTween.Sequence();
            _fillSequence.Append(_healthIndicator.transform.DOScaleX(healthRatio, _defaultTransitionTime));
            if (healthRatio > 0)
                _fillSequence.AppendInterval(_stayShownTime);
            _fillSequence.AppendCallback(Hide);
        }
    }
}