using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnHelper : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shop;

    public void SpawnPlayer()
    {
        Vector3 spawnLocation = FindSpawnLocation();
        player.transform.position = spawnLocation;
        shop.transform.position = spawnLocation + new Vector3(2, 0, 0);
    }

    private Vector3 FindSpawnLocation()
    {
        bool found = false;
        RaycastHit hit = new RaycastHit();

        while(!found)
        // for (int i = 0; i< 10; i++)
        {
            Vector3 origin = new Vector3(Random.Range(0f, 100f), 50, Random.Range(0f, 100f));
            Ray ray = new Ray(origin, Vector3.down * 100);
            Debug.DrawRay(origin, Vector3.down * 100, Color.magenta, 30f);

            if (Physics.Raycast(ray, out hit))
            {
                Tags hitTags;
                hit.collider.gameObject.TryGetComponent(out hitTags);
                if (hitTags != null)
                {
                    
                    if (hitTags.HasTag("Ground"))
                    {
                        found = true;
                        Collider[] hitColliders = Physics.OverlapSphere(hit.point, 25f);
                        foreach (var hitCollider in hitColliders)
                        {
                            print(hitCollider.gameObject.name); // This print does not return any FirTree objects to the console
                            Tags colliderTags = hitCollider.gameObject.GetComponent<Tags>();
                            if (colliderTags != null)
                            {
                                if (colliderTags.HasTag("Tree"))
                                {
                                    found = false;
                                    //  TODO: The OverlapSphere not detecting the trees
                                }
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