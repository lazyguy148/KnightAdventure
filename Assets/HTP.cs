using UnityEngine;

public class HTP : MonoBehaviour
{
    public GameObject htpMenu;

    public void ActiveMenu()
    {
        htpMenu.SetActive(true);
    }

    public void HideMenu()
    {
        htpMenu.SetActive(false);
    }

}
