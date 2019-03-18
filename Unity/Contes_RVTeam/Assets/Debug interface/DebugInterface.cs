using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInterface : MonoBehaviour {

    bool hidden;
    [SerializeField]
    Dropdown languagesDropdown;
    enum Language { French, English }

    [SerializeField]
    InputField COMPortField;

    [SerializeField]
    InputField lanternIndexField;

    public static string lanternKey = "lantern";
    public static string COMKey = "COM";

    private void Awake()
    {
        if (languagesDropdown)
            SetupLanguages();

        if (COMPortField)
            SetupCOMPort();

        if (lanternIndexField)
            SetupLanternIndex();

        // Hide the interface.
        gameObject.Hide();
        hidden = true;
    }

    void SetupLanguages()
    {
        // Populate languages dropdown.
        List<string> options = new List<string>();
        foreach (Language language in (Language[])Enum.GetValues(typeof(Language)))
            options.Add(language.ToString());
        languagesDropdown.AddOptions(options);
    }


    #region COM port -------------------
    void SetupCOMPort()
    {
        COMPortField.onEndEdit.AddListener(delegate { UpdateCOMPort(); });
    }
    void UpdateCOMPort()
    {
        if (COMPortField.text.Length == 1)
            PlayerPrefs.SetString(COMKey, COMPortField.text);
        else if (COMPortField.text.Length > 1)
            PlayerPrefs.SetString(COMKey, "\\\\.\\" + COMPortField.text);
    }
    #endregion -------------------


    #region Lantern index -------------
    void SetupLanternIndex()
    {
        if (PlayerPrefs.HasKey(lanternKey))
            lanternIndexField.text = PlayerPrefs.GetInt(lanternKey).ToString();
        lanternIndexField.onEndEdit.AddListener(delegate { UpdateLanternIndex(); });
    }
    void UpdateLanternIndex()
    {
        int number;
        if (!int.TryParse(lanternIndexField.text, out number))
            return;
        PlayerPrefs.SetInt(lanternKey, int.Parse(lanternIndexField.text));
        if (Lanterne.instance)
            Lanterne.instance.UpdateTrackerIndex();
    }
    #endregion --------------------------


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hidden = !hidden;
            if (hidden)
                gameObject.Hide();
            else
                gameObject.Show();
        }
    }
}
