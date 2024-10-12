using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static SoundFX instance;
    [SerializeField] private AudioSource sFXobject;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
    }

    public void playSound(AudioClip aclip, Transform spawnTransform, float volume){
        // spawn in oibject
        AudioSource aSource = Instantiate(sFXobject, spawnTransform.position, Quaternion.identity);
        // assign clip
        aSource.clip = aclip;
        // assign volume
        aSource.volume = volume;
        //play sound
        aSource.Play();
        //get length of clip
        float clipLength = aSource.clip.length;
        //destory clip
        Destroy(aSource.gameObject, clipLength);
    }
}
