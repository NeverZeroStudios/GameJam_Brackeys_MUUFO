using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace NeverZeroStudios
{
    public class GameManager : MonoBehaviour
    {
       
        public Cow _cow;
        public Planet planet;
        float delta;

       
        #region WaveSpawner

        [System.Serializable]
        public class Wave
        {
            public string name;
            public UFOController ufo;
            public int count;
            public float rate;
        }

        [Header("Waves :")]
        public Wave[] waves;
        int nextWave = 0;
        public float timeBetweenWaves = 5f;
        public float waveCountdown = 0;
        float searchCountdown = 1f;
        SpawnState state = SpawnState.Counting;
        void StartWave()
        {
            waveCountdown = timeBetweenWaves;

        }
      

        public bool enemyStillAlive;

        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if(searchCountdown <= 0)
            {
                searchCountdown = 1;
                if(GameObject.FindGameObjectWithTag("UFO") == null)
                {
                    return false;
                }
                
            }
            return true;
        }

        void UpdateWaves(float d)
        {
            enemyStillAlive = EnemyIsAlive();
            if(state == SpawnState.Waiting)
            {
                if (!EnemyIsAlive())
                {
                  
                    WaveCompleted();
                    

                }
                else
                {
                    return;
                }
            }
            if (waveCountdown <= 0)
            {
                if (state != SpawnState.Spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= d;
            }
        }

        void WaveCompleted()
        {
           
            state = SpawnState.Counting;
            waveCountdown = timeBetweenWaves;
            if(nextWave + 1 > waves.Length - 1)
            {
                nextWave = 0;
                
            }
            nextWave++;

        }

        IEnumerator SpawnWave(Wave _wave)
        {

            state = SpawnState.Spawning;

           
            for (int i = 0; i < _wave.count; i++)
            {
                SpawnUFO(_wave.ufo);
                yield return new WaitForSeconds(1f / _wave.rate);
            }

            state = SpawnState.Waiting;
            yield break;
        }

        void SpawnUFO(UFOController _ufo)
        {

            Instantiate(_ufo);
            int rand = Random.Range(0, planet.spawnPoints.Length - 1);
            _ufo.transform.position = planet.spawnPoints[rand].position;
            _ufo.transform.localScale = Vector3.one;

             
        }

        public enum SpawnState
        {
            Spawning,
            Waiting,
            Counting
        }

        #endregion


        [Header("Points : ")]
        public int points;
        int curPoints;
        public int pointsMultiplier = 1;
        public GameObject pointsObj;
        public Text pointsText;
        public Text finalPointsText;
        
        public void ResetLevel()
        {
            SceneManager.LoadScene("Level 01");
           
        }

        void HandlePoints()
        {
            pointsText.enabled = true;
            curPoints = points;
            pointsText.text = points.ToString();

            if (!_cow.isAlive)
                return;

            points += Mathf.RoundToInt((50 * pointsMultiplier) * Time.deltaTime);
        }
        public bool isGameOver;
       
        public void GameOver(GameObject mesh, Rigidbody rigi)
        {
            mesh.gameObject.SetActive(false);
            mesh.gameObject.GetComponentInParent<Collider>().enabled = false;
            rigi.isKinematic = true;

            isGameOver = true;
        }
        int finalPoints;

        private void Start()
        {
            pointsObj.SetActive(false);
            pointsText.enabled = false;
            _cow.Init();
            StartWave();
        }

        

        private void Update()
        {

            if (isGameOver)
            {
                finalPoints = curPoints;
                pointsObj.SetActive(true);
                pointsText.enabled = false;

                finalPointsText.text = "GAME OVER! " + "final Points : " + finalPoints.ToString();

            }
            delta = Time.deltaTime;
            _cow.Tick(delta);

            HandlePoints();

            UpdateWaves(delta);
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            _cow.FixedTick(delta);
        }

    }
}
