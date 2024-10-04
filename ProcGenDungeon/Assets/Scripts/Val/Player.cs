using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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
  Camera cam;
  public Interactable focus;
  public Animator myAnimator;
  public float curSpeed;
  public float horoSpeed;
  public float vertSpeed;
  public Vector3 mousepos;
  public float mosX;
  public float mosY;
  public Vector3 setMouse;

  // Start is called before the first frame update
  void Start()
  {
    myAnimator = GetComponent<Animator>();
    health = 100;
    transform.position = new Vector3(0f, 0f, 0f);
    moveSpeed = 7;
    timePassed = 0;
    moveDirection = new Vector3(0f, -1f, 0f);
    cd = true;
    damage = 20;
    armor = 10;
    cam = Camera.main;
    curSpeed = 0;
  }

  void Update()
  {
    mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousepos.z = 0f;
    mousepos = mousepos - transform.position;

    if (health <= 0)
    {
      Destroy(gameObject);
    }

    horoSpeed = Input.GetAxis("Horizontal");
    vertSpeed = Input.GetAxis("Vertical");

    moveDirection = new Vector3(horoSpeed, vertSpeed, 0f);
    transform.position += Vector3.Normalize(moveDirection) * moveSpeed * Time.deltaTime;


    curSpeed = System.Math.Abs(horoSpeed) + System.Math.Abs(vertSpeed);
    if (System.Math.Abs(horoSpeed) < System.Math.Abs(vertSpeed))
    {
      horoSpeed = 0;
    }
    else
    {
      vertSpeed = 0;
    }

    if (myAnimator != null)
    {
      myAnimator.SetFloat("Run", curSpeed);
      myAnimator.SetFloat("hSpeed", horoSpeed);
      myAnimator.SetFloat("vSpeed", vertSpeed);
    }

    // left mouse button
    if (Input.GetMouseButtonDown(0) && cd)
    {
      RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);

      if (hit.collider != null)
      {
        Debug.Log("PASS 1");
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
          SetFocus(interactable);
        }
        else
        {
          RemoveFocus();
        }
      }
      else
      {
        Debug.Log("FAIL 1");
        myAnimator.SetTrigger("TriAtk");
        StartCoroutine(waiterAnimate());
        StartCoroutine(waiterAtk());
        StartCoroutine(waiter());
      }
    }

    // right mouse button
    if (Input.GetMouseButtonDown(1))
    {

    }

    void SetFocus(Interactable newFocus)
    {
      if (newFocus != focus)
      {
        if (focus != null)
          focus.OnDefocused();
        focus = newFocus;
      }

      newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
      if (focus != null)
        focus.OnDefocused();
      focus = null;
    }

    if (invulne)
    {
      timePassed += Time.deltaTime;
      if (timePassed > 1)
      {
        timePassed = 0;
        invulne = false;
      }
    }

  }

  IEnumerator waiter()
  {
    cd = false;
    yield return new WaitForSeconds(3f);
    cd = true;
  }
  IEnumerator waiterAnimate()
  {
    myAnimator.SetBool("Atk", true);
    yield return new WaitForSeconds(1.02f);
    myAnimator.SetBool("Atk", false);
  }
  IEnumerator waiterAtk()
  {
    setMouse = mousepos;
    mosX = setMouse.x;
    mosY = setMouse.y;
    if (System.Math.Abs(mosX) > System.Math.Abs(mosY))
    {
      if (mosX > 0)
      {
        myAnimator.SetBool("mosRight", true);
      }
      else
      {
        myAnimator.SetBool("mosLeft", true);
      }
    }
    else
    {
      if (mosY > 0)
      {
        myAnimator.SetBool("mosUp", true);
      }
      else
      {
        myAnimator.SetBool("mosDown", true);
      }
    }
    yield return new WaitForSeconds(0.5f);
    Instantiate(attackHitbox, transform.position, Quaternion.identity);
    myAnimator.SetBool("mosUp", false);
    myAnimator.SetBool("mosDown", false);
    myAnimator.SetBool("mosLeft", false);
    myAnimator.SetBool("mosRight", false);
  }
}
