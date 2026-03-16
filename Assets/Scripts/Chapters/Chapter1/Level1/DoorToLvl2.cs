using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToLvl2 : MonoBehaviour
{
    void Update()
    {
        if(ElectricButton_Lvl1.isOn == true) transform.position = new Vector2(73.75f, -6);
    }
}
