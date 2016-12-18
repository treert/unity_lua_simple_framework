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
                    GameObject go = GameObject.Find("NguiCamera");
                    _parent = go.transform;
                    Util.ClearChild(_parent);
                }
                return _parent;
            }
        }

        public Transform CreateLayer(int layer_order)
        {
            Transform tr = Parent.FindChild("layer_" + layer_order);
            if(tr == null)
            {
                GameObject go = new GameObject();
                var rect = go.AddComponent<RectTransform>();
                rect.anchorMax = new Vector2(1, 1);
                rect.anchorMin = new Vector2(0, 0);
                rect.offsetMin = new Vector2(0, 0);
                rect.offsetMax = new Vector2(0, 0);
                go.name = "layer_" + layer_order;
                go.transform.SetParent(_parent, false);
                var canvas = go.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = layer_order;
                go.AddComponent<GraphicRaycaster>();

                tr = rect;
            }
            return tr;
        }

        public Transform CreateNguiLayer(int layer_order)
        {
            Transform tr = Parent.FindChild("ngui_layer_" + layer_order);
            if (tr == null)
            {
                GameObject go = new GameObject();
                var ui_panel = go.AddComponent<UIPanel>();
                ui_panel.depth = layer_order;
                go.name = "layer_" + layer_order;
                go.transform.SetParent(_parent, false);

                tr = go.transform;
            }
            return tr;
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