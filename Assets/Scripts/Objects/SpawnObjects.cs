using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] GameObject[] allObjects;

    public void SpawnObject(int id)
    {
        Instantiate(allObjects[id]);
    }
}
