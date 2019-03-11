using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenPrinter : ScriptableObject {

    static bool warningMade = false;
    static bool lastIsSlash = false;

    static TextMesh textDisplay;
    struct PendingMessage {
        public enum PrintStyle { Print, CleanPrint }
        public PrintStyle printStyle;
        public object message;
        public Object sender;
        public PendingMessage(object message, PrintStyle printStyle)
        {
            this.printStyle = printStyle;
            this.message = message;
            sender = null;
        }
        public PendingMessage(object message, PrintStyle printStyle, Object sender)
        {
            this.printStyle = printStyle;
            this.message = message;
            this.sender = sender;
        }
    }
    static List<PendingMessage> pendingMessages = new List<PendingMessage>();
    public static string defaultMeshName = "OnScreenPrinter output";


    #region Print -----------
    /// <summary>
    /// Spawn a textMesh with a message in front of the main camera
    /// </summary>
    public static void Print(object input)
    {
        OnScreenPrinter.Print(input, null);
    }
    public static void Print(object input, Object caller)
    {
        // Add pending debug message if no camera exists.
        if (!ValidCam())
        {
            DebugNoCamWarning();
            AddPendingMessage(input, PendingMessage.PrintStyle.Print, caller);
            return;
        }
        // Print on textDisplay and add debug infos.
        string lineIndic = lastIsSlash ? "\\" : "/";
        lastIsSlash = !lastIsSlash;
        AddToDisplay("[" + Time.time.ToString("00.0") + "]" + lineIndic + " " + input.ToString());
        if (caller != null)
            Debug.Log(input + " (" + caller.GetType() + " - " + caller.name + ")", caller);
        else
            Debug.Log(input);
    }

    /// <summary>
    /// Print a message without debug infos.
    /// </summary>
    /// <param name="input"></param>
    public static void CleanPrint(object input)
    {
        // Add pending clean message if no camera exists.
        if (!ValidCam())
        {
            DebugNoCamWarning();
            AddPendingMessage(input, PendingMessage.PrintStyle.CleanPrint, null);
            return;
        }
        // Get display and add it text.
        AddToDisplay(input.ToString());
    }

    static void AddToDisplay(string text)
    {
        // Add text to a display.
        if (textDisplay == null)
            CreateDisplay();
        textDisplay.text += FormatText(text, 30);
        textDisplay.text += "\n";
        if (textDisplay.text.Length > 2000)
            textDisplay.text = textDisplay.text.Remove(0, 1000);
    }

    /// <summary>
    /// Clean our message display instance.
    /// </summary>
    public static void Clear()
    {
        if (Camera.main == null || textDisplay == null)
            return;
        textDisplay.text = "";
    }
    #endregion ----------------------


    static void CreateDisplay()
    {
        // Create new printer.
        GameObject newObject = new GameObject();
        newObject.transform.parent = Camera.main.transform;
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localRotation = Quaternion.identity;
        newObject.name = defaultMeshName;
        newObject.transform.localPosition = new Vector3(0, -.2f, .5f);
        newObject.transform.localScale = Vector3.one * .01f;
        newObject.AddComponent<TextMesh>();
        textDisplay = newObject.GetComponent<TextMesh>();
        textDisplay.alignment = TextAlignment.Center;
        textDisplay.anchor = TextAnchor.LowerCenter;
        textDisplay.fontSize = 40;
    }


    static string FormatText(string text, int maxLength)
    {
        if (text.Length <= maxLength)
            return text;
        // The text is too long, format it by adding carriage returns.
        List<string> slices = new List<string>();
        int storedChars = 0;
        int lastBreak = 0;
        while (storedChars < text.Length)
        {
            bool sliced = false;
            string newSlice = "";
            if (text.Length < lastBreak + maxLength)
            {
                // We reached the end of the text, add the remaining part.
                newSlice = text.Substring(lastBreak, text.Length - lastBreak);
            }
            else
            {
                // Go backwards until the last break and search for a whitespace.
                for (int i = maxLength; i > 0; i--)
                {
                    if (text.Length > lastBreak + i && text[lastBreak + i] == ' ')
                    {
                        int newSliceLength = Mathf.Clamp(i, 0, text.Length);
                        newSlice = text.Substring(lastBreak, newSliceLength);
                        lastBreak += newSliceLength;
                        sliced = true;
                        break;
                    }
                }
                // If no whitespaces were found, slice arbitrarily.
                if (!sliced)
                {
                    int newSliceLength = Mathf.Clamp(lastBreak + maxLength, 0, text.Length - 1) - lastBreak;
                    newSlice = text.Substring(lastBreak, newSliceLength);
                    lastBreak += newSliceLength;
                }
            }
            // Store the new slice.
            slices.Add(newSlice + '\n');
            storedChars += newSlice.Length;
        }
        // Return slices in a string.
        string result = "";
        foreach (string str in slices)
            result += str;
        return result;
    }
    

    static bool ValidCam()
    {
        // Return whether we found a camera or not.
        if (Camera.main == null)
        {
            if (!warningMade)
            {
                Debug.Log("No main camera found. Putting messages on hold.");
                warningMade = true;
            }
            return false;
        }
        return true;
    }

    static void DebugNoCamWarning()
    {
        Debug.Log("OnScreenPrinter: No MainCamera was found. \n" +
            "The message will be printed when a MainCamera appears in the scene. To have a MainCamera, add the tag MainCamera to your primary camera.");
    }

    #region Print pending messages -------------------------
    static void AddPendingMessage(object message, PendingMessage.PrintStyle printStyle, Object sender)
    {
        PendingMessage newPendingMessage;
        if (sender == null)
            newPendingMessage = new PendingMessage(message, printStyle);
        else
            newPendingMessage = new PendingMessage(message, printStyle, sender);
        pendingMessages.Add(newPendingMessage);

        // Start waiting for the mainCam.
        if (waitingForValidCam == null)
        {
            waitingForValidCam = WaitForValidCam();
            if (CoroutineStarter.instance == null)
            {
                GameObject starter = new GameObject();
                starter.AddComponent<CoroutineStarter>();
            }
            CoroutineStarter.instance.StartCoroutine(waitingForValidCam);
        }
    }
    static IEnumerator waitingForValidCam;
    static IEnumerator WaitForValidCam()
    {
        // Print pending messages when we find a main camera.
        while (Camera.main == null)
        {
            yield return null;
        }
        foreach (PendingMessage pendingMessage in pendingMessages)
        {
            switch (pendingMessage.printStyle)
            {
                case PendingMessage.PrintStyle.Print:
                    Print(pendingMessage.message, pendingMessage.sender);
                    break;
                case PendingMessage.PrintStyle.CleanPrint:
                    CleanPrint(pendingMessage.message);
                    break;
            }
        }
        pendingMessages.Clear();
        waitingForValidCam = null;
        Destroy(CoroutineStarter.instance.gameObject);
    }
    #endregion --------------------------------------------------
}

class CoroutineStarter : MonoBehaviour {
    public static CoroutineStarter instance;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
}