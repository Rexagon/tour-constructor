using UnityEngine;
using UnityEngine.Assertions;

public class ToolBar : MonoBehaviour
{
    public ToolButton buttonPrefab;
    public ToolButtonInfo[] tools;

    public int spacing = 60;

    void Start()
    {
        Assert.IsNotNull(buttonPrefab, "Button prefab must be assigned");

        for (int i = 0; i < tools.Length; ++i)
        {
            ToolButton button = Instantiate(buttonPrefab, transform);
            button.transform.localPosition = -Vector3.up * spacing * i;

            button.SetInfo(tools[i]);
        }
    }
}
