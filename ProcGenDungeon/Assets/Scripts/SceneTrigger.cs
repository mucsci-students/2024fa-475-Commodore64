using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag("Player")) {
            StartCoroutine(LoadLevel(2));
        }
    }

    IEnumerator LoadLevel (int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
