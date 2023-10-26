using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawnerScript : MonoBehaviour
{

    public GameObject blob;
    public float spawnRate = 2;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnBlob();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnBlob();
            timer = 0;
        }
    }

    void spawnBlob()
    {
        Instantiate(blob, new Vector3(Random.Range(-7, 7), Random.Range(-5, 5), 0), transform.rotation);
    }
}
