/* 
    LuaFramework Code By Jarjin lee
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaFramework
{
    public class AppFacade
    {
        private static GameObject _game_object;
        public static GameObject RootObject
        {
            get
            {
                return _game_object;
            }
        }

        public static void StartUp(GameObject game_object)
        {
            _game_object = game_object;

            AddManager<LuaManager>();
            AddManager<ResourceManager>();
            AddManager<PanelManager>();
            AddManager<GameManager>();
        }

        private static Dictionary<string, object> _managers = new Dictionary<string, object>();
        /// <summary>
        /// 添加管理器
        /// </summary>
        public static T AddManager<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            var type_name = type.Name;

            object result = null;
            _managers.TryGetValue(type_name, out result);
            if(result == null)
            {
                result = RootObject.AddComponent<T>();
                _managers.Add(type_name, result);
            }

            return (T)result;
        }

        /// <summary>
        /// 获取管理器
        /// </summary>
        public static T GetManager<T>() where T : class
        {
            var type = typeof(T);
            var type_name = type.Name;

            object manager = null;
            _managers.TryGetValue(type_name, out manager);
            return (T)manager;
        }

        /**    管理器快捷访问     **/

        public static LuaManager LuaManager
        {
            get
            {
                return GetManager<LuaManager>();
            }
        }

        public static ResourceManager ResManager
        {
            get
            {
                return GetManager<ResourceManager>();
            }
        }
    }
}