using UnityEngine;
using UnityEngine.Assertions;

public class EditorScene : MonoBehaviour
{
    public static EditorScene instance { private set; get; }

    public Panorama panorama;

    void Awake()
    {
        Assert.IsNull(instance, "Only one instance of EditorScene can exist");
        instance = this;

        Assert.IsNotNull(panorama, "Editor scene must contain panorama");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            ProgramManager.instance.OpenMainScene();
        }
    }
}
