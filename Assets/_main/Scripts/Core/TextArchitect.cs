using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect 
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro : tmpro_world;


    public string currentText => tmpro.text;
    public string targetText { get; private set; } = "";
    public string preText { get; private set; } = "";
    private int preTextLength = 0;

    public string FullTargetText => preText + targetText;

    public enum BuildMethod {  instant,typewriter,fade};
    public BuildMethod buildMethod = BuildMethod.typewriter;

    public Color textColor {
        get { return tmpro.color; }
        set { tmpro.color = value; }
    }

    public float speed {
        get { return baseSpeed * speedMultiPlier; }
        set { speedMultiPlier = value; }
    }
    private const float baseSpeed = 1;
    private float speedMultiPlier = 1;
    private int characterMultiplier = 1;

    public int charactersPerCycles { // increases number of characters depending on what speed is put at
        get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; }
    }

    public bool hurryUp = false;

    // because not attached to game object needs a contructor

    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }

    public TextArchitect(TextMeshPro tmpro_world)
    {
        this.tmpro_world = tmpro_world;
    }
    // build new text
    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;
        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }
    // apped text to what is already in text architect
    public Coroutine Append(string text)
    {
        preText = tmpro.text;
        targetText = text;
        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }


    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;
    // stop text already in motion
    public void Stop()
    {
        if (!isBuilding)
        {
            return;
        }
        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }


    IEnumerator Building()
    {
        yield return null;
    }

}
