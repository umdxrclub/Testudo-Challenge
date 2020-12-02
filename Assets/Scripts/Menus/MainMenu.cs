using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class MainMenu : SimpleMenu<MainMenu>
{
    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
