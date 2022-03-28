using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider _battlePalce;
    [SerializeField] private List<CharacterProperties> _enemyPrefabs;
    [SerializeField] private int _countAddEnemyInWawe;
    [SerializeField] private int _startEnemyCount;
    
    private List<CharacterProperties> _spanedEnemys = new List<CharacterProperties>();

    public event UnityAction EndedBattle;

    public void SpawnEnemy()
    {
        for (int i = 0; i < _startEnemyCount; i++)
        {
            AddNewEnemy();
        }
    }

    private void AddNewEnemy()
    {
        Vector3Int newEnemyPosition = PlacedPosition(_battlePalce.bounds.size);
        CharacterProperties enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)],newEnemyPosition,Quaternion.identity, transform);
        enemy.Dying += OnDying;
        _spanedEnemys.Add(enemy);
    }

    private void OnDying(CharacterProperties character)
    {
        _spanedEnemys.Remove(character);

        if (_spanedEnemys.Count <= 0)
        {
            _startEnemyCount += _countAddEnemyInWawe;
            EndedBattle?.Invoke();
        }
    }

    private Vector3Int PlacedPosition(Vector3 placeSize)
    {
        Vector3Int newPosition;
        bool positionClear;

        int xSize = (int)placeSize.x / 2;
        int zSize = (int)placeSize.z / 2;

        do
        {
            int x = Random.Range(-xSize, xSize + 1);
            int z = Random.Range(-zSize, zSize + 1);
            newPosition = new Vector3Int(x, 0, z);

            if (newPosition == Vector3Int.zero)
                positionClear = true;
            else
                positionClear = _spanedEnemys.Any(rezult => rezult.transform.position == newPosition);
        }
        while (positionClear);

        return newPosition;
    }
}
