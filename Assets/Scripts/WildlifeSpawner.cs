using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WildlifeSpawner : MonoBehaviour
{
    [FormerlySerializedAs("chicken")] [SerializeField] private GameObject chickenPrefab;
    
    public void SpawnChickens()
    {
        for (var i = 0; i < 10; i++)
        {
            var spawnLocation = FindSpawnLocation();
            Console.WriteLine("Spawning one chicken");
            var chicken = Instantiate(chickenPrefab, transform);
            chickenPrefab.transform.position = spawnLocation;
        }
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
                        var hitColliders = Physics.OverlapSphere(hit.point, 25f);
                        foreach (var hitCollider in hitColliders)
                        {
                            var colliderTags = hitCollider.gameObject.GetComponent<Tags>();
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
