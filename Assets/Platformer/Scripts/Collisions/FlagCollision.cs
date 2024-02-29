using UnityEngine;

public class FlagCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelParser>().NextLevel();
            Destroy(gameObject);
        }
    }
}