using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.UI.Modules;

namespace TestTask.UI 
{
    /// <summary>
    /// With this singleton we're making sure that project would remain scalable as we're adding more UI, while getting a layer of control over input modules.
    /// That way we can disable ability to shoot during certain transitions and hide\show appropriate buttons - all in one place.
    /// We're also removing requirement to have StartLevelUI and ShootController to be explicit singletons, as now they can be considered components.
    /// We could also realize Message Bus here, although I'm not fond of that pattern.
    /// I find this approach somewhat weird, but that's my best attempt on cutting some singletons here as this was part of the feedback I've got.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private ShootInputHandler _shootControls;
        [SerializeField] private StartLevelScreen _startScreen;
        private static InputManager _instance;

        public static event Action onStartInput;
        public static event Action<Vector3, Transform> onShootInput;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogWarning($"There should only be 1 InputManager on the scene! Destroying the copy in {gameObject.name}");
                Destroy(this);
            }
        }

        private void Start()
        {
            _startScreen.onScreenTapped += OnStartTap;
            _shootControls.onShootInput += OnShootTap;

            _shootControls.Initialize();
            _startScreen.Initialize();
        }


        private void OnStartTap()
        {
            onStartInput?.Invoke();
        }

        private void OnShootTap(Vector3 shootPosition, Transform shootTarget)
        {
            onShootInput?.Invoke(shootPosition, shootTarget);
        }
    }
}