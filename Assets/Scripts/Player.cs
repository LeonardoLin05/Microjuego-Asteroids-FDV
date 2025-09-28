using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private float thrustForce = 16f;
    private float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;

    public static int SCORE = 0;

    private float xBorderLimit = 10f;
    private float yBorderLimit = 5.5f;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Limit();

        float rotation = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce * 6.25f);
        ControlSpeed();

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (!PauseMenu.gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Has disparado una bala");
                GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
                
                if (bullet != null)
                {
                    bullet.transform.position = gun.transform.position;
                    bullet.SetActive(true);
                    Bullet balaScript = bullet.GetComponent<Bullet>();
                    balaScript.tarjectVector = transform.right;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Comprueba si la nave se ha salido del límite del mapa
    /// </summary>
    private void Limit()
    {
        Vector3 pos = transform.position;

        if (pos.x > xBorderLimit)
        {
            pos.x = -xBorderLimit;
        }
        else if (pos.x < -xBorderLimit)
        {
            pos.x = xBorderLimit;
        }
        else if (pos.y > yBorderLimit)
        {
            pos.y = -yBorderLimit;
        }
        else if (pos.y < -yBorderLimit)
        {
            pos.y = yBorderLimit;
        }
        transform.position = pos;
    }

    /// <summary>
    /// Controla que tu velocidad no se pase de cierto límite para
    /// no estar acelerando continuamente y alcanzar velocidades
    /// demasiado altas
    /// </summary>
    private void ControlSpeed()
    {
        Vector3 shipSpeed = _rigid.linearVelocity;
        if (shipSpeed.magnitude > thrustForce)
        {
            shipSpeed = shipSpeed.normalized * thrustForce;
            _rigid.linearVelocity = shipSpeed;
        }
    }
}
