using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class TourGridNewItem : MonoBehaviour
{
    public delegate void OnClickEvent();

    public OnClickEvent onClick
    {
        set
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => value());
        }
        get => () => _button.onClick.Invoke();
    }

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        Assert.IsNotNull(_button, "TourGridNew must be a button");
    }
}
