using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnVent[] vents;
    [SerializeField] private GameObject[] bugPrefabs;
    [SerializeField] private float warningTime = 1f;
    [SerializeField] private float startInterval = 3f;
    [SerializeField] private float minInterval = 0.6f;
    [SerializeField] private float rampDuration = 120f; // seconds to reach minInterval
    [SerializeField] private PlayerHealth player;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (player == null || !player.IsDead)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / rampDuration);
            float interval = Mathf.Lerp(startInterval, minInterval, t);
            yield return new WaitForSeconds(interval);

            SpawnVent vent = vents[Random.Range(0, vents.Length)];
            vent.SetWarning(true);
            yield return new WaitForSeconds(warningTime);
            vent.SetWarning(false);

            GameObject prefab = bugPrefabs[Random.Range(0, bugPrefabs.Length)];
            Instantiate(prefab, vent.transform.position, Quaternion.identity);
        }
    }
}