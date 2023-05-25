using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchDetectionController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] CameraMoveController _camMoveController;
    [SerializeField] float _cameraSpeed, _minCamSize, _maxCamSize;

    private Control _controls;
    private Coroutine _zoomCoroutine;

    private void Awake()
    {
        _controls = new();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _controls.Map.SecondaryTouchContact.started += _ => ZoomStart();
        _controls.Map.SecondaryTouchContact.canceled += _ => ZoomEnd();
    }

    private void ZoomStart()
    {
        _zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    private void ZoomEnd()
    {
        StopCoroutine(_zoomCoroutine);
    }

    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f, distance = 0f;
        while (true)    
        {
            if(Touchscreen.current.touches.Count > 1)
            {
                distance = Vector2.Distance(_controls.Map.PrimaryTouchPosition.ReadValue<Vector2>(),
               _controls.Map.SecondaryTouchPosition.ReadValue<Vector2>());
                //Detection
                //Zoom out
                if (distance < previousDistance)
                {
                    _camera.orthographicSize += Time.deltaTime * _cameraSpeed;
                    _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minCamSize, _maxCamSize);

                    Vector2 newCamTransform = _camMoveController.ClampCamera(_camera.transform.position);
                    _camera.transform.position = new Vector3(newCamTransform.x, newCamTransform.y, _camera.transform.position.z);
                }
                //Zoom in   
                else if (distance > previousDistance)
                {
                    _camera.orthographicSize -= Time.deltaTime * _cameraSpeed;
                    _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minCamSize, _maxCamSize);

                    Vector2 newCamTransform = _camMoveController.ClampCamera(_camera.transform.position);
                    _camera.transform.position = new Vector3(newCamTransform.x, newCamTransform.y, _camera.transform.position.z);
                }
            }
            //Keep track of previous position to next loop

            previousDistance = distance;

            yield return null;
        }
    }
}
