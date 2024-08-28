using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

[SelectionBase]
public class ResourceBarTracker : MonoBehaviour
{
    [Header("Core Settings")]
    public Image bar;
    public ResourceType resourceType;
    [Space]
    public ShapeType shapeOfBar;

    public enum ShapeType
    {
        [InspectorName("Rectangle (Horizontal)")]
        RectangleHorizontal,
        [InspectorName("Rectangle (Vertical)")]
        RectangleVertical,
        [InspectorName("Circle")]
        Circle,
        Arc
    }

    public enum ResourceType
    {
        Air,
        Water,
        Currency
    }

    [Header("Arc Settings")]
    [SerializeField, Range(0, 360)] private int endDegreeValue = 360;

    [Header("Animation Speed")]
    [SerializeField, Range(0, 0.5f)] private float animationTime = 0.25f;
    private Coroutine fillRoutine;

    [Header("Text Settings")]
    [SerializeField] private DisplayType howToDisplayValueText = DisplayType.Percentage;
    [SerializeField] private TMP_Text resourceValueTextField;

    public enum DisplayType
    {
        [InspectorName("Long (50|100)")]
        LongValue,
        [InspectorName("Short (50)")]
        ShortValue,
        [InspectorName("Percent (85%)")]
        Percentage,
        None
    }

    [Header("Gradient Settings")]
    [SerializeField] private bool useGradient;
    [SerializeField] private Gradient barGradient;

    [Header("Events")]
    [SerializeField] private UnityEvent barIsFilledUp;
    private float previousFillAmount;

    [Header("Test mode")]
    [SerializeField] private bool enableTesting;

    private ResourceManager resourceManager;

    private void OnValidate()
    {
        ConfigureBarShapeAndProperties();
    }

    private void Start()
    {
        resourceManager = ResourceManager.instance;
        ConfigureBarShapeAndProperties();
        StartCoroutine(UpdateResourceBar());
    }

    private IEnumerator UpdateResourceBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f); // Adjust the interval as needed
            UpdateBarAndResourceText();
        }
    }

    private void ConfigureBarShapeAndProperties()
    {
        switch (shapeOfBar)
        {
            case ShapeType.RectangleHorizontal:
                bar.fillMethod = Image.FillMethod.Horizontal;
                break;
            case ShapeType.RectangleVertical:
                bar.fillMethod = Image.FillMethod.Vertical;
                break;
            case ShapeType.Circle:
            case ShapeType.Arc:
                bar.fillMethod = Image.FillMethod.Radial360;
                break;
        }

        if (!useGradient)
            bar.color = Color.white;

        UpdateBarAndResourceText();
    }

    private void UpdateBarAndResourceText()
    {
        float resourceCurrent = GetResourceCurrent();
        float resourceMax = 100; // Assuming max is 100 for simplicity

        float fillAmount;

        if (shapeOfBar == ShapeType.Arc)
            fillAmount = CalculateCircularFillAmount(resourceCurrent, resourceMax);
        else
            fillAmount = resourceCurrent / resourceMax;

        if (Mathf.Approximately(bar.fillAmount, fillAmount))
            return;

        if (fillRoutine != null)
            StopCoroutine(fillRoutine);

        fillRoutine = StartCoroutine(SmoothlyTransitionToNewValue(fillAmount));
        SetCurrentResourceValueText(resourceCurrent, resourceMax);
    }

    private float CalculateCircularFillAmount(float resourceCurrent, float resourceMax)
    {
        float fraction = resourceCurrent / resourceMax;
        float fillRange = endDegreeValue / 360f;
        return fillRange * fraction;
    }

    private void SetCurrentResourceValueText(float resourceCurrent, float resourceMax)
    {
        switch (howToDisplayValueText)
        {
            case DisplayType.LongValue:
                resourceValueTextField.SetText($"{resourceCurrent}/{resourceMax}");
                break;
            case DisplayType.ShortValue:
                resourceValueTextField.SetText($"{resourceCurrent}");
                break;
            case DisplayType.Percentage:
                float percentage = (resourceCurrent / resourceMax) * 100;
                resourceValueTextField.SetText($"{Mathf.RoundToInt(percentage)} %");
                break;
            case DisplayType.None:
                resourceValueTextField.SetText(string.Empty);
                break;
        }
    }

    private float GetResourceCurrent()
    {
        if(resourceManager == null) { return 0; }
        switch (resourceType)
        {
            case ResourceType.Air:
                return resourceManager.airLevels;
            case ResourceType.Water:
                return resourceManager.waterLevels;
            case ResourceType.Currency:
                return resourceManager.currencyLevels;
            default:
                return 0;
        }
    }

    private IEnumerator SmoothlyTransitionToNewValue(float targetFill)
    {
        float originalFill = bar.fillAmount;
        float elapsedTime = 0.0f;

        while (elapsedTime < animationTime)
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / animationTime;
            bar.fillAmount = Mathf.Lerp(originalFill, targetFill, time);

            UseGradient();

            yield return null;
        }

        bar.fillAmount = targetFill;

        HandleEvent();
        previousFillAmount = bar.fillAmount;
    }

    private void UseGradient()
    {
        if (!useGradient)
            return;

        if (shapeOfBar == ShapeType.Arc)
        {
            float fillRange = bar.fillAmount / (endDegreeValue / 360f);
            bar.color = barGradient.Evaluate(fillRange);
            return;
        }

        bar.color = barGradient.Evaluate(bar.fillAmount);
    }

    private void HandleEvent()
    {
        if (previousFillAmount >= 1)
            return;

        if (bar.fillAmount >= 1)
            barIsFilledUp?.Invoke();
    }
}
