using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthBar : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public Sprite spriteEmpty;
    public Sprite spriteHalf;
    public Sprite spriteFull;

    private List<Image> hearthImage;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        hearthImage = new List<Image>();
        foreach(Transform child in transform)
        {
            hearthImage.Add(child.gameObject.GetComponent<Image>());
        }
    }

    private void Update()
    {
        for(int i=0; i<hearthImage.Count; i++)
        {
            if(playerHealth.CurrentHealth <= i*2)
            {
                hearthImage[i].sprite = spriteEmpty;
            }
            else if (playerHealth.CurrentHealth == i*2+1)
            {
                hearthImage[i].sprite = spriteHalf;
            }
            else
            {
                hearthImage[i].sprite = spriteFull;
            }
        }
    }
}
