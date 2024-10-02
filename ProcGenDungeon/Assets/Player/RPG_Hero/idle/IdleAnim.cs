using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour
{
    private Animator myAnimator;
    private Sprite mySprite;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myAnimator != null){
            if(Input.GetKeyDown(KeyCode.W)){
                myAnimator.SetTrigger("TriUp");
            }
            else if(Input.GetKeyDown(KeyCode.S)){
                myAnimator.SetTrigger("TriDown");
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                myAnimator.SetTrigger("TriRight");
            }
            else if(Input.GetKeyDown(KeyCode.A)){
                myAnimator.SetTrigger("TriLeft");
            }
        }
    }
}
