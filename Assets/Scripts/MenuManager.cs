using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuItems[] menus;
    [Tooltip("Title Text for the screen, leave empty if no title")]
    [SerializeField] private TextMeshProUGUI headerText;
    public static MenuManager Instance;
    private readonly Stack<MenuItems> backStack = new Stack<MenuItems>();
    private MenuItems menu;
    public string currentMenu = "HomePage";
    public bool shouldPlaySound = true;
    [SerializeField]
    private RectTransform canvas = null;

    private void Awake()
    {
        Instance = this;
    }
    public MenuItems OpenMenu(string menuName)
    {
        if (canvas != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(canvas);
        }

        currentMenu = menuName;

        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name == menuName)
            {
                menus[i].Open();
                menu = menus[i];
                if (menus[i].GetTitle() != null)
                {
                    headerText.text = menus[i].GetTitle();
                }
            }
            else if (menus[i].isOpen)
            {
                CloseMenu(menus[i]);
                if (menus[i].isBackable)
                {
                    backStack.Push(menus[i]);
                }
            }
        }
        return menu;
    }
    public void UpdateCanvas()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvas);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GoBack();
            return;
        }
    }
    public void OpenMenu(MenuItems menu)
    {
        OpenMenu(menu.name);
    }
    public void CloseMenu(MenuItems menu)
    {
        menu.Close();
    }
    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name == menuName)
            {
                CloseMenu(menus[i]);
                if (menus[i].isBackable)
                {
                    backStack.Push(menus[i]);
                }
            }
        }
    }
    public void GoBack()
    {
        if (backStack.Count > 0)
        {
            OpenMenu(backStack.Peek());
            backStack.Pop();
        }
        else
        {
            Application.Quit();
        }
    }
    public MenuItems GetMenuName()
    {
        return menu;
    }
}
