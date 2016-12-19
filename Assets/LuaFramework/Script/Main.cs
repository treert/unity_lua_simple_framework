using UnityEngine;
using System.Collections;

namespace LuaFramework
{
    public class Main : MonoBehaviour
    {
        //启动游戏
        void Start()
        {
#if UNITY_STANDALONE_WIN
            Screen.SetResolution(382, 681, false);
#endif
            AppFacade.StartUp(gameObject);
        }


    }
}