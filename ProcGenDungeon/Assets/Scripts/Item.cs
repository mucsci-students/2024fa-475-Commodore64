using UnityEngine;
public class Item : MonoBehaviour
{
    public Sprite icon; // Item icon
    public ItemType type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player.tag == "Player" || !collision.GetComponent<projectile>().isWarp || collision.GetComponent<ItemType>() != ItemType.NONE)
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(collision.GetComponent<projectile>());
        }
    }
}

public enum ItemType { NONE, WEAPON, ARMOR, COLLECTABLE }