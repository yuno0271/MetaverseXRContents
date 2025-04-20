using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.XR.CoreUtils;
using Unity.Collections;

public class ARFaceRegionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] regionPrefabs;

    private ARFaceManager faceManager;
    private XROrigin xrOrigin;

    private NativeArray<ARCoreFaceRegionData> faceRegions;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
        xrOrigin = GetComponent<XROrigin>();

        for(int i = 0;i < regionPrefabs.Length; i++)
        {
            regionPrefabs[i] = Instantiate(regionPrefabs[i], xrOrigin.TrackablesParent);
        }
    }

    private void Update()
    {
        ARCoreFaceSubsystem subSystem = (ARCoreFaceSubsystem)faceManager.subsystem;

        foreach(ARFace face in faceManager.trackables)
        {
            subSystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions);

            foreach(ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                regionPrefabs[(int)regionType].transform.localPosition = faceRegion.pose.position;
                regionPrefabs[(int)regionType].transform.localRotation = faceRegion.pose.rotation;
            }
        }
    }
}
