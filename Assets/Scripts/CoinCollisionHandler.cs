using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionHandler : MonoBehaviour
{

    public LogicManager logic;

    void Start() {
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
    }
    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a coin
        if (other.CompareTag("Coin"))
        {
            // Destroy the coin object
            logic.AddScore();
            Destroy(other.gameObject);

            if (logic.playerScore >= 10 ) {
                logic.GameOver();
            }
        }
    }
}
