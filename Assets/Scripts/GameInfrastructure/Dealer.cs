using UnityEngine;

public class Dealer : MonoBehaviour
{
    public Transform player;
    public Transform triggerPoint;
    public Transform[] items;
    public GameObject[] itemsInfo;

    [Header("Distasnce parameters")]
    public float interactDistance;
    public float showDistance;


    private void FixedUpdate()
    {
        if (Vector2.Distance(triggerPoint.position, player.position) < interactDistance)
        {
            ShowClosestItem();
        }
        else
        {
            HideItems();
        }
    }

    void HideItems()
    {
        for (int i = 0; i < itemsInfo.Length; i++)
        {
            itemsInfo[i].SetActive(false);
        }
    }

    void ShowClosestItem()
    {
        float shortestDistance = 10000;
        int itemToShowIndex = 0;

        for (int i = 0; i < items.Length; i++)
        {
            Transform item = items[i];
            if(item != null && Vector2.Distance(item.position, player.position) < shortestDistance)
            {
                itemToShowIndex = i;
                shortestDistance = Vector2.Distance(item.position, player.position);
            }
            else
            {
                
                shortestDistance = 10000;
            }
        }

        if(itemsInfo.Length == items.Length)
        {
            for (int i = 0; i < itemsInfo.Length; i++)
            {
                if(i != itemToShowIndex)
                {
                    itemsInfo[i].SetActive(false);
                }
                else
                {
                    //print("activating item info: "+ itemToShowIndex);
                    itemsInfo[itemToShowIndex].SetActive(true);
                }
            }
        }


    }
}
