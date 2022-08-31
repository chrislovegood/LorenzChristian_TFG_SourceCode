using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;
    Vector3 targetPosition;
    [SerializeField] Tilemap  targetTilemap;
    [SerializeField] PlaceableObjectsReferenceManager  placeableObjectsReferenceManager;
    SpriteRenderer spriteRenderer;

    
    bool canSelect;
    bool show;

    public bool CanSelect
    {   
        set {
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    public bool Show
    {   
        set {
            show = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    private void Awake()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {   
        targetPosition = targetTilemap.CellToWorld(cellPosition);
        transform.position = targetPosition + targetTilemap.cellSize/2;

        Vector3Int position = Vector3Int.FloorToInt(transform.position);

        if(placeableObjectsReferenceManager.Check(position) == true)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }
    
    internal void Set(Sprite icon)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = icon;
    }
}
