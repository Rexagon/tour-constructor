using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tool Button", menuName = "Tool Buttons", order = 0)]
public class ToolButtonInfo : ScriptableObject
{
    public Sprite icon;
    public UnityEvent targetEvent;
}
