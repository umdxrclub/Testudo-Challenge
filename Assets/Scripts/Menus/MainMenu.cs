using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class MainMenu : SimpleMenu<MainMenu>
{
    public void LoadARScene()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void SendBugReportEmail()
    {
        string email = "umd.xr.club@gmail.com";
        string subject = MyEscapeURL("Found bug in Testudo's Altar App");
        Application.OpenURL("mailto:" + email + "?subject=" + subject);
    }

    private string MyEscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
