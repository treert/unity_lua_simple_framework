using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LuaFramework
{
    public class ResourceManager : MonoBehaviour
    {
        Dictionary<string, UnityEngine.Object> m_mResList = new Dictionary<string, UnityEngine.Object>();

        protected void OnDestroy()
        {
            m_mResList.Clear();
            m_mResList = null;
        }

        public GameObject GetRes(string sPath)
        {
            UnityEngine.Object obj;
            if (m_mResList.ContainsKey(sPath))
            {
                obj = m_mResList[sPath];
            }
            else
            {
                obj = Resources.Load(sPath, typeof(GameObject));
                if (obj != null)
                    m_mResList.Add(sPath, obj);
            }

            if (obj == null)
            {
                Debug.LogError("No Resource In Path=" + sPath);
                return null;
            }

            return (GameObject)obj;
        }

        public T GetRes<T>(string sPath) where T : UnityEngine.Object
        {
            UnityEngine.Object obj;
            if (m_mResList.ContainsKey(sPath))
            {
                obj = m_mResList[sPath];
            }
            else
            {
                obj = Resources.Load(sPath);
                if (obj != null)
                    m_mResList.Add(sPath, obj);
            }
            if (obj != null)
                return (T)obj;
            else
            {
                Debug.LogError(sPath + " resources  is  null");
                return null;
            }
        }
    }
}