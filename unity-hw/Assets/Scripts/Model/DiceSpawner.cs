using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private Dice dicePrefab;
    [SerializeField] [Range(2f, 5f)] private float diceDispersion;
    [SerializeField] private Vector3 startPosition;

    public static DiceSpawner Instance;
    
    private List<Dice> dices = new List<Dice>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    //Using start to ensure that singleton is initialized
    private void Start()
    {
        GameState.Instance.diceCountChanged.AddListener(RespawnDices);
        //In case if binding happened after first event invoke
        RespawnDices(GameState.Instance.initialDiceCount);
    }

    private void RespawnDices(int diceCount)
    {
        //There could be pooling logic, but nah
        foreach (var dice in dices)
        {
            Destroy(dice.gameObject);
        }
        dices.Clear();
        
        for (int i = 0; i < diceCount; i++)
        {
            var displace = Random.insideUnitSphere * diceDispersion;
            dices.Add(Instantiate(dicePrefab, startPosition + displace, Quaternion.identity));
        }
     
        DiceRoller.Instance.SetDices(dices);
    }
}
