using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemManager : MonoBehaviour
{
    public static ScoreItemManager Instance;

    public int totalScoreItem;
    public int currentScoreItem;

    [SerializeField] private GameObject keyPrefap;
    [SerializeField] private Transform[] keySpawnPoint;

    private bool isKeySpawn = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScoreItem()
    {
        currentScoreItem++;

        if (!isKeySpawn && currentScoreItem >= totalScoreItem)
        {
            SpawnKeyItem();
            isKeySpawn = true;
        }
    }

    private void SpawnKeyItem()
    {
        Transform point = keySpawnPoint[Random.Range(0, keySpawnPoint.Length)];
        Instantiate(keyPrefap, point.position, Quaternion.identity);

    }
}
