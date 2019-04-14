using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Linq;
using System.Collections.Generic;

public class TourGrid : MonoBehaviour
{
    public TourGridItem gridItemPrefab;
    public TourGridNewItem gridItemNewPrefab;

    public float padding = 5.0f;
    public Vector2 minElementSize = new Vector2(200.0f, 100.0f);

    private RectTransform _rectTransform;
    private RectTransform _parentRectTransform;

    private List<TourGridItem> _gridItems = new List<TourGridItem>();
    private TourGridNewItem _gridItemNew;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Assert.IsNotNull("TourGrid must have RectTransform component");

        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Assert.IsNotNull("TourGrid's parent must have RectTransform component");

        _gridItemNew = Instantiate(gridItemNewPrefab, transform);
        _gridItemNew.onClick = () => ProgramManager.instance.OpenEditorScene(null);
        _gridItemNew.transform.SetAsFirstSibling();
    }

    void Update()
    {
        float width = _parentRectTransform.rect.width;
        float height = _parentRectTransform.rect.height;

        float awailableWidth = width - padding;

        int itemCount = transform.childCount;
        int columnCount = Mathf.FloorToInt(awailableWidth / (minElementSize.x + padding));

        if (columnCount <= 0)
            columnCount = 1;
        
        Vector3 size = new Vector3(awailableWidth / columnCount - padding, minElementSize.y);

        int rowCount = itemCount / columnCount + (itemCount > 0 && itemCount % columnCount == 0 ? 0 : 1);

        _rectTransform.sizeDelta = new Vector2(-width, padding + (size.y + padding) * rowCount);

        int col = 0;
        int row = 0;
        foreach (RectTransform item in transform)
        {
            item.sizeDelta = size;
            item.localPosition = new Vector3(padding + col * (size.x + padding), - padding - row * (size.y + padding), 0.0f);

            if (++col >= columnCount)
            {
                col = 0;
                ++row;
            }
        }
    }

    public void UpdateTours(Tour[] tours)
    {
        int lengthDifference = tours.Length - _gridItems.Count;

        if (lengthDifference > 0) 
        { 
            for (int i = 0; i < lengthDifference; ++i)
            {
                _gridItems.Add(Instantiate(gridItemPrefab, transform));
            }
        }
        else if (lengthDifference < 0)
        {
            for (int i = lengthDifference; i < 0; ++i)
            {
                Destroy(_gridItems.First().gameObject);
                _gridItems.RemoveAt(0);
            }
        }

        int tourIndex = 0;
        foreach (var item in _gridItems)
        {
            Tour tour = tours[tourIndex];

            item.tourName = tour.name;
            item.onClick = () => MainScene.instance.tourInfoPanel.SetTour(tour);
            
            if (tour.coverImageUrl.Length == 0)
            {
                item.tourBackground = null;
            }
            else
            {
                StartCoroutine(item.LoadBackgroundImage(tour.coverImageUrl));
            }
        }
    }
}
