using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string bName;
    [SerializeField] float speedMove;
    public bool direction;

    private void Start()
    {
        if (bName == "fireBlast")
        transform.Rotate(0f, 180f, 0f);    
        if (bName == "arrow")
        transform.Rotate(0f, 0f, 90f);
    }

    void Update()
    {
        if (bName == "fireBlast")
            transform.position += transform.right * speedMove * Time.deltaTime;
        if (bName == "arrow")
            transform.position += transform.up * speedMove * Time.deltaTime;
        //    if (direction == true)
        //    {

        /*   }
           if (direction == false)
           {
               transform.position += -1 * transform.right * speedMove * Time.deltaTime;
           }

       }*/
    }

}
