using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private static ScreenController Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Screen.fullScreen = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
