using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI text;
    
    public static Fade instance;

    public AudioClip fightSceneAudio;
    public AudioClip writeAudio;
    public AudioClip endDialogueAudio;
    public AudioClip confirmAudio;
    public AudioClip skipAudio;


    public int gameScene;

    bool canDialogue = true;
    public GameObject btnLoad;
    public Button btnSkip;

    [Range(0,0.1f)]
    public float dialogueLetterDelay;
    public int lettersPerSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence( string sentence) 
    {
        int count = 0;
        text.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            text.text += letter;
            count++;
            if (writeAudio != null && count == lettersPerSound) {
                AudioManager.instance.PlaySound(writeAudio);
                count = 0;
            }
            if (canDialogue) yield return new WaitForSeconds(dialogueLetterDelay);
            else {
                text.text = sentence;
                Skip();
                yield break; 
            }
        }
        btnLoad.SetActive(true);
        btnSkip.interactable = false;
        if (endDialogueAudio != null)
            AudioManager.instance.PlaySound(endDialogueAudio);
        yield return new WaitForSeconds(3f);

    }
    
    public void LoadScene() {
        AudioManager.instance.PlaySound(confirmAudio);
        LoadManager.instance.LoadScene(gameScene);
        AudioManager.instance.FadeMusic(fightSceneAudio);
    }

    public void Skip() {
        AudioManager.instance.PlaySound(skipAudio);
        btnLoad.SetActive(true);
        canDialogue = false;
        btnSkip.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && btnSkip.interactable) Skip();
    }

}
