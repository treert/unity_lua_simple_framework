using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;

namespace LuaFramework
{
    public class PanelManager : MonoBehaviour
    {
        /// <summary>
        /// 所有UI的父节点
        /// </summary>
        private Transform _parent;

        Transform Parent
        {
            get
            {
                if (_parent == null)
                {
                    GameObject go = GameObject.Find("Canvas");
                    _parent = go.transform;
                    Util.ClearChild(_parent);
                }
                return _parent;
            }
        }

        public GameObject CreatePanelSync(int lua_id, string asset_name)
        {
            GameObject template = AppFacade.ResManager.GetRes<GameObject>(asset_name);
            GameObject go = GameObject.Instantiate(template) as GameObject;
            //go.name = asset_name;
            //go.layer = LayerMask.NameToLayer("Default");
            go.transform.SetParent(Parent, false);
            LuaBehaviour behaviour = go.AddComponent<LuaBehaviour>();
            behaviour.m_lua_id = lua_id;
            return go;
        }

        /// <summary>
        /// 获取预制物体，不实例化，应该是获取阈值物体吧
        /// </summary>
        /// <param name="asset_name"></param>
        /// <returns></returns>
        public GameObject GetGameObject(string asset_name)
        {
            var go = AppFacade.ResManager.GetRes(asset_name);
            return go;
        }
    }
}