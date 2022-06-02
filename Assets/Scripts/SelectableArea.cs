using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableArea : MonoBehaviour
{

    public bool selected = false;

    GameObject border;

    // Start is called before the first frame update
    void Start()
    {
        border = transform.Find("Border").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        border.SetActive(selected);
    }
}
