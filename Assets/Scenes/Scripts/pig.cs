using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    
    public GameObject Spamner;

    private void Start()
    {
        Spamner = GameObject.Find("Spampig");
    }
    private void OnCollisionEnter2D(Collision2D collision) // phương pháp va chạm
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject); // sau đó đối tượng bị loại bỏ
            
            Spamner.GetComponent<Spam>().SpamPig();
        }
    }
}
