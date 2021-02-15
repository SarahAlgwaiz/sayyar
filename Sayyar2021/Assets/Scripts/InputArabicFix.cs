using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ArabicSupport;
using TMPro;

public class InputArabicFix : MonoBehaviour
{
    // Use this for initialization
    public TMP_InputField textfield;
    public TextMeshProUGUI fakeDisplay;

    public bool arabic = true;
    void Start()
    {
        textfield = GetComponent<TMP_InputField>();
        textfield.textComponent.color = Color.black;
        fakeDisplay = (TextMeshProUGUI)GameObject.Instantiate(textfield.textComponent, textfield.textComponent.transform.localPosition, textfield.textComponent.transform.localRotation);
        fakeDisplay.transform.SetParent(textfield.transform, false);
        fakeDisplay.color = Color.white;
        textfield.onValueChanged.AddListener(delegate { FixNewText(); });
    }

    public string text
    {
        get
        {
            return textfield.text;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixNewText()
    {
        if (arabic)
        {
            fakeDisplay.text = ArabicFixer.Fix(textfield.text);
        }
        else
        {
            fakeDisplay.text = textfield.text;
        }
    }
}