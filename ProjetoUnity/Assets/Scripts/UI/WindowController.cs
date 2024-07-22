using AYellowpaper;
using UnityEngine;

public abstract class WindowController<T> : MonoBehaviour where T: IWindow
{

    [RequireInterface(typeof(IWindow))]
    [SerializeField] private MonoBehaviour windowObject;

    protected T window;

    protected void Awake()
    {
        window = windowObject.GetComponent<T>();
    }

    public virtual void Show()
    {
        window.Show();
    }

    public virtual void Hide()
    {
        window.Hide();
    }
}
