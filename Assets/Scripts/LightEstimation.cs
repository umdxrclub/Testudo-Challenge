using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public ARCameraManager m_CameraManager;
    public Light m_Light;

    void OnEnable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived += ChangeLighting;
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= ChangeLighting;
    }

    void ChangeLighting(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        }

        if (args.lightEstimation.mainLightColor.HasValue)
        {
            m_Light.color = args.lightEstimation.mainLightColor.Value;
        }
        else if (args.lightEstimation.colorCorrection.HasValue)
        {
            m_Light.color = args.lightEstimation.colorCorrection.Value;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            m_Light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            m_Light.intensity = args.lightEstimation.mainLightIntensityLumens.Value;
        }
        else if (args.lightEstimation.averageBrightness.HasValue)
        {
            m_Light.intensity = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
        }
    }
}
