using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnHelper : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shop;

    public void SpawnPlayer()
    {
        var spawnLocation = FindSpawnLocation();
        player.transform.position = spawnLocation;
        shop.transform.position = spawnLocation + new Vector3(2, -1f, 0);
    }

    private Vector3 FindSpawnLocation()
    {
        var found = false;
        var hit = new RaycastHit();

        while(!found)
        {
            var origin = new Vector3(Random.Range(0f, 100f), 50, Random.Range(0f, 100f));
            var ray = new Ray(origin, Vector3.down * 100);
            Debug.DrawRay(origin, Vector3.down * 100, Color.magenta, 30f);

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.TryGetComponent(out Tags hitTags);
                if (hitTags != null)
                {
                    if (hitTags.HasTag("Ground"))
                    {
                        found = true;
                        var hitColliders = Physics.OverlapSphere(player.transform.position, 5f);
                        foreach (var hitCollider in hitColliders)
                        {
                            var colliderTags = hitCollider.GetComponent<Tags>();

                            if (colliderTags != null && colliderTags.HasTag("Tree"))
                            {
                                found = false;
                            }
                        }
                    }
                }
            }
        }

        if (found)
        {
            return hit.point + Vector3.up;
        }
        
        return Vector3.one;
    }
}