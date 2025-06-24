using System;

public class UpdateManager : Singleton<UpdateManager>
{
    public Action UpdateMethod;
    public Action LateUpdateMethod;
    public Action FixedUpdateMethod;
    protected override void OnAwakeWork()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        UpdateMethod?.Invoke();
    }
    private void LateUpdate()
    {
        LateUpdateMethod?.Invoke();
    }
    private void FixedUpdate()
    {
        FixedUpdateMethod?.Invoke();
    }
    public void SubscribeUpdate(Action action)
    {
        if (action == null)
            return;
        UpdateMethod += action;
    }
    public void UnsubscribeUpdate(Action action)
    {
        if (action == null)
            return;
        UpdateMethod -= action;
    }
    public void SubscribeLateUpdate(Action action)
    {
        if (action == null)
            return;
        LateUpdateMethod += action;
    }
    public void UnsubscribeLateUpdate(Action action)
    {
        if (action == null)
            return;
        LateUpdateMethod -= action;
    }
    public void SubscribeFixedUpdate(Action action)
    {
        if (action == null)
            return;
        FixedUpdateMethod += action;
    }
    public void UnsubscribeFixedUpdate(Action action)
    {
        if (action == null)
            return;
        FixedUpdateMethod -= action;
    }

    protected override void OnDestroyedWork()
    {
        UpdateMethod = null;
        LateUpdateMethod = null;
        FixedUpdateMethod = null;
    }
}
