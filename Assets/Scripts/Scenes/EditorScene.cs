using UnityEngine;
using UnityEngine.Assertions;

public class EditorScene : MonoBehaviour
{
    public static EditorScene instance { private set; get; }

    void Awake()
    {
        Assert.IsNull(instance, "Only one instance of EditorScene can exist");
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ProgramManager.instance.OpenMainScene();
        }
    }
}
