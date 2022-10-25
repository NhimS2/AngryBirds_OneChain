using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject Spamner;

    private void OnCollisionEnter2D(Collision2D collision) // phương pháp va chạm
    {
        if (collision.transform.CompareTag("pig"))
        {
            Destroy(gameObject); // sau đó đối tượng bị loại bỏ

            Spamner.GetComponent<Spam>().SpamPig();
        }
    }
}
