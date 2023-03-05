using System;
using System.Collections;
using UnityEngine;

public class TomatoPlant : MonoBehaviour
{
    [SerializeField]
    private GameObject[] growthStagePrefabs;

    [SerializeField]
    private int timeToGrow;

    private int currentGrowthStage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Grow());
    }

    private void OnDisable()
    {
        StopCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Grow()
    {
        for (int i = 0; i < growthStagePrefabs.Length-1; i++)
        {
            yield return new WaitForSeconds(timeToGrow);
            growthStagePrefabs[i].gameObject.SetActive(false);
            growthStagePrefabs[i+1].gameObject.SetActive(true);
        }
    }
}
