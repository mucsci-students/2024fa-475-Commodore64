using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikingDummy : MonoBehaviour
{
    public Animator myAnimator;
    [SerializeField]
    public AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 12) {
            myAnimator.SetTrigger("Bob");
            myAudioSource.Play();
        }
	}
}
