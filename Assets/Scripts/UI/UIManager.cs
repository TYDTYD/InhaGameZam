using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button GameStartButton;
    [SerializeField] Button HowToPlayButton;
    [SerializeField] Button QuitButton;
    [SerializeField] GameObject HowToPlayUI;

    private void Start()
    {
        GameStartButton.onClick.AddListener(ChangeScene);
        HowToPlayButton.onClick.AddListener(EnableHowToPlay);
        QuitButton.onClick.AddListener(DisableHowToPlay);
    }

    void EnableHowToPlay() => HowToPlayUI.SetActive(true);

    void DisableHowToPlay() => HowToPlayUI.SetActive(false);

    void ChangeScene() => SceneManager.LoadScene("Stage");
}