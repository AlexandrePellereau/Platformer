using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().AddScore(100);
        }
    }
}
