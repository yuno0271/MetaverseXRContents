using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class Utility : MonoBehaviour
{
    private static ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    static Utility()
    {
        raycastManager = GameObject.FindFirstObjectByType<ARRaycastManager>();
    }

    public static bool Raycast(Vector2 screenPosition, out Pose pose)
    {
        if(raycastManager.Raycast(screenPosition, hits, TrackableType.AllTypes))
        {
            pose = hits[0].pose;
            return true;
        }
        else
        {
            pose = Pose.identity;
            return false;
        }
    }

    public static bool TryGetInputPosition(out Vector2 position)
    {
        position = Vector2.zero;

        if(Input.touchCount == 0)
        {
            return false;
        }

        position = Input.GetTouch(0).position;

        if(Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
