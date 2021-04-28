using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcPoolManager : MonoBehaviour
{
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    private static NpcPoolManager instance;

    public static NpcPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NpcPoolManager>();
            }
            return instance;
        }
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }

    public void ReuseNpc(GameObject prefab, Transform position)
    {
        int poolkey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolkey))
        {
            GameObject objectToReuse = poolDictionary[poolkey].Dequeue();
            poolDictionary[poolkey].Enqueue(objectToReuse);
            
            objectToReuse.transform.position = position.position;
            objectToReuse.SetActive(true);
        }
    }
}
