using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    public GameObject[] toyPrefabs;

    public int SpawnCount = 15;

    public float SpawnRadius = 50f;
    private void Awake()
    {
        enabled = false;
    }


    public void SpawnToy()
    {
        for(int i =0; i< SpawnCount; i++)
        {
            int RandomIndex = Random.Range(0, toyPrefabs.Length);

            Vector3 randomPos = new Vector3(Random.Range(-SpawnRadius,SpawnRadius),0.5f, Random.Range(-SpawnRadius,SpawnRadius));

            Instantiate(toyPrefabs[RandomIndex],randomPos,Quaternion.identity);
        }
    }
}
