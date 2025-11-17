using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemOPManager : MonoBehaviour
{
    public static UseItemOPManager Instance;

    [SerializeField] private GameObject speedItemPre;
    [SerializeField] private GameObject magnetItemPre;

    [SerializeField] int poolSize = 3;

    private Queue<GameObject> speedPool = new Queue<GameObject>();
    private Queue<GameObject> magnetPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        CreatePool(speedItemPre, speedPool);
        CreatePool(magnetItemPre, magnetPool);
    }

    private void CreatePool(GameObject prefab, Queue<GameObject> pool)
    {
        for(int i =0; i< poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Spawn(ItemType type, Vector3 pos)
    {
        switch(type)
        {
            case ItemType.Speed:
                return SpawnFromPool(speedPool, pos);

            case ItemType.Magnet:
                return SpawnFromPool(magnetPool, pos);
        }
        return null;
    }
    
    private GameObject SpawnFromPool(Queue<GameObject> pool, Vector3 pos)
    {
        GameObject obj = pool.Dequeue();

        obj.transform.position = pos;
        obj.SetActive(true);

        pool.Enqueue(obj);
        return obj;
    }
}
