using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ARMenu : SimpleMenu<ARMenu>
{
    [Header("New Offering Button")]
    public GameObject outerHighlight;
    private IEnumerator enableHighlightCoroutine;

    [Header("Capture and Share Button")]
    public Image shareIcon;
    public Sprite androidIcon;
    public Sprite iosIcon;

    private void Start()
    {
        enableHighlightCoroutine = EnableHighlightCoroutine();
        StartCoroutine(enableHighlightCoroutine);

#if UNITY_ANDROID
        shareIcon.sprite = androidIcon;
#elif UNITY_IOS
        shareIcon.sprite = iosIcon;
#endif
    }

    private IEnumerator EnableHighlightCoroutine()
    {
        yield return new WaitForSeconds(5);
        outerHighlight.SetActive(true);
    }

    public void DisableHighlight()
    {
        StopCoroutine(enableHighlightCoroutine);
        outerHighlight.SetActive(false);
    }

    public void TakeScreenshotAndShare()
	{
		StartCoroutine(TakeScreenshotAndShareCoroutine());
	}

    private IEnumerator TakeScreenshotAndShareCoroutine()
    {
        // Create a unique filename based on current time.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        string fileName = "TestudoToGo_" + timestamp + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        ScreenCapture.CaptureScreenshot(fileName);

        // Screenshot capture and saving takes place in the background. Don't
        // try to share until image has finished saving.
        while(!File.Exists(filePath))
        {
            yield return null;
        }

        // Open iOS/Android native sharing popup.
        string message = "Check out my virtual Testudo! - Created with Testudo-To-Go app. " +
            "Download now on the App Store or Google Play Store";
        new NativeShare().AddFile(filePath).SetText(message).Share();
    }

    public void ShowSelectOfferingScreen()
    {
        SelectOffering.Show();
    }

	public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnBackPressed()
    {
        LoadMainMenuScene();
    }
}
