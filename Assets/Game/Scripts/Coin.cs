using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class Coin : MonoBehaviour
{
    [Header("UI reference")]
    [SerializeField] TMP_Text txtScore;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header("Available coins: (coin to pool)")]
    [SerializeField] int maxCoin;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation setting")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2.0f)] float maxAnimDuration;
    [SerializeField] Ease easeType;
    [SerializeField] float spread;


    private int _coin;
    public int coins
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            UpdateScore();
        }
    }
    private void UpdateScore()
    {
        txtScore.text = _coin.ToString();
    }
    private void Awake()
    {
        PrepareCoins();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            coins++;
            Animate(transform.position, 10);
        }
    }
    void PrepareCoins()
    {
        for (int i = 0; i < maxCoin; i++)
        {
            GameObject coin;
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }
    void Animate(Vector3 collectedCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // check if there's coin from the pool
            if (coinsQueue.Count > 0)
            {
                // extract a coin from pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                // move coin to the collected coin position
                coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0, Random.Range(-spread, spread));

                // animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(target.transform.position, duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        // excutes whenever coin reach target position
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);
                        coins++;
                    });
            }
        }
    }
}
