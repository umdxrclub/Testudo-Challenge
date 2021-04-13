using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class ARMenu : SimpleMenu<ARMenu>
{

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

	public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnBackPressed()
    {
        LoadMainMenuScene();
    }
}
