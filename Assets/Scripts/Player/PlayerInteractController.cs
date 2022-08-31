using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody2D rigidbody2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] HighlightController hightlightController;
    public Interactable lastInteraction;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
   {
       Check();

       if(Input.GetKeyDown(KeyCode.E))
       {
            Interact();
       }
   }

   private void Check()
   {
        Vector2 position = rigidbody2d.position + playerController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable interaction = c.GetComponent<Interactable>();
            
            if(interaction != null)
            {
                hightlightController.Hightlight(interaction.gameObject);
                return;
            }
        }
        
        hightlightController.Hide();

        if(lastInteraction != null)
        {
            lastInteraction.OnEndedInteraction();
            lastInteraction = null;
        }
   }

   private void Interact()
   {
        Vector2 position = rigidbody2d.position + playerController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable interaction = c.GetComponent<Interactable>();
            
            if(interaction != null)
            {
                if(lastInteraction != null && lastInteraction != interaction) 
                {
                    lastInteraction.OnEndedInteraction();
                    lastInteraction = null;
                }

                if(lastInteraction != interaction)
                {
                    interaction.Interact();
                    lastInteraction = interaction;
                }
                else
                {
                    interaction.finishInteraction();
                    lastInteraction = null;
                }

                break;
            }
        }
   }
}
