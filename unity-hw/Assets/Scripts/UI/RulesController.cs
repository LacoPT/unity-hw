using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [SerializeField] private Slider diceCountSlider;
    [SerializeField] private TMP_InputField winConditionInput;
    [SerializeField] private TMP_InputField lossConditionInput;

    private void Start()
    {
        var gameState = GameState.Instance;
        diceCountSlider.minValue = GameState.MIN_DICE_COUNT;
        diceCountSlider.maxValue = GameState.MAX_DICE_COUNT;
        diceCountSlider.onValueChanged.AddListener((value) =>
        {
            var intValue =  (int)value;
            gameState.ChangeDiceCount(intValue);
        });
        gameState.diceCountChanged.AddListener((value) => diceCountSlider.value = value);
        
        //winConditionInput.text = gameState.InitialWinCondition.ToString();
        //lossConditionInput.text = gameState.InitialLossCondition.ToString();
        
        winConditionInput.onEndEdit.AddListener((str) => gameState.ChangeWinCondition(int.Parse(str)));
        lossConditionInput.onEndEdit.AddListener((str) => gameState.ChangeLossCondition(int.Parse(str)));
        //This could be useful if user entered invalid data (e.g. wic condition that is > diceCount * 6)
        gameState.winConditionChanged.AddListener((value) => winConditionInput.text = value.ToString());
        gameState.lossConditionChanged.AddListener((value) => lossConditionInput.text = value.ToString());
    }
}
