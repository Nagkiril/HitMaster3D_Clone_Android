using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class StartLevelUI : MonoBehaviour
    {
        [SerializeField] private Animator _ownAnim;
        [SerializeField] private Button _tapScreen;

        public static event Action onScreenTapped;
        private static StartLevelUI _instance;

        private int _animShowHash;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _animShowHash = Animator.StringToHash("Show");
                _tapScreen.onClick.AddListener(OnScreenTap);
            }
            else
            {
                Debug.LogWarning("There has to be only 1 StartLevelUI on the scene! Destroying 2nd instance.");
                Destroy(this);
            }
        }

        private void OnScreenTap()
        {
            Hide();
            onScreenTapped?.Invoke();
        }

        private void Show()
        {
            _tapScreen.gameObject.SetActive(true);
            _ownAnim.SetBool(_animShowHash, true);
        }

        private void Hide()
        {
            _ownAnim.SetBool(_animShowHash, false);
            _tapScreen.gameObject.SetActive(false);
        }

        public static void ShowScreen()
        {
            _instance.Show();
        }
    }
}