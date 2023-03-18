using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TestTask.Characters;

namespace TestTask.Level
{
    public class ShootController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float _maxProjectionDistance;
        [SerializeField] private float _targetlessAimDistance;
        private const string TARGET_LAYER_NAME = "Targetable";
        private LayerMask _targetLayerMask;

        private Camera _mainCam;

        private void Awake()
        {
            _mainCam = Camera.main;
            _targetLayerMask = LayerMask.GetMask(TARGET_LAYER_NAME);
        }

        private void PropagateInput(PointerEventData eventData)
        {
            Vector3 shootPosition;
            Transform shootTarget = null;

            RaycastHit raycastHit;
            Vector3 propagationStart = _mainCam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, transform.localPosition.z));
            Vector3 propagationEnd = _mainCam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, _maxProjectionDistance));
            if (Physics.Raycast(propagationStart, propagationEnd - propagationStart, out raycastHit, _maxProjectionDistance, _targetLayerMask))
            {
                shootPosition = raycastHit.point;
                shootTarget = raycastHit.transform;
            } else
            {
                shootPosition = _mainCam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, _targetlessAimDistance));
            }
            Player.OrderShoot(shootPosition, shootTarget);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PropagateInput(eventData);
        }
    }
}