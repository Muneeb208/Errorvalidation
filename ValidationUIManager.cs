using UnityEngine;
using TMPro;

// Single Responsibility: ONLY handles displaying results on screen
// It knows nothing about how validation works — just how to show it
public class ValidationUIManager : MonoBehaviour
{
    [Header("Result Panel Texts")]
    [SerializeField] private TextMeshProUGUI text_Status;
    [SerializeField] private TextMeshProUGUI text_MeasuredValue;
    [SerializeField] private TextMeshProUGUI text_AbsoluteError;
    [SerializeField] private TextMeshProUGUI text_PercentageError;
    [SerializeField] private TextMeshProUGUI text_Message;

    [Header("Dependencies")]
    [SerializeField] private ErrorValidator errorValidator;

    private void OnEnable()
    {
        // Subscribe to the validator's event
        if (errorValidator != null)
            errorValidator.OnValidationComplete += DisplayResult;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        if (errorValidator != null)
            errorValidator.OnValidationComplete -= DisplayResult;
    }

    public void DisplayResult(ValidationResult result)
    {
        text_MeasuredValue.text  = $"Measured: {result.MeasuredValue:F3} mm";
        text_AbsoluteError.text  = $"Absolute Error: {result.AbsoluteError:F3} mm";
        text_PercentageError.text = $"Error %: {result.PercentageError:F2}%";
        text_Message.text        = result.Message;

        // Change color based on status
        switch (result.Status)
        {
            case ValidationStatus.Acceptable:
                text_Status.text  = "✔ ACCEPTABLE";
                text_Status.color = Color.green;
                break;
            case ValidationStatus.Warning:
                text_Status.text  = "⚠ WARNING";
                text_Status.color = Color.yellow;
                break;
            case ValidationStatus.Failed:
                text_Status.text  = "✘ FAILED";
                text_Status.color = Color.red;
                break;
        }
    }
}