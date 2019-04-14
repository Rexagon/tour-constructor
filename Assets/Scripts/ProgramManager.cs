using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgramManager : MonoBehaviour
{
    public string mainSceneName;
    public string editorSceneName;

    public static ProgramManager instance 
    { 
        private set 
        {
            _instance = value;
            DontDestroyOnLoad(_instance);
        }
        get => _instance;
    }

    private static ProgramManager _instance;

    public Tour currentTour { private set; get; }

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void OpenEditorScene(Tour tour)
    {
        currentTour = tour;
        SceneManager.LoadScene(editorSceneName);
    }
}
