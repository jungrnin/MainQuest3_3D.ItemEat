using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemSpawn : MonoBehaviour
{
    public static UseItemSpawn Instance;

    public string speedPool = "SpeedItem";
    public string magnetPool = "MagnetItem";

    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float respawnDelay = 10f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SpawnRandom(ItemType.Speed);
        SpawnRandom(ItemType.Magnet);
    }
    public void ItemUse(ItemType type)
    {
        StartCoroutine(RespawnRoutine(type));
    }

    private IEnumerator RespawnRoutine(ItemType type)
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnRandom(type);
    }
    private void SpawnRandom(ItemType type)
    {
        int index = Random.Range(0, spawnPoint.Length);
        Transform point = spawnPoint[index];


        UseItemOPManager.Instance.Spawn(type, point.position);
    }
}
