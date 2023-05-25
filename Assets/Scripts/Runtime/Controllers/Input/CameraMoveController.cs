using System.Collections;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] SpriteRenderer _mapRenderer;

    private Control _controls; 
    private Coroutine _coroutine;
    private float _mapMinX, _mapMinY, _mapMaxX, _mapMaxY;


    private void Awake()
    {
        _controls = new();

        _mapMinX = _mapRenderer.transform.position.x - _mapRenderer.bounds.size.x / 2f;
        _mapMaxX = _mapRenderer.transform.position.x + _mapRenderer.bounds.size.x / 2f;
        _mapMinY = _mapRenderer.transform.position.y - _mapRenderer.bounds.size.y / 2f;
        _mapMaxY = _mapRenderer.transform.position.y + _mapRenderer.bounds.size.y / 2f;
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
        _controls.Map.IsTouched.started += (context) => TouchStart();
        _controls.Map.IsTouched.canceled += (context) => TouchEnd();
    }

    private void TouchStart()
    {
        _coroutine = StartCoroutine(Move());
    }

    private void TouchEnd()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    IEnumerator Move()
    {
        Vector2 touchPosition = _controls.Map.PrimaryTouchPosition.ReadValue<Vector2>();
        Vector2 originTouchPosition = _camera.ScreenToWorldPoint(touchPosition);

        if (this.IsPointerOverUIObject(touchPosition))
            yield break;

        Ray ray = _camera.ScreenPointToRay(touchPosition);

        if(Physics2D.Raycast(ray.origin, ray.direction))
            yield break;
        
        while (true)
        {
            Vector2 screenPosition = _controls.Map.PrimaryTouchPosition.ReadValue<Vector2>();
            Vector2 currentTouchPosition = _camera.ScreenToWorldPoint(screenPosition);
            Vector2 cameraPosition = (Vector2)_camera.transform.position;
            Vector2 difference = currentTouchPosition - cameraPosition;

            cameraPosition = ClampCamera(originTouchPosition - difference);
            _camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, _camera.transform.position.z);
  
            yield return null; 
        }
    }

    public Vector2 ClampCamera(Vector2 targetPosition)
    {
        float camHeight = _camera.orthographicSize;
        float camWidth = _camera.orthographicSize * _camera.aspect;

        float minX = _mapMinX + camWidth;
        float maxX = _mapMaxX - camWidth;
        float minY = _mapMinY + camHeight;
        float maxY = _mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector2(newX, newY);
    }
} 
