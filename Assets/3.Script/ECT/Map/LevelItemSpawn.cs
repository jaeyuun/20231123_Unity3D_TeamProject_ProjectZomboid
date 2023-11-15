using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] objPrefabs;
    [SerializeField] private Transform[] spanwnLocation;

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Generate();
        }
    }
    private void Generate()
    {
        for (int i = 0; i < spanwnLocation.Length; i++)
        {
            if (spanwnLocation[i].childCount > 0)
            {
                Destroy(spanwnLocation[i].GetChild(0).gameObject);
            }

            int ranNum = Random.Range(0, objPrefabs.Length);
            GameObject obj = Instantiate(objPrefabs[ranNum], spanwnLocation[i].position, objPrefabs[ranNum].transform.rotation);
            obj.transform.SetParent(spanwnLocation[i]);
        }
    }
}
