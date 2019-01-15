using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static Dictionary<GameObject, List<GameObject>> poolAvailable = new Dictionary<GameObject, List<GameObject>>();
    private static Dictionary<GameObject, GameObject> PrefabMapper = new Dictionary<GameObject, GameObject>();
    private static int SizeToAddAtFull = 5;
    private static ObjectPool Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        
    }

    private void OnLevelWasLoaded(int level)
    {
        poolAvailable.Clear();
        PrefabMapper.Clear();
    }
   
    private GameObject GetObjectFromPool(GameObject objKey)
    {
        if (poolAvailable[objKey].Count <= 0) return null;
        GameObject obj = poolAvailable[objKey][0];
        PrefabMapper[obj] = objKey;
        poolAvailable[objKey].Remove(obj);
        obj.transform.localScale = objKey.transform.localScale;
        obj.transform.rotation = objKey.transform.rotation;
        obj.transform.position = objKey.transform.position;
        obj.SetActive(true);

        return obj;
    }
    //Public facing methods
    public static void Destroy(GameObject Object)
    {
        if (PrefabMapper.ContainsKey(Object))
        {
            poolAvailable[PrefabMapper[Object]].Add(Object);
            Object.SetActive(false);
        }
        else
            UnityEngine.Object.Destroy(Object);
    }
    public static GameObject Instantiate(GameObject Prefab, Transform Parent)
    {
        GameObject temp = Instantiate(Prefab);
        temp.transform.parent = Parent;
        temp.transform.localPosition = Vector3.zero;
        return temp;
    }
    public static GameObject Instantiate(GameObject Prefab, Vector3 Posistion)
    {
        GameObject temp = Instantiate(Prefab);
        temp.transform.position = Posistion;
        return temp;
    }
    public static GameObject Instantiate(GameObject Prefab, Vector3 Posistion, Quaternion rotation)
    {
        GameObject temp = Instantiate(Prefab);
        temp.transform.position = Posistion;
        temp.transform.rotation = rotation;
        return temp;
    }
    public static GameObject Instantiate(GameObject Prefab)
    {
        if (!poolAvailable.ContainsKey(Prefab)) poolAvailable[Prefab] = new List<GameObject>();
        if (poolAvailable[Prefab].Count <= 0)
        {
            //Add object to fill out pool
            for (int i = 0; i < SizeToAddAtFull; i++)
            {
                GameObject temp = GameObject.Instantiate(Prefab);
                temp.transform.parent = Instance.transform;
                temp.SetActive(false);
                poolAvailable[Prefab].Add(temp);
            }
        }
        return Instance.GetObjectFromPool(Prefab);
    }
}
