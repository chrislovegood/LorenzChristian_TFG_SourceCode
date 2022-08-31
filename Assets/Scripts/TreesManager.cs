using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreesManager : MonoBehaviour
{
    [SerializeField] TreesContainer container;
    [SerializeField] GameObject normalTreePrefab;
    [SerializeField] GameObject fruitTreePrefab;
    [SerializeField] Tilemap targetTilemap;

    public List<Sprite> variations;
    public List<Item> fruits;

    void Start()
    {
        VisualizeMap();
    }

    void OnDestroy()
    {
        FileHandler.SavetoJSON<Tree>(container.trees,"trees.json");
    }

    private void VisualizeMap()
    {
        for(int i = 0; i < container.trees.Count; i++)
        {
           VisualizeItem(container.trees[i]);
        }
    }

    private void VisualizeItem(Tree tree)
    {
        switch(tree.type)
        {
            case TypeTree.NormalTree:

                GameObject go = Instantiate(normalTreePrefab);
                go.transform.parent = transform;

                Vector3 position = targetTilemap.WorldToCell(tree.position) + targetTilemap.cellSize/2;
                position -= Vector3.forward * 0.1f;
                go.transform.position = position;

                TreeStateController controller = go.GetComponent<TreeStateController>();
                controller.treeData = tree;
                controller.growing = tree.growing;
                controller.growingTime = tree.growing ? tree.growingTime : 0;

                if(controller.growing)
                {
                    go.GetComponent<Animator>().SetTrigger("cutted");
                }
                break;

            case TypeTree.FruitTree:

                GameObject go2 = Instantiate(fruitTreePrefab);
                go2.transform.parent = transform;

                Vector3 position2 = targetTilemap.WorldToCell(tree.position) + targetTilemap.cellSize/2;
                position2 -= Vector3.forward * 0.1f;
                go2.transform.position = position2;

                SpriteRenderer renderer = go2.GetComponent<SpriteRenderer>();
                ItemSpawner spawner = go2.GetComponent<ItemSpawner>();

                switch(tree.fruit)
                {
                    case Fruit.Apple:
                        renderer.sprite = variations[0];
                        spawner.toSpwan = fruits[0];
                        break;

                    case Fruit.Orange:
                        renderer.sprite = variations[1];
                         spawner.toSpwan = fruits[1];
                        break;

                     case Fruit.Pear:
                        renderer.sprite = variations[2];
                        spawner.toSpwan = fruits[2];
                        break;
                }

                break;
        }
        
    }   


}
