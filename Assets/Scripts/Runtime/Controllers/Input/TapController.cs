using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapController : MonoBehaviour
{
    public event Tap OnTapStarted;
    public event Tap OnTapMoved;
    public event Tap OnTapEnded;
    public delegate void Tap(ClickableObject clickObject);

    [SerializeField] Camera _camera;

    private Control _control = null;
    private Coroutine _coroutine = null;

    private bool _isDragging = false;
    private bool _isTargetTouching = false;

    private void Awake()
    {
        _control = new Control();

        _control.Map.IsTouched.started += (context) => GetStartTarget();
        _control.Map.IsTouched.canceled += (context) => GetEndTap();
        _control.Map.IsTouched.performed += _ => _coroutine = StartCoroutine(FindTap());

        _control.Enable();
    }

    private void OnDestroy() =>
        _control.Disable();

    private void GetStartTarget()
    {
        _isDragging = false;

        Vector2 touchPosition = _control.Map.PrimaryTouchPosition.ReadValue<Vector2>();

        if (this.IsPointerOverUIObject(touchPosition))
            return;

        Ray ray = _camera.ScreenPointToRay(touchPosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        Dictionary<Transform, ClickableObject> targets = new();

        for (int i = 0; i < hits.Length; i++)
        {
            ClickableObject target = hits[i].transform.GetComponent<ClickableObject>();

            if (target != null)
                targets.Add(hits[i].transform, target);
        }

        Transform targetTransform = targets.Keys.Where(hit => targets[hit] != null).
            OrderBy(hit => Vector3.Distance(hit.transform.position, ray.origin))
            .FirstOrDefault();

        if (targetTransform != null && targets[targetTransform].IsValid)
        {

            OnTapStarted(targets[targetTransform]);
            _isDragging = true;
            _isTargetTouching = true;
        }
    }

    IEnumerator FindTap()
    {
        while (_isDragging)
        {
            Vector2 touchPosition = _control.Map.PrimaryTouchPosition.ReadValue<Vector2>();

            Ray ray = _camera.ScreenPointToRay(touchPosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 100);

            Dictionary<Transform, ClickableObject> targets = new();

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.TryGetComponent<ClickableObject>(out var target))
                {
                    targets.Add(hits[i].transform, target);
                }
            }

            Transform targetTransform = targets.Keys.Where(hit => targets[hit] != null).
                OrderBy(hit => Vector3.Distance(hit.transform.position, ray.origin))
                .FirstOrDefault();

            ClickableObject tap = null;

            bool isValid = targetTransform != null && targets[targetTransform].IsValid;

            if (isValid)
            {
                tap = targets[targetTransform];

                if (_isTargetTouching)
                {
                    OnTapMoved?.Invoke(tap);
                }
            }
            yield return null;
        }
    }

    private void GetEndTap()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        Vector2 touchPosition = _control.Map.PrimaryTouchPosition.ReadValue<Vector2>();

        Ray ray = _camera.ScreenPointToRay(touchPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 100);

        Dictionary<Transform, ClickableObject> targets = new();

        for (int i = 0; i < hits.Length; i++)
        {   
            if (hits[i].transform.TryGetComponent<ClickableObject>(out var target))
            {
                targets.Add(hits[i].transform, target);
            }
        }

        Transform targetTransform = targets.Keys.Where(hit => targets[hit] != null).
            OrderBy(hit => Vector3.Distance(hit.transform.position, ray.origin))
            .FirstOrDefault();

        ClickableObject tap = null;

        bool isValid = targetTransform != null && targets[targetTransform].IsValid;

        if(isValid)
        {
            tap = targets[targetTransform];
  
            if (_isTargetTouching)
            {
                OnTapEnded(tap);
                _isTargetTouching = false;
            }
        }
    }
}
