using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using System.IO;


namespace LuaFramework
{
    public class GameManager : MonoBehaviour
    {
        protected static bool initialize = false;
        private List<string> downloadFiles = new List<string>();

        /// <summary>
        /// 初始化游戏管理器
        /// </summary>
        void Awake()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            DontDestroyOnLoad(gameObject);  //防止销毁自己

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        void Start()
        {
            AppFacade.LuaManager.InitStart();
            Util.CallGlobalLuaFunction("OnInitOK");     //初始化完成
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        void OnDestroy()
        {
            if (AppFacade.LuaManager != null)
            {
                AppFacade.LuaManager.Close();
            }
            Debug.Log("~GameManager was destroyed");
        }
    }
}