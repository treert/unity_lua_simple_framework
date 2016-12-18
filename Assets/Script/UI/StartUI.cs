using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour
{

    public void OnClickStart()
    {
        Debug.Log("Start Game");
        Hide();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
