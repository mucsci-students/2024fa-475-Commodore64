using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zomHealth : MonoBehaviour
{

    public int hb;
    public Vector3 scaling;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        hb = 100;
        offset = new Vector3 (0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + offset;
        scaling = new Vector3 (hb/300f, 0.025f, 0f);
        transform.localScale = scaling;

    }
}
