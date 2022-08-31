using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{

    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;
    public int lifeTree = 3;

    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    public int dropCount = 3;
    [SerializeField] ResourceNodeType nodeType;
    
    Animator animator;
    TreeStateController controller;

     private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<TreeStateController>();
    }

    public override void Hit()
    {
        animator.SetTrigger("hit");
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

    private void processHit(){
        lifeTree -= 1;

        if(lifeTree == 0)
        {
            while(dropCount > 0)
            {
                dropCount -= 1;

                Vector3 position = transform.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;

                ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
            }
            controller.Cut();
        }
    }
}
