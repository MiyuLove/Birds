using Management;

public class ManagerLogo : Manage
{
    protected override void Awake()
    {
        base.Awake();
        SetFadeOut(1);
    }

    public override void SetStart() { }
}

