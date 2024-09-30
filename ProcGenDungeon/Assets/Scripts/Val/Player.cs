using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public GameObject attackHitbox;
    public Vector3 moveDirection;
    public float moveSpeed;
    public int health;   
    public bool cd;
    public bool invulne;
    public float timePassed;
    public int damage;
    public int armor;

    // Start is called before the first frame update
    void Start(){
      
      health = 100;
      transform.position = new Vector3(5f, 5f, 0f);
      moveSpeed = 2;
      timePassed = 0;
      moveDirection = new Vector3(0f, -1f, 0f);
      cd = true;
      damage = 20;
      armor = 10;
    }

    // Update is called once per frame
    void Update(){
      if(health <= 0){
        Destroy(gameObject);
      }

      moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
      transform.position += moveDirection * moveSpeed * Time.deltaTime;

      if (Input.GetMouseButtonDown(0) && cd){
        Instantiate(attackHitbox, transform.position, Quaternion.identity);
        StartCoroutine(waiter());
      }

      if(invulne){
        timePassed += Time.deltaTime;
        if (timePassed > 3){
          timePassed = 0;
          invulne = false;
        }
      }
  
    }


    IEnumerator  waiter(){
      cd = false;
      yield return new WaitForSeconds(1f);
      cd = true;
    }
}
