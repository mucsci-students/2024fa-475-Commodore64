using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  public GameObject attackHitbox;
  public Vector3 moveDirection;
  public float moveSpeed;
  public int currentHealth;
  public int maxHealth = 100;
  public int currentEnergy;
  public int maxEnergy = 100;
  public bool cd;
  public bool invulne;
  public float timePassed;
  public int damage;
  public int armor;
  Camera cam;
  public Animator myAnimator;
  public float curSpeed;
  public float horoSpeed;
  public float vertSpeed;
  public Vector3 mousepos;
  public float mosX;
  public float mosY;
  public Vector3 setMouse;
  public bool isDead;
  public HealthBar healthBar;
  public EnergyBar energyBar;
  public Inventory inventory;

  private void Awake()
  {
    // Initializes Inventory to 18 slots
    inventory = new Inventory(18);
  }

  // Start is called before the first frame update
  void Start()
  {
    myAnimator = GetComponent<Animator>();
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);
    currentEnergy = maxEnergy;
    energyBar.SetMaxEnergy(maxEnergy);
    transform.position = new Vector3(0f, 0f, 0f);
    moveSpeed = 7;
    timePassed = 0;
    moveDirection = new Vector3(0f, -1f, 0f);
    cd = true;
    damage = 20;
    armor = 10;
    cam = Camera.main;
    curSpeed = 0;
    isDead = false;

    DontDestroyOnLoad(this.gameObject);

  }

  void Update()
  {
    if (currentHealth <= 0 && !isDead)
    {
      StartCoroutine(deathTime());
    }
    else if (isDead)
    {
      //do nothing
    }
    else
    {
      mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousepos.z = 0f;
      mousepos = mousepos - transform.position;

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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
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
  }

  IEnumerator waiter()
  {
    cd = false;
    yield return new WaitForSeconds(1.14f);
    cd = true;
  }
  IEnumerator deathTime()
  {
    isDead = true;
    myAnimator.SetBool("dead", true);
    yield return new WaitForSeconds(3f);
    myAnimator.gameObject.GetComponent<Animator>().enabled = false;
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
