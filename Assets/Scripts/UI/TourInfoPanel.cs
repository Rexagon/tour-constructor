using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class TourInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public Button editorButton;

    void Awake()
    {
        Assert.IsNotNull(titleText, "Title text element must be specified");
        Assert.IsNotNull(descriptionText, "Description text element must be specified");
        Assert.IsNotNull(editorButton, "Editor button must be specified");

        SetTour(null);
    }

    public void SetTour(Tour tour)
    {
        titleText.text = tour != null ? tour.name : "";
        descriptionText.text = tour != null ? tour.description : "";

        editorButton.gameObject.SetActive(tour != null);
        editorButton.onClick.RemoveAllListeners();
        if (tour != null)
        {
            editorButton.onClick.AddListener(() => ProgramManager.instance.OpenEditorScene(tour));
        }
    }
}
