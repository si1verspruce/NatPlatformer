using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    [SerializeField] private GameObject _tilemap;
    [SerializeField] private GameObject[] _levelElements;
    [SerializeField] private Transform _playerSpawnPoint;

    private void OnEnable()
    {
        if (_tilemap)
            _tilemap.SetActive(true);

        foreach (GameObject levelElement in _levelElements)
            if (levelElement)
                levelElement.SetActive(true);
    }

    private void OnDisable()
    {
        if (_tilemap)
            _tilemap.SetActive(false);

        foreach (GameObject levelElement in _levelElements)
            if (levelElement)
                levelElement.SetActive(false);
    }

    public void Launch()
    {
        gameObject.SetActive(true);
        _tilemap.SetActive(true);
    }

    public void Exit()
    {
        _tilemap.SetActive(false);
        gameObject.SetActive(false);
    }
}
