using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnVent[] vents;
    [SerializeField] private GameObject[] bugPrefabs;
    [SerializeField] private PlayerHealth player;

    [Header("Timing")]
    [SerializeField] private float warningTime = 1f;
    [SerializeField] private float startInterval = 3f;
    [SerializeField] private float minInterval = 0.6f;

    [Header("Difficulty ramp")]
    [SerializeField] private float rampDuration = 120f;   // seconds to reach max difficulty
    [SerializeField] private float maxSpeedMultiplier = 2.5f; // bug speed at max difficulty

    private float startTime;

    private void Start()
    {
        if (vents.Length == 0 || bugPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: assign Vents and Bug Prefabs.");
            return;
        }
        startTime = Time.time;
        StartCoroutine(SpawnLoop());
    }

    // 0 = start of the game, 1 = fully ramped up
    private float Difficulty() => Mathf.Clamp01((Time.time - startTime) / rampDuration);

    private IEnumerator SpawnLoop()
    {
        while (player == null || !player.IsDead)
        {
            float interval = Mathf.Lerp(startInterval, minInterval, Difficulty());
            yield return new WaitForSeconds(interval);

            SpawnVent vent = vents[Random.Range(0, vents.Length)];
            vent.SetWarning(true);
            yield return new WaitForSeconds(warningTime);
            vent.SetWarning(false);

            GameObject prefab = bugPrefabs[Random.Range(0, bugPrefabs.Length)];
            GameObject bug = Instantiate(prefab, vent.transform.position, Quaternion.identity);

            var scalable = bug.GetComponentInChildren<ISpeedScalable>();
            if (scalable != null)
                scalable.SetSpeedMultiplier(Mathf.Lerp(1f, maxSpeedMultiplier, Difficulty()));
        }
    }
}