using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class StartLevelUI : MonoBehaviour
    {
        [SerializeField] Animator ownAnim;
        [SerializeField] Button tapScreen;

        public static event Action OnScreenTapped;
        static StartLevelUI _instance;

        // Start is called before the first frame update
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                tapScreen.onClick.AddListener(OnScreenTap);
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
            OnScreenTapped?.Invoke();
        }

        private void Show()
        {
            tapScreen.gameObject.SetActive(true);
            ownAnim.SetBool("Show", true);
        }

        private void Hide()
        {
            ownAnim.SetBool("Show", false);
            tapScreen.gameObject.SetActive(false);
        }

        public static void ShowScreen()
        {
            _instance.Show();
        }
    }
}