using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitch : MonoBehaviour
{
    public GameObject activeBorder;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            activeBorder.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            activeBorder.SetActive(false);
        }
    }
}
