using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MainMenu : SimpleMenu<MainMenu>
{
    public GameObject ButtonsContainer;
    public Image audioIconHolder;
    public Sprite volumeIcon;
    public Sprite mutedIcon;
    public GameObject ARCoreDisclosure;

    private void Start()
    {
        audioIconHolder.sprite = AudioManager.Instance.Muted ? mutedIcon : volumeIcon;
#if UNITY_ANDROID
        ARCoreDisclosure.SetActive(true);
        ButtonsContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 225);
#endif
    }

    public void ShowInfoPage()
    {
        InfoPage.Show();
    }

    public void ShowCredits()
    {
        Credits.Show();
    }

    public void LoadARScene()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void ToggleAudio()
    {
        AudioManager.Instance.ToggleSound();
        audioIconHolder.sprite = AudioManager.Instance.Muted ? mutedIcon : volumeIcon;
    }

    public void SendBugReportEmail()
    {
        string email = "umd.xr.club@gmail.com";
        string subject = MyEscapeURL("Found bug in Testudo-To-Go App");
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
