using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : MonoBehaviour
{
    [SerializeField] LogData logItem;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.mangoCount++;
            LogScript.instance.AddItem(logItem);
            Destroy(gameObject);
        }
    }
}
