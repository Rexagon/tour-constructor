using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using System.Collections;

public class TourGridItem : MonoBehaviour
{
    public delegate void OnClickEvent();

    public static Color[] defaultColors = new Color[]
    {
        new Color(0.667f, 0.29f, 0.224f),
        new Color(0.667f, 0.443f, 0.224f),
        new Color(0.145f, 0.361f, 0.412f),
        new Color(0.161f, 0.486f, 0.275f)
    };

    public string tourName
    {
        set => _nameText.text = value;
        get => _nameText.text;
    }

    public Texture tourBackground
    {
        set
        {
            _backgroundImage.texture = value;
            _backgroundImage.color = value ? Color.white : _fallbackColor;
        }
        get => _backgroundImage.texture;
    }

    public OnClickEvent onClick
    {
        set
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => value());
        }
        get => () => _button.onClick.Invoke();
    }

    private Text _nameText;
    private RawImage _backgroundImage;
    private Button _button;
    private RectTransform _rectTransform;

    private Color _fallbackColor;

    private string _currentImageUrl;

    private Rect _lastSize;

    void Awake()
    {
        _nameText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Assert.IsNotNull(_nameText, "TourGrid item must contain name text");

        _backgroundImage = GetComponent<RawImage>();
        Assert.IsNotNull(_backgroundImage, "TourGrid item must contain background image");

        _button = GetComponent<Button>();
        Assert.IsNotNull(_button, "TourGrid item must be a button");

        _rectTransform = GetComponent<RectTransform>();
        Assert.IsNotNull(_rectTransform, "TourGrid item must have RectTransform");

        _fallbackColor = defaultColors[Random.Range(0, defaultColors.Length)];

        tourBackground = null;
    }

    void Update()
    {
        if (tourBackground != null && _rectTransform.rect != _lastSize)
        {
            FitBackgroundImage();
            _lastSize = _rectTransform.rect;
        }
    }

    public IEnumerator LoadBackgroundImage(string url)
    {
        _currentImageUrl = url;
        UnityWebRequest requestTexture = UnityWebRequestTexture.GetTexture(url);

        yield return requestTexture.SendWebRequest();

        if (url != _currentImageUrl)
            yield break;

        if (requestTexture.isNetworkError || requestTexture.isHttpError)
            yield break;

        Texture2D texture = DownloadHandlerTexture.GetContent(requestTexture);
        texture.Apply();

        tourBackground = texture;
    }

    void FitBackgroundImage()
    {
        Vector2 targetSize = _rectTransform.rect.size;
        float targetAspect = targetSize.x / targetSize.y;

        Texture texture = _backgroundImage.texture;

        Vector2 textureSize = new Vector2(texture.width, texture.height);
        float textureAspect = textureSize.x / textureSize.y;

        Vector2 size = textureSize, offset = Vector2.zero;
        bool shouldUpdate = false;

        if (textureAspect > targetAspect)
        {
            size = new Vector2(textureSize.y * targetAspect, textureSize.y);
            offset = new Vector2((textureSize.x - size.x) * 0.5f, 0.0f);
            shouldUpdate = true;
        }
        else if (textureAspect < targetAspect)
        {
            size = new Vector2(textureSize.x, textureSize.x / targetAspect);
            offset = new Vector2(0.0f, (textureSize.y - size.y) * 0.5f);
            shouldUpdate = true;
        }

        if (shouldUpdate)
            _backgroundImage.uvRect = new Rect(offset / textureSize, size / textureSize);
    }
}
