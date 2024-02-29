using UnityEngine;

public class FlagCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelParser>().NextLevel();
        }
    }
}