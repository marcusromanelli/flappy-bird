using AYellowpaper;
using UnityEngine;

public abstract class WindowController<T> : MonoBehaviour where T: IWindow
{

    [RequireInterface(typeof(IWindow))]
    [SerializeField] private MonoBehaviour windowObject;

    protected T window;

    private void Awake()
    {
        window = windowObject.GetComponent<T>();
    }

    public void Show()
    {
        window.Show();
    }

    public void Hide()
    {
        window.Hide();
    }
}
