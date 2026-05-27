using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    public GameObject end;
    public void GameOver()
    {
        end.SetActive(true);
    }
}
