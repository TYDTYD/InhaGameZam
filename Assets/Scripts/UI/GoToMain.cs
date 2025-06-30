using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GoToMain : MonoBehaviour
{
    Button GoToMainButton;
    private void Awake()
    {
        GoToMainButton = GetComponent<Button>();
        Cursor.visible = true;
    }

    private void Start()
    {
        GoToMainButton.onClick.AddListener(LoadMain);
    }

    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
