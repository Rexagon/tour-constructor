using UnityEngine;
using UnityEngine.Assertions;

public class MainScene : MonoBehaviour
{
    public static MainScene instance { private set; get; }

    public TourGrid tourGrid;
    public TourInfoPanel tourInfoPanel;

    void Awake()
    {
        Assert.IsNull(instance, "Only one instance of MainScene can exist");
        instance = this;

        Assert.IsNotNull(tourGrid, "Scene must contain TourGrid");
        Assert.IsNotNull(tourInfoPanel, "Scene must contain TourInfoPanel");

        //TODO: make tour loading from own service

        Tour testTour = new Tour("test");
        testTour.coverImageUrl = "https://www.boutiquelodgingco.com/wp-content/uploads/2016/01/Panorama-mountain-resort-greywolf-golf-cliffhanger.jpg";

        tourGrid.UpdateTours(new Tour[]
        {
            testTour,
            testTour,
            testTour
        });
    }


}
