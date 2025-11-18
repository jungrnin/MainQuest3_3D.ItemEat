using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemData itemData;
    

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController player = other.GetComponent<PlayerController>();
        
        switch(itemData.type)
        {
            case ItemType.Score:
                ScoreItemData score = (ScoreItemData)itemData;
                player.AddScore(score.scoreItemAmount);
                SFXManager.Instance.PlaySFX(SFXManager.Instance.score);
                ScoreItemManager.Instance.AddScoreItem();
                Destroy(gameObject);
                return;

            case ItemType.Key:
                SFXManager.Instance.PlaySFX(SFXManager.Instance.key);
                ExitDoor.Instance.OpenDoor();
                Destroy(gameObject);
                return;

            case ItemType.Speed:
                SpeedItemData speed = (SpeedItemData)itemData;
                player.GetSpeedItem(speed.addSpeed, speed.speedTime);
                SFXManager.Instance.PlaySFX(SFXManager.Instance.speed);
                UseItemSpawn.Instance.ItemUse(ItemType.Speed);
                gameObject.SetActive(false);
                return;

            case ItemType.Magnet:
                MagnetItemData magnet = (MagnetItemData)itemData;
                player.GetMagnetItem(magnet.addMagnetRange, magnet.magnetTime);
                SFXManager.Instance.PlaySFX(SFXManager.Instance.magnet);
                UseItemSpawn.Instance.ItemUse(ItemType.Magnet);
                gameObject.SetActive(false);
                return;
        }
    }
}
