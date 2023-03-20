using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI.Modules
{
    public class StartLevelScreen : MonoBehaviour, IModularInputHandler
    {
        [SerializeField] private Animator _ownAnim;
        [SerializeField] private Button _tapScreen;

        public event Action onScreenTapped;

        private int _animShowHash;

        public void Initialize()
        {
            _animShowHash = Animator.StringToHash("Show");
            _tapScreen.onClick.AddListener(OnScreenTap);
        }

        private void OnScreenTap()
        {
            Hide();
            onScreenTapped?.Invoke();
        }

        public void Show()
        {
            _tapScreen.gameObject.SetActive(true);
            _ownAnim.SetBool(_animShowHash, true);
        }

        public void Hide()
        {
            _ownAnim.SetBool(_animShowHash, false);
            _tapScreen.gameObject.SetActive(false);
        }
    }
}