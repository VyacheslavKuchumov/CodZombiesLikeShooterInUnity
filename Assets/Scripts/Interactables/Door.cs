using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Door : Interactable
{
    [SerializeField]
    private GameObject door;
    [SerializeField] 
    private GameObject player;

    public float doorPrice;
    // Start is called before the first frame update
    void Start()
    {
        promptMessage += doorPrice.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    protected override void Interact()
    {
        if (player.GetComponent<PlayerScore>().CanPlayerBuyThis(doorPrice))
        {
            player.GetComponent<PlayerScore>().SpendScore(doorPrice);
            Destroy(door); // TODO: add door opening animation
        }
        else
        {
            /// do nothing or mb prompt that not enoug money
        }
        
        
        
    }
}
