using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class ARMenu : SimpleMenu<ARMenu>
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnBackPressed()
    {
        LoadMainMenuScene();
    }
}
