using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{

    public LineRenderer[] lineRenderers; 
    public Transform[] stripPosition; // mảng vị trí
    public Transform center;        // điểm trung tâm
    public Transform idlePosition;

    public Vector3 currentPosition;
    public float maxLeght;          // giới hạn chiều dài
    public float bottomBonndary;

    bool isMouseDown;

    public GameObject birdsPrefab;
    public float birdPositionOffset;
    Rigidbody2D bird;
    Collider2D birdCollider;
    public float force;


    public GameObject PointPrefab;
    public GameObject[] Points;

    public int numberOfPoint ;

    public Vector3 batdau;
    public Vector3 ketthuc;

    public int number;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderers[0].positionCount = 2;                                 //độ dài ban đầu
        lineRenderers[1].positionCount = 2;         
        lineRenderers[0].SetPosition(0, stripPosition[0].position);
        lineRenderers[1].SetPosition(0, stripPosition[1].position);
        CreateBirds();

        Points = new GameObject[numberOfPoint];
        for(int i = 0; i < numberOfPoint; i++)
        {
            Points[i] = Instantiate(PointPrefab,transform.position, Quaternion.identity);
        }
        
    }
    Vector2 PointPosition(float t)      //tạo các point dự đoán đường đi 
    {
        Vector2 Direction = -(currentPosition - center.position) ;
        Vector2 currentPointPos = (Vector2)bird.transform.position + (Direction * force * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }
    void CreateBirds()                  //tạo vật bắn (chim)
    {
        bird = Instantiate(birdsPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;
        bird.isKinematic = true;

    }
    

    // Update is called once per frame
    void Update()
    {

        // sợi dây sẽ kéo dãn
        if (isMouseDown)                        // Khi nhấn chuột
        {
            Vector3 mousePotision = Input.mousePosition;       //Tạo vecto từ điểm ban đầu nhấn xuống của chuột
            mousePotision.z = 10;                   //Xét giá trị của vecto cột z = 10

            currentPosition = Camera.main.ScreenToWorldPoint(mousePotision); 
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition-center.position,maxLeght);   //giới hạn lực kéo
            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
        }
        else
        {
            ResetStrip(); //đưa dây về lại vị trí ban đầu
        }
        for (int i = 1; i < Points.Length; i++)
        {
            Points[i].transform.position = PointPosition(i * 0.1f); //khoảng cách của các point 
        }
    }
    private void OnMouseDown()
    {
        isMouseDown = true;
       
      
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
       
        Shoot();

    }
    void Shoot()
    {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;
        bird = null;
        birdCollider = null;
        Invoke("CreateBirds", 2);
        

    }
    void ResetStrip()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }
    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
        
    }
    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBonndary, 1000);
        return vector;
    }
   

}
