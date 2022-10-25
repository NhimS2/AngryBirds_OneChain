using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    public GameObject[] EmnemyPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        SpamPig();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpamPig()
    {
        int randomenemy = Random.Range(0, EmnemyPrefabs.Length);
        Vector3 spampos = new Vector3(Random.Range(1, 7), transform.position.y, transform.position.z);
        Instantiate(EmnemyPrefabs[randomenemy], spampos, EmnemyPrefabs[randomenemy].transform.rotation);

    }
}
