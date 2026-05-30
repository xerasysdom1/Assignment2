using UnityEngine;

public class EnemyVerticalPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 1.5f;
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
        transform.position += Vector3.up * direction * speed * Time.deltaTime;
        float distanceFromStart = transform.position.y - startPosition.y;
        if (Mathf.Abs(distanceFromStart) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
