using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBox : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void DestroyTrigger()
    {
        anim.SetTrigger("destroy");
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
