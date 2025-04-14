using UnityEngine;
using UnityEngine.EventSystems;

public class PlacedObject : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeSelected;

    public bool IsSelected
    {
        get => SelectedObject == this;
    }

    private static PlacedObject selectedObject;
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if(selectedObject == value)
            {
                return;
            }

            if(selectedObject != null)
            {
                selectedObject.cubeSelected.SetActive(false);
            }

            selectedObject = value;

            if(value != null)
            {
                value.cubeSelected.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        cubeSelected.SetActive(false);
    }

    public void OnPointerDrag(BaseEventData bed)
    {
        if (IsSelected)
        {
            PointerEventData ped = (PointerEventData)bed;
            if(Utility.Raycast(ped.position, out Pose hitPose))
            {
                transform.position = hitPose.position;
            }
        }
    }
}
