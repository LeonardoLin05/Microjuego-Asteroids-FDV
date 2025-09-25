using UnityEngine;

public class Player : MonoBehaviour
{

    private float thrustForce = 100f;
    private float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.tarjectVector = transform.right;
        }

    }
}
