using UnityEngine;

public class RulesHistoryAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ToggleRules()
    {
        animator.SetTrigger("RulesToggle");
    }

    public void ToggleHistory()
    {
        animator.SetTrigger("HistoryToggle");
    }
}
