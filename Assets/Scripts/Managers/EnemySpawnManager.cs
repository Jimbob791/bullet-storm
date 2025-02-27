using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.InputSystem;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();
    public int wave;
    public TextMeshProUGUI waveText;
    public GameObject winWindow;
    public GameObject loseWindow;

    public float credits;
    public float creditsPerSecond;

    private float timeElapsed;

    bool complete = false;

    [Header("Effects")]
    [SerializeField] GameObject spawnEffect;

    private void Start()
    {
        StartCoroutine(SpawnWave());
        waveText.text = "WAVE " + wave + " /10";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); }

        if (HelperFunctions.tutorial)
        {
            if (complete && InputSystem.actions.FindAction("TimeSlow").WasPressedThisFrame())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }

        timeElapsed += Time.deltaTime;

        creditsPerSecond = ((timeElapsed / 60) * 0.1f + 1) * Mathf.Pow(1.15f, timeElapsed / 30f);

        credits += creditsPerSecond * Time.deltaTime;
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(Random.Range(10f, 20f));

        SpawnEnemies();

        wave += 1;
        waveText.text = "WAVE " + wave + " /10";

        if (wave == 11)
        {
            Win();
        }

        StartCoroutine(SpawnWave());
    }

    public void Win()
    {
        winWindow.SetActive(true);
        Time.timeScale = 0;
        HelperFunctions.tutorial = true;
        complete = true;
    }

    public void Lost()
    {
        loseWindow.SetActive(true);
        Time.timeScale = 0;
        HelperFunctions.tutorial = true;
        complete = true;
    }

    private void SpawnEnemies()
    {
        int maxEnemies = 6;
        for (int m = 0; m < maxEnemies; m++)
        {
            // Find affordable enemies
            List<EnemyInfo> affordableEnemies = new List<EnemyInfo>();
            foreach (EnemyInfo enemy in enemies)
            {
                if (enemy.cost <= credits) { affordableEnemies.Add(enemy); }
            }

            if (affordableEnemies.Count == 0) { Debug.Log("No affordable enemies");  return; }

            // Get total weight
            float totalWeight = affordableEnemies.Sum(k => k.weight);

            // Get random weight
            float selectedWeight = Random.Range(0.0f, totalWeight);
            Debug.Log(totalWeight + " " + selectedWeight);

            // Find enemy for that weight
            foreach (EnemyInfo enemy in affordableEnemies)
            {
                selectedWeight -= enemy.weight;
                if (selectedWeight <= 0f)
                {
                    // Spawn Enemy
                    StartCoroutine(SpawnEnemy(enemy));
                    break;
                }
            }
        }
    }

    private IEnumerator SpawnEnemy(EnemyInfo enemy)
    {
        credits -= enemy.cost;

        Vector2 spawnPos = new Vector2(Random.Range(-18f, 18f), Random.Range(-8f, 8f));

        Instantiate(spawnEffect, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(10f/12f);

        Instantiate(enemy.prefab, spawnPos, Quaternion.identity);
    }
}

[System.Serializable]
public struct EnemyInfo
{
    public GameObject prefab;
    public float cost;
    public float minTime;
    public float weight;
}