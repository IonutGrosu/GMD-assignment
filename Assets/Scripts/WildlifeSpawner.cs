using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WildlifeSpawner : MonoBehaviour
{
    [FormerlySerializedAs("chicken")] [SerializeField] private GameObject chickenPrefab;
    
    public void SpawnChickens()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnLocation = FindSpawnLocation();
            Console.WriteLine("Spawning one chicken");
            GameObject chicken = Instantiate(chickenPrefab, transform);
            chickenPrefab.transform.position = spawnLocation;
        }
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
