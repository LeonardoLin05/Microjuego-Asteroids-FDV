using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject asteroidPrefab;
    private float spawnRatePerMinute = 30f;
    private float spawnRateIncrement = 1f;
    private float xLimit = 6f;
    private float maxLifeTime = 4f;

    private float spawnNext = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2(rand, 8f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxLifeTime);
        }
    }
}
