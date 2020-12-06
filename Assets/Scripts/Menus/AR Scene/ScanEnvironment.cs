using UnityEngine.UI.Extensions;
using UnityEngine.XR.ARFoundation;

public class ScanEnvironment : SimpleMenu<ScanEnvironment>
{
    protected override void Awake()
    {
        base.Awake();
        GetComponent<UIManager>().arSessionOrigin = ((ARSessionOrigin) FindObjectOfType(typeof(ARSessionOrigin))).gameObject;
        GetComponent<ARUXAnimationManager>().coachingOverlay = (ARKitCoachingOverlay) FindObjectOfType(typeof(ARKitCoachingOverlay));
    }

    public override void OnBackPressed()
    {
    }
}
