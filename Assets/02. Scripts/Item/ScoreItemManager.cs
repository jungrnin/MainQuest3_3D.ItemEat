using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemManager : MonoBehaviour
{
    public static ScoreItemManager Instance;

    public static event Action<int, int> OnScoreChanged;
    public static event Action OnKeySpawn;

    public int totalScoreItem;
    public int currentScoreItem;

    [SerializeField] private GameObject keyPrefap;
    [SerializeField] private Transform[] keySpawnPoint;

    private bool isKeySpawn = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnScoreChanged?.Invoke(currentScoreItem, totalScoreItem);
    }
    public void AddScoreItem()
    {
        currentScoreItem++;

        OnScoreChanged?.Invoke(currentScoreItem, totalScoreItem);

        if (!isKeySpawn && currentScoreItem >= totalScoreItem)
        {
            SpawnKeyItem();
            isKeySpawn = true;
        }
    }

    private void SpawnKeyItem()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.scoreAllSelec);
        Transform point = keySpawnPoint[UnityEngine.Random.Range(0, keySpawnPoint.Length)];
        Instantiate(keyPrefap, point.position, Quaternion.identity);

        OnKeySpawn?.Invoke();
    }
}
