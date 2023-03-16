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

        // Start is called before the first frame update
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
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
            _ownAnim.SetBool("Show", true);
        }

        private void Hide()
        {
            _ownAnim.SetBool("Show", false);
            _tapScreen.gameObject.SetActive(false);
        }

        public static void ShowScreen()
        {
            _instance.Show();
        }
    }
}