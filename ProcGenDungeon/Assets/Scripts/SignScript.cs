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
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if(dialogueBox.activeInHierarchy) {
                dialogueBox.SetActive(false);
            } else {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2d(Collider2D col) {
        if(col.CompareTag("Player")) {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
    


}
