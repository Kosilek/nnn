using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public static void Destroy(GameObject gameObject, Animator anim)
    {
        anim.SetBool("Death", true);
        Destroy(gameObject, 0.5f);
    }
}
