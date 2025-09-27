using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField]
    private Dice dicePrefab;
    [SerializeField] [Range(1, 10)] private int diceCount;
    [SerializeField] [Range(2f, 5f)] private float diceDispersion;
    [SerializeField] private Vector3 startPosition;
    
    private List<Dice> dices = new List<Dice>();
    private DiceRoller roller;

    private void Awake()
    {
        roller = GetComponent<DiceRoller>();
        RespawnDices();
    }

    private void RespawnDices()
    {
        foreach (var dice in dices)
        {
            Destroy(dice);
        }
        dices.Clear();
        
        for (int i = 0; i < diceCount; i++)
        {
            var displace = Random.insideUnitSphere * diceDispersion;
            dices.Add(Instantiate(dicePrefab, startPosition + displace, Quaternion.identity));
        }
     
        roller.SetDices(dices);
    }
}
