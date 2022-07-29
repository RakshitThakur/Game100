using UnityEngine;

public class MenuItems : MonoBehaviour
{
    [Tooltip("Set this true for the default screen which is openend when the scene is loaded.")]
    public bool isOpen;
    [Tooltip("Can the user come back to this screen.")]
    public bool isBackable;
    [Tooltip("Enter the Screen Title if any.")]
    [SerializeField] private string title;
    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }
    public string GetTitle() => !string.IsNullOrEmpty(title) ? title : null;
}
