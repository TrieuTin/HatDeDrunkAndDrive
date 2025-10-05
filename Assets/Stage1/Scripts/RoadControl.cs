using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControl : MonoBehaviour
{
    public GameObject Road;
    public float roadLength = 20f;
   // public float speed = 5f;

    public List<GameObject> Obstacles;

    int ListCount { get => instantRoad.Count > 0 ? instantRoad.Count : 0; }
    private List<GameObject> instantRoad = new List<GameObject>();
    public TMPro.TMP_Text score_text;

    float score_text_size = 0.05f;
    float current_score = 0;
    void Start()
    {
        // Spawn 
        for (int i = 0; i < 2; i++)
        {
            Spawn(i * roadLength);
        }

        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        // Move roads
        //foreach (var road in instantRoad)
        //{
        //    if (road != null)
        //    {
        //        road.transform.Translate(Vector3.back * speed * Time.deltaTime);
        //    }
        //}

       // current_score += score_text_size * Time.deltaTime;

       // score_text.text = current_score.ToString("F2") + "Km";
       // Debug.Log(score_text.text);
    }
    public void Score()
    {
        current_score += score_text_size * Time.deltaTime;

        score_text.text = current_score.ToString("F2") ;
    }
    void Spawn(float zPos)
    {
        
        GameObject newRoad = Instantiate(Road, new Vector3(0f, 0f, zPos), Quaternion.identity);

        instantRoad.Add(newRoad);

        

        roadLength = instantRoad[ListCount - 1].GetComponent<Collider>().bounds.size.z ;

       
    }
   void SpawnNext()
    {
        //lay do dai cua doan duong chia 2
        
        roadLength = instantRoad[ListCount - 1].GetComponent<Collider>().bounds.size.z / 2;
        //lay diem origin z cua doan duong cong cho nua doan duong nhan voi 2
        var newZ = instantRoad[ListCount-1 ].transform.position.z + roadLength *2;
   
        Spawn(newZ);
        SpawnObstacle(newZ);
    }

    void SpawnObstacle(float newZ)
    {
        if (Obstacles.Count > 0)
        {
            var currentZ = Vector3.zero;
            for (int i = 0; i < 6; i++)
            {
                if (currentZ != Vector3.zero)
                {

                    var pos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(newZ - 5, newZ + roadLength - 10));

                    currentZ = pos;

                    var obs = Instantiate(Obstacles[Random.Range(0, 2)], pos, Quaternion.identity);

                    obs.transform.SetParent(instantRoad[ListCount - 1].transform, true);
                }
                else
                {
                    var pos = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(newZ - 5, newZ + currentZ.z));

                    currentZ = pos;

                    var obs = Instantiate(Obstacles[Random.Range(0, 2)], pos, Quaternion.identity);

                    obs.transform.SetParent(instantRoad[ListCount - 1].transform, true);
                    
                }
            }

        }
     

    }


    bool IsOverScreen()
    {
        Camera cam = Camera.main;
        Renderer rend = instantRoad[0].GetComponent<Renderer>();

        Vector3 topPoint = rend.bounds.max;

        Vector3 screenPoint = cam.WorldToScreenPoint(topPoint);      
        return (screenPoint.y) < 0 || (screenPoint.y) < -100;


    }
 
    void DestroyRoad()
    {
        if (IsOverScreen())
        {
            if (instantRoad.Count > 0)
            {

                Destroy(instantRoad[0]);
                instantRoad.RemoveAt(0);
            }
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            DestroyRoad();

            if (instantRoad.Count < 2) 
            {
                SpawnNext();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
