using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    Camera mainCamera;
    protected override void OnAwakeWork()
    {
        DontDestroyOnLoad(this.gameObject);
        mainCamera = Camera.main;
    }

    private void Start()
    {
        SetResolution();
    }

    protected override void OnDestroyedWork()
    {
        base.OnDestroyedWork();
    }

    void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        float setResolution = (float)setWidth / setHeight;
        float deviceResolution = (float)deviceWidth / deviceHeight;

        if(setResolution < deviceResolution)
        {
            float newWidth = setResolution / deviceResolution;
            mainCamera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = deviceResolution / setResolution;
            mainCamera.rect = new Rect(0f, (1f - newHeight), 1f, newHeight);
        }
    }
}
