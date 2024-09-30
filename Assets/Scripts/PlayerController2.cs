using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController2 : MonoBehaviour
{
    private bool hasBronzeCoin = false;
    private bool hasSilverCoin = false;
    private bool hasGoldCoin = false;
    private bool clear = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("bronzeCoin"))
        {
            Destroy(collision.gameObject);
            hasBronzeCoin = true;
        }
        
        else if (collision.gameObject.CompareTag("silverCoin"))
        {
            Destroy(collision.gameObject);
            hasSilverCoin = true;
        }
        
        else if (collision.gameObject.CompareTag("goldCoin"))
        {
            Destroy(collision.gameObject);
            hasGoldCoin = true;
        }
        
        else if (collision.gameObject.CompareTag("goal"))
        {
            if (hasBronzeCoin && hasSilverCoin && hasGoldCoin)
            {
                clear = true;
                Debug.Log("Clear! > _ < ");
            }
        }
    }
}
