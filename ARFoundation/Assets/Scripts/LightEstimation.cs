using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager arCameraManager;
    private new Light light;

    private void Awake()
    {
        if(arCameraManager == null)
        {
            Debug.Log("AR Camera Manager is Nothing!");
            Destroy(this);
        }

        light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameChanged;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameChanged;
    }

    private void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            light.intensity = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            light.color = args.lightEstimation.colorCorrection.Value;
        }

        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            light.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        }

        if(args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            light.intensity = args.lightEstimation.averageMainLightBrightness.Value;
        }

        if(args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
        }
    }
}
