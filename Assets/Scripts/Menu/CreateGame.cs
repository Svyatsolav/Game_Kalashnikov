using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{
    public void ChangeWorldType(int value)
    {
        PlayerPrefs.SetInt("WorldType", value);
        Debug.Log(value);
    }
}
