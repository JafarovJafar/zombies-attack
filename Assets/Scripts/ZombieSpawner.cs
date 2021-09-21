using System.Collections;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    #region Vars
    public Transform[] SpawnPoints => _spawnPoints;

    private Coroutine _spawnCoroutine;
    private Coroutine _speedUpCoroutine;

    [SerializeField] ZombieModel[] _models;
    [SerializeField] GameObject _zombiePrefab;

    [SerializeField] private ObjectPool _zombiesPool;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _speedUpInterval;
    [SerializeField] private float _speedDecreaseValue;
    [SerializeField] private float _minSpeed;

    [SerializeField] private float[] _spawnChances;
    private float[] _spawnChancesStarts;
    #endregion

    #region Methods
    public void Enable()
    {
        if (_spawnChances == null || _spawnChances.Length != _models.Length)
        {
            Debug.LogError("Ќеправильные веро€тности у спавнера!!!");
        }

        /// нормализаци€
        float chancesSum = _spawnChances.Sum();

        if (chancesSum != 100)
        {
            float multiplier = 100f / chancesSum;

            for (int i = 0; i < _spawnChances.Length; i++)
            {
                _spawnChances[i] *= multiplier;
            }
        }

        _spawnChancesStarts = new float[_spawnChances.Length];
        _spawnChancesStarts[0] = 0;

        for (int i = 1; i < _spawnChances.Length; i++)
        {
            _spawnChancesStarts[i] = _spawnChances[i - 1] + _spawnChancesStarts[i - 1];
        }

        _spawnCoroutine = StartCoroutine(StartSpawning());
        _speedUpCoroutine = StartCoroutine(ProcessSpawnTime());
    }

    public void Disable()
    {
        StopCoroutine(_spawnCoroutine);
        StopCoroutine(_speedUpCoroutine);
    }

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

    private void SpawnZombie()
    {
        int modelID = 0;

        int tempInt = Random.Range(0, 100);
        for (int i = _spawnChancesStarts.Length - 1; i >= 0; i--)
        {
            if (tempInt >= _spawnChancesStarts[i])
            {
                modelID = i;
                break;
            }
        }

        int spawnID = Random.Range(0, _spawnPoints.Length);

        GameObject zombieGO = _zombiesPool.GetItem();

        ZombieController zombie = zombieGO.GetComponent<ZombieController>();
        zombie.Init(_models[modelID]);
        zombie.CopyTransform(_spawnPoints[spawnID]);
    }
    #endregion
}