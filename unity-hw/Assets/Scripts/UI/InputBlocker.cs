using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//Some unholy things are happening here
//Actually this class is needed so input actions won't be triggerred when typing in inputFields
[RequireComponent(typeof(TMP_InputField))]
public class InputBlocker : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private TMP_InputField inputField;
    
    public void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(BlockInput);
        inputField.onDeselect.AddListener(UnblockInput);
    }

    //Note: we're not using the string in inputField here, but it's required by event
    public void BlockInput(string unused)
    {
        playerInput.DeactivateInput();
    }

    public void UnblockInput(string unused)
    {
        playerInput.ActivateInput();
    }
}
