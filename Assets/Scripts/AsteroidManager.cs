using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Asteroid
{
    public GameObject prefab;
    public Vector3 velocity;
    public Vector3 spin;
}
public class AsteroidManager : MonoBehaviour {
    public GameObject[] asteroidPrefabs;
    public Vector3 halfExtents = new Vector3(6, 0, 4);
    public Vector3 maxVelocity = new Vector3(3, 0, 3);
    public Vector3 maxSpin = new Vector3(360, 360, 360);
    public int nAsteroids = 10;
    public float scale = 1;

    private Queue<Asteroid> asteroidsPool = new Queue<Asteroid>();
    private float rnd(float half)
    {
        return Random.Range(-half, half);
    }
    private Vector3 rndVec3(Vector3 bounds)
    {
        return new Vector3(rnd(bounds.x), rnd(bounds.y), rnd(bounds.z));
    }
    private void randomize(Asteroid asteroid)
    {
        asteroid.prefab.transform.localScale = Vector3.one * scale;
        asteroid.prefab.transform.position = rndVec3(halfExtents);
        asteroid.spin = rndVec3(maxSpin);
        asteroid.velocity = rndVec3(maxVelocity);
    }
    void Start () {
        for (int i = 0; i < nAsteroids; i++)
        {
            Asteroid asteroid = new Asteroid
            {
                prefab = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)])
            };
            randomize(asteroid);
            asteroidsPool.Enqueue(asteroid);
        }
	}
	void Update () {
		foreach(Asteroid asteroid in asteroidsPool)
        {
            var transform = asteroid.prefab.transform;
            transform.Rotate(asteroid.spin * Time.deltaTime);
            transform.Translate(asteroid.velocity * Time.deltaTime, Space.World);
            for (int i = 0; i < 3; ++i)
            {
                if (transform.position[i] > halfExtents[i] || transform.position[i] < -halfExtents[i])
                    randomize(asteroid);
            }
        }
	}
}
