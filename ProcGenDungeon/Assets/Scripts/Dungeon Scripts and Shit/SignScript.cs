using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    [SerializeField]
    public GameObject dialogueBox;
    [SerializeField]
    public TMP_Text dialogueText;
    [SerializeField]
    public string dialogue;
    [SerializeField]
    public bool playerInRange = false;

    [SerializeField]
    private float timeUntilSignDisappears = 3f;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Awake()
    {
        timeUntilSignDisappears = 5f;
    }

    void Update()
    {
        if (playerInRange)
        {
            timeUntilSignDisappears -= Time.deltaTime;
            if (timeUntilSignDisappears <= 0)
            {
                dialogueBox.SetActive(false);
                timeUntilSignDisappears = 3f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
            dialogueBox.SetActive(true);
            dialogueText.text = dialogue;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
            timeUntilSignDisappears = 3f;
        }
    }

}
