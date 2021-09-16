using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] ZombieModel[] _models;
    [SerializeField] GameObject _zombiePrefab;

    [SerializeField] private float _spawnInterval;

    [SerializeField] private Transform[] _spawnPoints;

    private Coroutine _spawnCoroutine;
    private Coroutine _speedUpCoroutine;

    [SerializeField] private float _speedUpInterval;
    [SerializeField] private float _speedDecreaseValue;
    [SerializeField] private float _minSpeed;

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            SpawnZombie();

            yield return null;
        }
    }

    private IEnumerator ProcessSpawnTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_speedUpInterval);

            if (_spawnInterval <= _minSpeed)
            {
                yield break;
            }

            _spawnInterval -= _speedDecreaseValue;

            yield return null;
        }
    }

    public void Enable()
    {
        _spawnCoroutine = StartCoroutine(StartSpawning());
        _speedUpCoroutine = StartCoroutine(ProcessSpawnTime());
    }

    public void Disable()
    {
        StopCoroutine(_spawnCoroutine);
        StopCoroutine(_speedUpCoroutine);
    }

    private void SpawnZombieInternal(ZombieModel model, Transform spawnPoint)
    {
        ZombieController zombie = Instantiate(_zombiePrefab).GetComponent<ZombieController>();
        zombie.Init(model);
        zombie.transform.position = spawnPoint.position;
        zombie.transform.rotation = spawnPoint.rotation;
    }

    private void SpawnZombie()
    {
        int modelID = Random.Range(0, _models.Length);
        int spawnID = Random.Range(0, _spawnPoints.Length);

        SpawnZombieInternal(_models[modelID], _spawnPoints[spawnID]);
    }
}