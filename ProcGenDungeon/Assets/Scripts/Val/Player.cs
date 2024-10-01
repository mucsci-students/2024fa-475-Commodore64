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
  Camera cam;
  public Interactable focus;

  // Start is called before the first frame update
  void Start()
  {

    health = 100;
    transform.position = new Vector3(5f, 5f, 0f);
    moveSpeed = 2;
    timePassed = 0;
    moveDirection = new Vector3(0f, -1f, 0f);
    cd = true;
    damage = 20;
    armor = 10;
    cam = Camera.main;
  }

  void Update()
  {
    if (health <= 0)
    {
      Destroy(gameObject);
    }

    moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    transform.position += moveDirection * moveSpeed * Time.deltaTime;

    if (Input.GetMouseButtonDown(0) && cd)
    {
      Instantiate(attackHitbox, transform.position, Quaternion.identity);
      StartCoroutine(waiter());
    }

    if (Input.GetMouseButtonDown(1))
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
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
      if (timePassed > 3)
      {
        timePassed = 0;
        invulne = false;
      }
    }

  }

  IEnumerator waiter()
  {
    cd = false;
    yield return new WaitForSeconds(1f);
    cd = true;
  }
}
