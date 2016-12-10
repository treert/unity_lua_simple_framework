/* 
    LuaFramework Code By Jarjin lee
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;

public class AppFacade
{
    private static AppFacade _instance;
    public static AppFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AppFacade();
            }
            return _instance;
        }
    }

    public static LuaManager LuaManager {
        get
        {
            return Instance.GetManager<LuaManager>("LuaManager");
        }
    }

    public static ResourceManager ResManager
    {
        get
        {
            return Instance.GetManager<ResourceManager>("ResourceManager");
        }
    }

    protected AppFacade()
    {
        _game_manager = GameObject.Find("GameManager");

    }

    private GameObject _game_manager;
    private Dictionary<string, object> _managers = new Dictionary<string, object>();

    /// <summary>
    /// 添加管理器
    /// </summary>
    public void AddManager(string typeName, object obj)
    {
        if (!_managers.ContainsKey(typeName))
        {
            _managers.Add(typeName, obj);
        }
    }

    /// <summary>
    /// 添加Unity对象
    /// </summary>
    public T AddManager<T>(string typeName) where T : Component
    {
        object result = null;
        _managers.TryGetValue(typeName, out result);
        if (result != null)
        {
            return (T)result;
        }
        Component c = _game_manager.AddComponent<T>();
        _managers.Add(typeName, c);
        return default(T);
    }

    /// <summary>
    /// 获取系统管理器
    /// </summary>
    public T GetManager<T>(string typeName) where T : class
    {
        if (!_managers.ContainsKey(typeName))
        {
            return default(T);
        }
        object manager = null;
        _managers.TryGetValue(typeName, out manager);
        return (T)manager;
    }

    /// <summary>
    /// 删除管理器
    /// </summary>
    public void RemoveManager(string typeName)
    {
        if (!_managers.ContainsKey(typeName))
        {
            return;
        }
        object manager = null;
        _managers.TryGetValue(typeName, out manager);
        Type type = manager.GetType();
        if (type.IsSubclassOf(typeof(MonoBehaviour)))
        {
            GameObject.Destroy((Component)manager);
        }
        _managers.Remove(typeName);
    }
}
