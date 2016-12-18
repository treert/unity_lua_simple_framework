using UnityEngine;
using System.Collections;

public class ResultUI : MonoBehaviour
{
    public UILabel m_LabelScore;
    public UILabel m_LabelBest;
    public UISprite m_SpriteMedal;
    public void InitResultUI(int curScore,int bestScore)
    {
        m_LabelBest.text = bestScore.ToString();
        m_LabelScore.text = curScore.ToString();
    }
    //点击重新开始游戏按钮
    public void OnClickRetryBtn()
    {
        Debug.Log("Restart Game");
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
