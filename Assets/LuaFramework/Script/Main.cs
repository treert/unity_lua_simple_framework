using UnityEngine;
using System.Collections;

namespace LuaFramework
{
    public class Main : MonoBehaviour
    {
        //启动游戏
        void Start()
        {
            AppFacade.StartUp(gameObject);
        }


    }
}