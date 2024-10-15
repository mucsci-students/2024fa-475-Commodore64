using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingDummy : MonoBehaviour
{
    public Animator myAnimator;
    [SerializeField]
    public AudioSource myAudioSource;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            myAnimator.SetTrigger("Bob");
            myAudioSource.Play();
        }
    }
}
