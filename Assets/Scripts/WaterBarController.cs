using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBarController : MonoBehaviour
{
    [SerializeField] Image waterBar;
    public List<Sprite> sprites;
    private int uses;

    private void Start()
    {
        uses =  (int) GameManager.instance.gameData.entries[4].value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        waterBar.sprite = sprites[uses];
    }

    public void Fill()
    {
        uses = 5;
        GameManager.instance.gameData.entries[4].value = uses;
        UpdateBar();
    }

    public void SpendUse() {
        --uses;
        if(uses < 0){uses = 0;}
        GameManager.instance.gameData.entries[4].value = uses;
        UpdateBar();
    }
    
    public int GetUses() {
        return uses;
    }

    public void SetVisible(bool visible) {
        waterBar.gameObject.SetActive(visible);
    }
}
