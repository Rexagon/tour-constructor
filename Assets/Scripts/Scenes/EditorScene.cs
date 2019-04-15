using UnityEngine;
using UnityEngine.Assertions;

public class EditorScene : MonoBehaviour
{
    public static EditorScene instance { private set; get; }

    public Panorama panorama;
    public SceneViewPanel sceneViewPanel;
    public TourStateEditPanel tourStateEditPanel;

    void Awake()
    {
        Assert.IsNull(instance, "Only one instance of EditorScene can exist");
        instance = this;

        Assert.IsNotNull(panorama, "Editor scene must contain panorama");
        Assert.IsNotNull(sceneViewPanel, "Editor scene must contain scene view panel");
        Assert.IsNotNull(tourStateEditPanel, "Editor scene must contain state edit panel");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            ProgramManager.instance.OpenMainScene();
        }
    }
}
