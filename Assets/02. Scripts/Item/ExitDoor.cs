using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public static ExitDoor Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
