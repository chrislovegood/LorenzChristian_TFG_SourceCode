using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public List<InventoryButton> buttons;
    public ItemContainer storage;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void Show()
    {
        
    }

    public virtual void OnClick(int id)
    {

    }
}
