using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FIL
{
    public class FIL_GameManager : MonoBehaviour
    {
        public static FIL_GameManager instance;

        [SerializeField]
        private int[] _pointsPerPlacement;
        [SerializeField]
        private List <Player> _placement = new List<Player>();
        [SerializeField]
        private GameObject[] _players;
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
        public GameObject _smokePrefab;
        private WaitForSeconds _waitForSeconds;

        public GameObject playerParent;
        public GameObject playerUIParent;

        public bool gameStarted = false;

        private void Awake()
        {
            instance = this;
        }

        //Start sätter variabler som behövs och hittar relevanta players från Player-classen. Startar en countdown och musik.
        private void Start()
        {
            _players = new GameObject[Player.AllPlayers.Count];

            for (int i = 0; i < Player.AllPlayers.Count; i++)
            {
                Player player = Player.AllPlayers[i];
                FIL_PlayerController controller = playerParent.transform.GetChild(player.RewierdId).gameObject.GetComponent<FIL_PlayerController>();
                controller.myPlayer = player;
                controller.gameObject.SetActive(true);
                _players[i] = controller.gameObject;
                playerUIParent.transform.GetChild(player.RewierdId).gameObject.SetActive(true);
            }
 
            _tablesList = new List<GameObject>();
            _waitForSeconds =  new WaitForSeconds(_tableDrownDelay);

            for (int i = 0; i < tables.transform.childCount; i++)
            {
                _tablesList.Add(tables.transform.GetChild(i).gameObject);
            }

            _uI.StartTimer();

            SoundManager.Instance.PlayLavaLoop();
            SoundManager.Instance.PlayBGM();
        }


        public void StartGameLoop()
        {
            gameStarted = true;
            StartCoroutine("DrownTable");
        }

        //kollar hur många spelare som är kvar i spelet och avslutar om det bara är en kvar.
        private void Update()
        {
            if(_placement.Count == _players.Length - 1)
            {
                EndGame();
            }
        }

        //slumpar fram ett bord ur en lista och startar en metod på det som får det att skaka och sedan sjunka ner i lavan.
        private IEnumerator DrownTable()
        {
            if (_tablesList.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, _tablesList.Count - 1);
                GameObject table = _tablesList[random];
                _tablesList.Remove(table);
                table.GetComponent<FIL_TableShake>().DrownTable(2f);
                yield return _waitForSeconds;
                StartCoroutine("DrownTable");
            }
            else
            {
                yield return new WaitForEndOfFrame();
                Invoke("EndGame", 2f);
            }
        }

        //Avslutar spelet, ger ut poäng och laddar nästa scen.
        private void EndGame()
        {
            StopAllCoroutines();
            AddLastPlayer();
            FlipPlacementList();
            AwardPoints();

            SceneManager.LoadScene("ScoreScreenScene");
        }

        //vänder på listan över spelarnas placering för att den ska fungera med Player.DistributePoints().
        private void FlipPlacementList()
        {
            _placement.Reverse();
        }

        //lägger till den överlevande spelarna i placement listan.
        private void AddLastPlayer()
        {
            foreach (GameObject player in _players)
            {
                Player myPlayer = player.GetComponent<FIL_PlayerController>().myPlayer;
                if (!_placement.Contains(myPlayer))
                {
                    _placement.Add(myPlayer);
                }
            }
        }
        
        private void AwardPoints()
        {

            Debug.Log("GameEnded");

            Player[] placementArray = _placement.ToArray();

            Player.DistributePoints(placementArray);
        }

        //lägger till en död spelare till en lista (_placement). Index 0 är den som dör först, sista index den som vinner.
        public void PlayerDeath(GameObject player)
        {
            player.SetActive(false);
            Player myPlayer = player.GetComponent<FIL_PlayerController>().myPlayer;
            _placement.Add(myPlayer);

            if (_placement.Count == _players.Length - 1)
            {
                EndGame();
            }
        }

        //tar bort et bord från listan över bord så att spelet inte försöker skaka och sänka ner det i lavan flera gånger.
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