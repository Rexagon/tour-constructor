using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SceneViewPanel : MonoBehaviour
{
    public Camera sourceCamera;

    private RawImage _targetImage;
    private RectTransform _rectTransform;

    private Vector2 _currentSize;

    void Awake()
    {
        Assert.IsNotNull(sourceCamera, "Source camera must be specified");

        _targetImage = GetComponent<RawImage>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_currentSize != _rectTransform.rect.size)
        {
            _currentSize = _rectTransform.rect.size;
            UpdateView();
        }
    }

    void UpdateView()
    {
        if (sourceCamera.targetTexture)
            sourceCamera.targetTexture.Release();

        sourceCamera.targetTexture = new RenderTexture((int)_currentSize.x, (int)_currentSize.y, 24);
        _targetImage.texture = sourceCamera.targetTexture;

        sourceCamera.aspect = _currentSize.x / _currentSize.y;
    }
}
