using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This class connects the UI input to the ErrorValidator
// It also acts as the entry point for teammates to send measurements
public class MeasurementBridge : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ErrorValidator errorValidator;
    [SerializeField] private TMP_InputField inputField_Measurement;
    [SerializeField] private Button button_Validate;

    private void Start()
    {
        // Wire up the button click
        button_Validate.onClick.AddListener(OnValidateButtonClicked);
    }

    // Called when UI button is pressed (for your own testing)
    private void OnValidateButtonClicked()
    {
        if (float.TryParse(inputField_Measurement.text, out float value))
        {
            errorValidator.ReceiveMeasurement(value);
        }
        else
        {
            Debug.LogWarning("MeasurementBridge: Invalid input. Please enter a number.");
        }
    }

    // Called by teammates' scripts to send a measurement programmatically
    public void SendMeasurement(float value)
    {
        errorValidator.ReceiveMeasurement(value);
    }
}