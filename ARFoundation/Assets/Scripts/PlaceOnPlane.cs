using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private LayerMask placedObjectLayerMask;

    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hit;


    private void Update()
    {
        if (!Utility.TryGetInputPosition(out touchPosition)) return;

        // 오브젝트 선택
        ray = arCamera.ScreenPointToRay(touchPosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, placedObjectLayerMask))
        {
            PlacedObject.SelectedObject = hit.transform.GetComponentInChildren<PlacedObject>();
            return;
        }

        PlacedObject.SelectedObject = null;

        if(Utility.Raycast(touchPosition, out Pose hitPose))
        {
            Instantiate(placedPrefab, hitPose.position , hitPose.rotation);
        }
    }
}
