using System.Collections.Generic;
using TMPro;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown localeDropdown;
    [SerializeField] private TMP_Text rebindButtonText;
    //[SerializeField] private InputActionReference rollInputActionRef;
    [SerializeField] private PlayerInput playerInput;
    
    private static readonly Dictionary<string, string> localeNames = new()
    {
        { "English (en)", "English" },
        { "Russian (ru)", "Русский" }
    };
    
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private List<Locale> locales;
    private InputAction rollInputAction;

    private void Awake()
    {
        localeDropdown.ClearOptions();
        locales = LocalizationSettings.AvailableLocales.Locales;
        foreach (var locale in locales)
        {
            var name = locale.LocaleName;
            localeDropdown.options.Add(new TMP_Dropdown.OptionData { text = localeNames[name] });
        }
        localeDropdown.onValueChanged.AddListener(ChangeLocalization);
        
        //I tried using InputActionReference but for some reason it wouldn't update an actual binding
        rollInputAction = playerInput.actions["Dice/Roll"];
        rebindButtonText.text = rollInputAction.GetBindingDisplayString();
    }

    public void RebindRoll()
    {
        rollInputAction.Disable();
        rebindingOperation = rollInputAction.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/Escape")
            .OnMatchWaitForAnother(0.2f)
            .OnCancel(op =>
            {
                rebindButtonText.text = rollInputAction.GetBindingDisplayString();
                rollInputAction.Enable();
                op.Dispose();
            })
            .OnComplete(op =>
            {
                rebindButtonText.text = rollInputAction.GetBindingDisplayString();
                rollInputAction.Enable();
                op.Dispose();
            })
            .Start();
        
        rebindButtonText.text = "<...>";
    }

    private void ChangeLocalization(int localizationId)
    {
        //LocalizationSettings.InitializationOperation.WaitForCompletion();
        LocalizationSettings.SelectedLocale = locales[localizationId];
    }
}