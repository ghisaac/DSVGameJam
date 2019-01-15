using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_GameManager : MonoBehaviour
    {
        public static FIL_GameManager instance;

        [SerializeField]
        private int[] _pointsPerPlacement;

        [SerializeField]
        private List <GameObject> _placement = new List<GameObject>();

        [SerializeField]
        private GameObject[] _players;

        [SerializeField]
        private Transform[] _spawns;

        [SerializeField]
        private float _drownSpeed = 0.005f;

        [SerializeField]
        private GameObject tables;

        [SerializeField] private float _tableDrownDelay = 2f;
        private List<GameObject> _tablesList;

        private WaitForSeconds _waitForSeconds;
        [SerializeField]
        private float _gameStartDelay = 2f;
        [SerializeField]
        private FIL_UI _uI;


        private void Awake()
        {
            instance = this;
        }


        private void Start()
        {
            for(int i = 0; i < _players.Length; i++)
            {
                _players[i].transform.position = _spawns[i].position;
            }

            _tablesList = new List<GameObject>();
            _waitForSeconds =  new WaitForSeconds(_tableDrownDelay);
            for (int i = 0; i < tables.transform.childCount; i++)
            {
                _tablesList.Add(tables.transform.GetChild(i).gameObject);
            }

            //kolla hur många PlayerControllers som finns / antalet spelare.
            //anpassa UI efter antalet spelare
            //tilldela varje spelare en karaktär.

            //Spelarna kan inte röra sig

            _uI.StartTimer();
        }


        public void StartGameLoop()
        {
            //Spelarna kan röra sig
            StartCoroutine("DrownTable");
        }


        private void Update()
        {
            if(_placement.Count == _players.Length - 1)
            {
                EndGame();
            }
        }


        private IEnumerator DrownTable()
        {
            if (_tablesList.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, _tablesList.Count - 1);
                GameObject table = _tablesList[random];
                _tablesList.Remove(table);

                table.GetComponent<FIL_TableShake>().ShakeTable();
                //spela bubbeleffekter och rök runt det valda bordet här

                yield return _waitForSeconds;
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    Vector3 newPos = table.transform.position - new Vector3(0, _drownSpeed * Time.deltaTime, 0);
                    table.transform.position = newPos;

                    if (table.transform.position.y < -1f)
                    {
                        break;
                    }
                }

                StartCoroutine("DrownTable");
            }

            else
            {
                yield return new WaitForEndOfFrame();
                Invoke("EndGame", 2f);
            }
        }


        private void EndGame()
        {
            //spelare kan inte längre röra sig
            StopAllCoroutines();
            AddLastPlayer();
            AwardPoints();
        }


        private void AddLastPlayer()
        {
            foreach (GameObject player in _players)
            {
                if (!_placement.Contains(player))
                {
                    _placement.Add(player);
                }
            }
        }


        private void AwardPoints()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                //_placement[i].DistributePoints(_pointsPerPlacements[i]);
            }
        }


        public void PlayerDeath(GameObject player)
        {
            _placement.Add(player);
        }


    }
}