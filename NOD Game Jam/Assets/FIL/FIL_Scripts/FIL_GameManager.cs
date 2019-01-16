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
        [SerializeField] 
        private List<GameObject> _tablesList;
        [SerializeField]
        private float _gameStartDelay = 2f;

        [SerializeField]
        private FIL_UI _uI;
        [SerializeField]
        private GameObject playerPrefab;
        public GameObject _lavaBubblePrefab;
        [SerializeField]
        private GameObject _smokePrefab;
        private WaitForSeconds _waitForSeconds;

        public bool gameStarted = false;


        private void Awake()
        {
            instance = this;
        }


        private void Start()
        {
            _players = new GameObject[Player.AllPlayers.Count];

            for (int i = 0; i < Player.AllPlayers.Count; i++)
            {
                GameObject player = Instantiate(playerPrefab);
                _players[i] = player;

                _players[i].GetComponent<PlayerController>().myPlayer = Player.AllPlayers[i];
                _players[i].transform.position = _spawns[i].position;
                //Sätt rätt färg på spelare / i UIt beroende på rewired ID
            }
 
            _tablesList = new List<GameObject>();
            _waitForSeconds =  new WaitForSeconds(_tableDrownDelay);

            for (int i = 0; i < tables.transform.childCount; i++)
            {
                _tablesList.Add(tables.transform.GetChild(i).gameObject);
            }

            _uI.StartTimer();
        }


        public void StartGameLoop()
        {
            gameStarted = true;
            StartCoroutine("DrownTable");
        }


        private void Update()
        {
            if(_placement.Count == _players.Length - 1)
            {
                EndGame();
            }

            if (!gameStarted)
            {
                for (int i = 0; i < _players.Length; i++)
                {
                    _players[i].transform.position = _spawns[i].position;
                }
            }
        }


        private IEnumerator DrownTable()
        {
            if (_tablesList.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, _tablesList.Count - 1);
                GameObject table = _tablesList[random];
                _tablesList.Remove(table);
                Vector3 tablePos = table.transform.position;
                table.GetComponent<FIL_TableShake>().ShakeTable();
                
                yield return _waitForSeconds;
                Instantiate(_smokePrefab, tablePos, Quaternion.Euler(-90, 0, 0));

                GameObject bubble = Instantiate(_lavaBubblePrefab, tablePos, Quaternion.identity);
                bubble.transform.localScale *= 2;
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    Vector3 newPos = table.transform.position - new Vector3(0, _drownSpeed * Time.deltaTime, 0);
                    table.transform.position = newPos;

                    if (table.transform.position.y < -2f)
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
            StopAllCoroutines();
            AddLastPlayer();
            FlipPlacementList();
            AwardPoints();
        }

        private void FlipPlacementList()
        {
            _placement.Reverse();
        }

        private void AddLastPlayer()
        {
            foreach (GameObject player in _players)
            {
                if (!_placement.Contains(player))
                {
                    //add myPlayer, not the gameobject to placement list 
                    _placement.Add(player);
                }
            }
        }

        private void AwardPoints()
        {

            Debug.Log("GameEnded");

            //Player.DistributePoints(_placements);
            //load scoreboardscene

            for (int i = 0; i < _players.Length; i++)
            {
                //_placement[i].DistributePoints(_pointsPerPlacements[i]);
            }
        }

        public void PlayerDeath(GameObject player)
        {
            player.SetActive(false);
            _placement.Add(player);
           
            if (_placement.Count == _players.Length - 1)
            {
                EndGame();
            }
        }

        public void RemoveTableFromList(GameObject table)
        {
            _tablesList.Remove(table);
        }

        public void RespawnPlayer(FIL_PlayerController player)
        {
            Debug.Log("Respawning " + player.gameObject.name);
            int random = UnityEngine.Random.Range(0, _tablesList.Count - 1);
            GameObject table = _tablesList[random];
            player.gameObject.transform.position = table.transform.position + (Vector3.up * 4);
        }

    }
}