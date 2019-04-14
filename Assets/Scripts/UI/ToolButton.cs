using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class ToolButton : MonoBehaviour
{
    private Image _image;
    private Button _button;

    void Awake()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        Assert.IsNotNull(_image, "Image must be in children of this component");

        _button = GetComponent<Button>();
        Assert.IsNotNull(_button, "Button must assigned to this component");
    }

    public void SetInfo(ToolButtonInfo info)
    {
        _image.sprite = info.icon;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => info.targetEvent.Invoke());
    }
}
