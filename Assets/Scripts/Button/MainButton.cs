using UnityEngine;
using UnityEngine.SceneManagement;
public class MainButton : MonoBehaviour
{
    public void LoadStageScene()
    {
        SceneManager.LoadScene("Stage");
    }
}
