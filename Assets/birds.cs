using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birds : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision) // phương pháp va chạm
    {
        if (collision.transform.CompareTag("pig"))
        {
            Destroy(gameObject); // sau đó đối tượng bị loại bỏ
        }
        Destroy(gameObject, 0.1f);
    }
}
