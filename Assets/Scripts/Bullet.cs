using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    private float speed = 5f;
    public Vector3 tarjectVector;

    private float xBorderLimit = 12.5f;
    private float yBorderLimit = 5.8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * tarjectVector * Time.deltaTime);
        Limit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos " + Player.SCORE;
    }
    
    private void Limit()
    {
        Vector3 pos = transform.position;

        if (pos.x > xBorderLimit || pos.x < -xBorderLimit
        || pos.y > yBorderLimit || pos.y < -yBorderLimit)
        {
            gameObject.SetActive(false);
        }
    } 
}
