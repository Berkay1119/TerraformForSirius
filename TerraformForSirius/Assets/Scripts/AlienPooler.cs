using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AlienPooler : MonoBehaviour
{
    [SerializeField] private GameObject alienPrefab;
    [SerializeField] private HexagonalGridGenerator hexagonalGridGenerator;
    [SerializeField] private int poolCount;
    private List<GameObject> alienList = new List<GameObject>();
    [SerializeField] private float offSetFromTile;
    [SerializeField] private float alienSpeed;
    [SerializeField] private float timeBetweenShips=0.3f;


    private void OnEnable()
    {
        EventManager.OnAlienInvasion+=SpawnAliens;
    }

    private void OnDisable()
    {
        EventManager.OnAlienInvasion-=SpawnAliens;
    }

    private void Awake()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject alien = Instantiate(alienPrefab,transform);
            alienList.Add(alien);
            alien.SetActive(false);
        }
    }


    private void SpawnAliens(AlienInfo alienInfo)
    {
        StartCoroutine(SpawnAlienRoutine(alienInfo));
    }

    private IEnumerator SpawnAlienRoutine(AlienInfo alienInfo)
    {
        List<HexagonalGrid> grids = hexagonalGridGenerator.GetEdgeTileList();
        for (int i = 0; i < alienInfo.damage; i++)
        {
            HexagonalGrid hexagonalGrid = grids[Random.Range(0, grids.Count)];
            
            if (hexagonalGrid.GetCoordinate().X==0)
            {
                alienList[i].transform.position = hexagonalGrid.transform.position-new Vector3(offSetFromTile,0,0);
            }
            else
            {
                alienList[i].transform.position = hexagonalGrid.transform.position+new Vector3(offSetFromTile,0,0);
            }
            alienList[i].SetActive(true);
            var i1 = i;
            alienList[i].transform.DOMove(hexagonalGrid.transform.position, alienSpeed).SetSpeedBased().OnComplete(() =>
            {
                alienList[i1].SetActive(false);
                EventManager.OnBarrierHasBeenDamaged(1);
            });
            yield return new WaitForSeconds(timeBetweenShips);
        }
    }
}
