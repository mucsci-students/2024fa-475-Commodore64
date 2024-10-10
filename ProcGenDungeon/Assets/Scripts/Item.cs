using System.Collections;
using UnityEngine;
public class Item : MonoBehaviour
{
    public Sprite icon; // Item icon
    public ItemType type;
    public bool isColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        Player player = collision.GetComponent<Player>();

        if (player.tag == "Player" || !collision.GetComponent<projectile>().isWarp)
        {
            player.inventory.Add(this); // Add object to inventory
            Destroy(this.gameObject); // Delete from Scene
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

public enum ItemType { NONE, WEAPON, ARMOR, COLLECTABLE }