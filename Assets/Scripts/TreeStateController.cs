using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStateController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer renderer;
    ResourceNode resourceNode;

    [SerializeField] Sprite grownUp;
    [SerializeField] Sprite cutted;
    [SerializeField] AudioClip OnWoodSplinters;

    public Tree treeData;

    public bool growing;
    public int growingTime;
    private int timeToGrow = 400;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        resourceNode = GetComponent<ResourceNode>();

        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += Growing;
    }

    void Growing()
    {
        if(growing)
        {
            ++growingTime;

            if(growingTime % 96 == 0)
            {
                growing = false;
                growingTime = 0;

                treeData.growing = false;
                treeData.growingTime = 0;

                animator.SetTrigger("grownUp");

                
                resourceNode.lifeTree = 3;
                resourceNode.dropCount = 3;
            }
            treeData.growingTime = growingTime;
        }
    }

    public void Cut()
    {
        AudioManager.instance.Play(OnWoodSplinters);
        animator.SetTrigger("cutted");
        growing = true;
        treeData.growing = true;
    }

}
