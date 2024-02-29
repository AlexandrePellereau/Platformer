using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager gm = FindObjectOfType<GameManager>();
            gm.AddCoin();
            gm.AddScore(100);
        }
    }
}
