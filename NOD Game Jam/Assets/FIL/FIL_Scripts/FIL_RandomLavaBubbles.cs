using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIL_RandomLavaBubbles : MonoBehaviour
{

    [SerializeField] private BoxCollider _lavaCollider;
    [SerializeField] private GameObject[] _lavaParticlePrefabs;
    [SerializeField] private float _minRandom = 0f;
    [SerializeField] private float _maxRandom = 5f;
    [SerializeField] private float _randomNumber;
    [SerializeField] private Vector3 _randomPos;
    private float _timer = 0;

    private void Start()
    {
        _randomNumber = Random.Range(_minRandom, _maxRandom);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _randomNumber)
        {
            _randomNumber = Random.Range(_minRandom, _maxRandom);
            _timer = 0f;
            float _randomXPos = Random.Range(0, _lavaCollider.size.x) - (_lavaCollider.size.x / 2);
            float _randomYPos = Random.Range(0, _lavaCollider.size.y) - (_lavaCollider.size.y / 2);
            _randomPos = new Vector3(_randomXPos, 1.2f, _randomYPos);
            int randomPrefab = Random.Range(0, _lavaParticlePrefabs.Length - 1);
            Instantiate(_lavaParticlePrefabs[randomPrefab], _randomPos, Quaternion.identity);
        }
    }
}
