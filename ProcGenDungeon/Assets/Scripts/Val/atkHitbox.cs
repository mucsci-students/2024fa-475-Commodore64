using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class atkHitbox : MonoBehaviour
{
    [SerializeField] private AudioClip atkSound;
    public GameObject player;
    public Player ps;
    public Vector3 mousepos;
    public Vector3 atkDir;
    public float atkAngle;

    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        mousepos = ps.setMouse;

        atkDir = Vector3.Normalize(mousepos);
        transform.position += atkDir;
        atkAngle = Vector3.Angle(new Vector3(0f, 1f, 0f), atkDir);

        if (atkDir.x > 0)
        {
            atkAngle = -atkAngle;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, atkAngle);


        SoundFX.instance.playSound(atkSound, transform, 1f);


        Destroy(gameObject, 0.2f);
    }

    void Update()
    {
    }
}