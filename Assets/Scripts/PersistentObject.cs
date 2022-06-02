using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{

    private static PersistentObject self = null;

    void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(gameObject);

            //Initialization code goes here
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
