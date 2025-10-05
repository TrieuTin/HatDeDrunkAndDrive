using UnityEngine;

public class CarControl2 : MonoBehaviour
{

    private Vector3 Moveforce;
    [SerializeField]
    float MoveSpeed = 10;
   

    float HorizontalAxis = 0f;
    float steerAngle =35f;

    bool collided = false;

    public float backDistance = .8f;
    public float backSpeed = .98f;
    private Vector3 backTarget;
    void Update()
    {
        if (!collided)
        {

            //Move
            Moveforce = transform.forward * MoveSpeed;
            transform.position += Moveforce * Time.deltaTime;
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            GameObject.Find("GM").GetComponent<RoadControl>().Score();
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {

                    HorizontalAxis = Mathf.Clamp(touch.deltaPosition.x / Screen.width * 10f, -1f, 1f);

                    transform.Rotate(Vector3.up * HorizontalAxis * Moveforce.magnitude * 5f * steerAngle * Time.deltaTime);


                }
            }
        }
        else
        {
            /*bat dau lui theo tuyen tinh*/
            transform.position = Vector3.MoveTowards(transform.position, backTarget, MoveSpeed * Time.deltaTime);
            /*neu khoang cach lui duoi .01 thi stop*/
            if (Vector3.Distance(transform.position, backTarget) < 0.01f)
            {
                MoveSpeed = 0f;
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
        {
            
            collided = true;
            MoveSpeed = .98f;
            /*tinh khoang cach lui*/
            backTarget = transform.position - transform.forward * backDistance;
        }

    }

 
}
