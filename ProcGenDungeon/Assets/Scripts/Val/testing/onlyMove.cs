using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyMove : MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
