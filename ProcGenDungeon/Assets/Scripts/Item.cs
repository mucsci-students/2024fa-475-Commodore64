using System.Collections;
using TMPro;
using UnityEngine;
public class Item : MonoBehaviour
{
    public Sprite icon; // Item icon
    public ItemType type; // Item type
    private bool isColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        Player player = collision.GetComponent<Player>();

        if (player.tag == "Player" || !collision.GetComponent<projectile>().isWarp)
        {
            player.inventory.Add(this); // Add object to inventory
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
            this.gameObject.GetComponent<Collider2D>().enabled = false; // make item non-interactable and disable collider
        }
        else
        {
            // If it is a projectile, just destroy it and do not add to inventory
            Destroy(collision.GetComponent<projectile>());
        }

        StartCoroutine(Reset());
    }
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}

public enum ItemType { NONE, SWORD, HAMMER, ARMOR, COLLECTABLE, HEALTH, ENERGY, HEALTHENERGY }