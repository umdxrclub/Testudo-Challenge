using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool Muted { get; private set; }
    private AudioListener listener;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        listener = FindObjectOfType<AudioListener>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (PlayerPrefs.HasKey("volume"))
        {
            Muted = PlayerPrefs.GetInt("volume") == 0;
        }
        else
        {
            PlayerPrefs.SetInt("volume", 1);
            Muted = false;
        }

        listener.enabled = !Muted;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        listener = FindObjectOfType<AudioListener>();
        listener.enabled = !Muted;
    }

    public void ToggleSound()
    {
        Muted = !Muted;
        listener.enabled = !Muted;
        PlayerPrefs.SetInt("volume", Muted ? 0 : 1);
    }
}
