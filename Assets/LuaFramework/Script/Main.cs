using UnityEngine;
using System.Collections;

namespace LuaFramework
{
    public class Main : MonoBehaviour
    {
        //启动游戏
        void Start()
        {
            AppFacade.Instance.AddManager<LuaManager>("LuaManager");
            AppFacade.Instance.AddManager<ResourceManager>("ResourceManager");
            AppFacade.Instance.AddManager<PanelManager>("PanelManager");
            AppFacade.Instance.AddManager<GameManager>("GameManager");
        }
    }
}