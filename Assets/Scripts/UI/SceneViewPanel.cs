using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class SceneViewPanel : MonoBehaviour
{
    public CameraController cameraController;

    private RawImage _targetImage;
    private RectTransform _rectTransform;
    private Camera _sourceCamera;

    private Vector2 _currentSize;

    private GraphicRaycaster _raycaster;

    void Awake()
    {
        Assert.IsNotNull(cameraController, "Source camera must be specified");

        _targetImage = GetComponent<RawImage>();
        _rectTransform = GetComponent<RectTransform>();
        _sourceCamera = cameraController.GetComponent<Camera>();

        _raycaster = GetComponentInParent<GraphicRaycaster>();
        Assert.IsNotNull(_raycaster);
    }

    void Update()
    {
        if (_currentSize != _rectTransform.rect.size)
        {
            _currentSize = _rectTransform.rect.size;
            UpdateView();
        }

        if (Input.GetMouseButtonDown(2))
        {
            PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(pointerEvent, results);

            foreach (var result in results)
            {
                if (result.gameObject == gameObject)
                {
                    cameraController.mouseRotationEnabled = true;
                    break;
                }
            }
        }
        else if (Input.GetMouseButtonUp(2))
        {
            cameraController.mouseRotationEnabled = false;
        }
    }

    void UpdateView()
    {
        if (_currentSize.x < 1 || _currentSize.y < 1)
            return;

        if (_sourceCamera.targetTexture)
            _sourceCamera.targetTexture.Release();

        _sourceCamera.targetTexture = new RenderTexture((int)_currentSize.x, (int)_currentSize.y, 24);
        _targetImage.texture = _sourceCamera.targetTexture;

        _sourceCamera.aspect = _currentSize.x / _currentSize.y;
    }
}
