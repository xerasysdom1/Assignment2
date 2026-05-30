using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed = 2f;
    public float moveDistance = 3f;
    Vector3 startPosition;
    int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
        float distanceFromStart = transform.position.x - startPosition.x;
        if (Mathf.Abs(distanceFromStart) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
