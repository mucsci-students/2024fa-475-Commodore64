using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col) {
        SceneManager.LoadScene(2);
    }
}
