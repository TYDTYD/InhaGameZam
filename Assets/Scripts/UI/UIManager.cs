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

        GameStartButton.onClick.RemoveAllListeners();
        GameStartButton.onClick.AddListener(ChangeScene);


        HowToPlayButton.onClick.RemoveAllListeners();
        HowToPlayButton.onClick.AddListener(EnableHowToPlay);

        QuitButton.onClick.RemoveAllListeners();
        QuitButton.onClick.AddListener(DisableHowToPlay);

    }

    void EnableHowToPlay()
    {
        SoundManager.Instance.PlaySound(SoundType.MainButtonClick);
        HowToPlayUI.SetActive(true);

    }

    void DisableHowToPlay()
    {
        SoundManager.Instance.PlaySound(SoundType.MainButtonClick);
        HowToPlayUI.SetActive(false);
    }

    void ChangeScene()
    {
        SoundManager.Instance.PlaySound(SoundType.MainButtonClick);
        SceneManager.LoadScene("Stage");
    }
}