using UnityEngine;
using UnityEngine.EventSystems;

public class OnImageClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private BuildingPlace myBuildingPlace = null;

    [SerializeField]
    private int myBuildingIndex = -1;

    public void OnPointerClick(PointerEventData eventData)
    {
        myBuildingPlace.TryToBuild(myBuildingIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myBuildingPlace.ShowBuilding(myBuildingIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myBuildingPlace.ShowBuilding(-1);
    }
}
