using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameObject collectibleSoundPrefab;
    public float soundDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.collectiblesFound += 1;
                InGameUI.UpdateCollectibleCountText(player.collectiblesFound);
            }
            Instantiate(collectibleSoundPrefab);
            Destroy(gameObject);
        }
    }

}