using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public List<T> objPool;
    public void Awake()
    {
        objPool = new List<T>();

    }

    public T GetOrCreateFromPool(T objPrefab, Vector2 position, Transform transform)
    {
        if (objPool.Count > 0)
        {
            T obj = objPool[objPool.Count - 1];
            objPool.RemoveAt(objPool.Count - 1);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(objPrefab, position, Quaternion.identity, transform);
        }
    }
    public void ReturnToPool(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            objPool.Add(obj);

        }
    }
}
