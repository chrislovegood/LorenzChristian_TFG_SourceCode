using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    Transform transform;

    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLive = 10f;

    public Item item;
    public int count = 1;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
        transform = GetComponent<Transform>();
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon; 
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Update()
    {
        timeToLive -= Time.deltaTime;

        if(timeToLive < 0)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        
        if(distance > pickUpDistance || !GameManager.instance.inventoryContainer.HasFreeSpace())
        {
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position, 
            player.position,
            speed * Time.deltaTime
        );
        if(distance < 0.2f )
        {
            if (GameManager.instance.inventoryContainer != null)
            {
                if(GameManager.instance.inventoryContainer.HasFreeSpace())
                {
                    GameManager.instance.inventoryContainer.Add(item, count);
                    Destroy(gameObject);
                }
            }
            else 
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }
        }
    }
}
