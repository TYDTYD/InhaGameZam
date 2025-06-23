public class GameManager : Singleton<GameManager>
{
    protected override void OnAwakeWork()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnDestroyedWork()
    {
        base.OnDestroyedWork();
    }
}
