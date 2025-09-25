using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 5f;
    private float maxLifeTime = 5f;
    public Vector3 tarjectVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * tarjectVector * Time.deltaTime);
    }
}
