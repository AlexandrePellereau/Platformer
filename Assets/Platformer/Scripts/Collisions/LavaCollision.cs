using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
