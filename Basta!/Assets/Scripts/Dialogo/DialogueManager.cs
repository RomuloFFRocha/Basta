using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public bool haveDialogueMenu;             //checa se existe um menu de dialogo ou não
    private bool isTalking = false;
    
    [SerializeField] private GameObject modelMessageHalfLine;
    [SerializeField] private GameObject modelMessageHalfLinePlayer;
    [SerializeField] private GameObject modelMessageOneLine;
    [SerializeField] private GameObject modelMessageOneLinePlayer;
    [SerializeField] private GameObject modelMessageTwoLines;
    [SerializeField] private GameObject modelMessageTwoLinesPlayer;
    [SerializeField] private GameObject modelMessageThreeLines;
    [SerializeField] private GameObject modelMessageThreeLinesPlayer;
    [SerializeField] private GameObject modelMessageImage;
    [SerializeField] private GameObject modelMessageImagePlayer;
    private GameObject messageToSend;

    [SerializeField] private GameObject imageFullSize;

    [SerializeField] private GameObject modelButtonHalfLine;
    [SerializeField] private GameObject modelButtonOneLine;
    [SerializeField] private GameObject modelButtonTwoLines;
    [SerializeField] private GameObject modelButtonThreeLines;
    private GameObject buttonToCreate;
    [SerializeField] private GameObject buttonsPanel;

    [SerializeField] private RectTransform messageConteiner;
    [SerializeField] private RectTransform buttonsConteiner;
    [SerializeField] private RectTransform zapPanel;

    [SerializeField] private Button answerBar;
    [SerializeField] private Text[] celularTime;
    [SerializeField] private GameObject feedBackText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ChatButton chatButton;

    private List<RectTransform> dialogueBoxesRectTransforms = new List<RectTransform>();

    [SerializeField] private UIManager uiManager;

    [SerializeField] private int lineLength;

    [SerializeField] private Color[] colors;
    [SerializeField] private AudioClip messageSound;
    [SerializeField][Range(0, 1)] private float volume;

    private Vector3 messagePosition = new Vector3(0, 0, 0);

    private Message messageComponents;
    private ShowUpImageComponents imageComponents;
    
    private float inicialConteinerHeight;
    private float messagesInTheScreenHeight;
    private int messageIndex = 0;
    private Coroutine nextMessage;

    public static int totalPoints;

    private Queue<string> names = new Queue<string>();                              //lista de nomes
    private Queue<string> sentences = new Queue<string>();                          //lista de dialogos
    private Queue<Sprite> imagesSent = new Queue<Sprite>();
    private Queue<string> hours = new Queue<string>();
    private Queue<string> minutes = new Queue<string>();
    private Queue<float> times = new Queue<float>();
    private Queue<bool> whoSentTheMessage = new Queue<bool>();
    private Queue<int> nameColors = new Queue<int>();
    private Queue<string> buttonsText = new Queue<string>();                        //lista de botões do menu de dialogo
    private Queue<DialogueTrigger> buttonsDialogueToTrigger = new Queue<DialogueTrigger>();
    private Queue<int> buttonPointsToAdd = new Queue<int>();

    private CheckScore checkScore;
    private bool newMessage;
    
    private UnityEvent eventToTrigger;

    private Dictionary<string, GameObject> msg = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> img = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> btn = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mdl = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mdlBtn = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        mdl["modelMessageHalfLine"] = modelMessageHalfLine;
        mdl["modelMessageHalfLinePlayer"] = modelMessageHalfLinePlayer;
        mdl["modelMessageOneLine"] = modelMessageOneLine;
        mdl["modelMessageOneLinePlayer"] = modelMessageOneLinePlayer;
        mdl["modelMessageTwoLines"] = modelMessageTwoLines;
        mdl["modelMessageTwoLinesPlayer"] = modelMessageTwoLinesPlayer;
        mdl["modelMessageThreeLines"] = modelMessageThreeLines;
        mdl["modelMessageThreeLinesPlayer"] = modelMessageThreeLinesPlayer;
        mdl["modelMessageImage"] = modelMessageImage;
        mdl["modelMessageImagePlayer"] = modelMessageImagePlayer;

        mdlBtn["modelButtonHalfLine"] = modelButtonHalfLine;
        mdlBtn["modelButtonOneLine"] = modelButtonOneLine;
        mdlBtn["modelButtonTwoLines"] = modelButtonTwoLines;
        mdlBtn["modelButtonThreeLines"] = modelButtonThreeLines;

        inicialConteinerHeight = messageConteiner.sizeDelta.y;
    }

    //método chamado para inicializar todas as variaveis e iniciar o dialogo
    public void StartDialogue(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            names.Enqueue(sentence.name);
            sentences.Enqueue(sentence.sentence);
            hours.Enqueue(sentence.hour);
            minutes.Enqueue(sentence.minute);
            times.Enqueue(sentence.timeToNextMessage);
            imagesSent.Enqueue(sentence.imageSent);
            whoSentTheMessage.Enqueue(sentence.thePlayerHasSentTheMessage);
            nameColors.Enqueue(sentence.nameColor);
        }

        foreach (AnswerButton button in dialogue.answerButtons)
        {
            buttonsText.Enqueue(button.buttonText);
            buttonsDialogueToTrigger.Enqueue(button.dialogueToTrigger);
            buttonPointsToAdd.Enqueue(button.pointsWorth);
        }

        haveDialogueMenu = dialogue.haveDialogueMenu;
        checkScore = dialogue.checkScore;
        
        eventToTrigger = dialogue.eventToTrigger;
        newMessage = dialogue.newMessage;

        if (!isTalking)
        {
            DisplayNextSentence();
            isTalking = true;
        }
    }

    //escreve o dialogo
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            isTalking = false;
        
            if (haveDialogueMenu)
                DisplayDialogueMenu();

            if (checkScore != null)
                checkScore.Check(totalPoints);
            
            if (eventToTrigger != null)
                eventToTrigger.Invoke();
            
            return;
        }

        bool thePlayerHasSentTheMessage = whoSentTheMessage.Dequeue();
        string name = names.Dequeue();
        string sentence = sentences.Dequeue();
        string hour = hours.Dequeue();
        string minute = minutes.Dequeue();
        Sprite imageSent = imagesSent.Dequeue();
        int nameColor = nameColors.Dequeue();
        string keyMdl = "";

        if (imageSent != null)
        {
            keyMdl = "modelMessageImage";
        }
        else if (sentence.Length <= lineLength / 2)
        {
            keyMdl = "modelMessageHalfLine";
        }
        else if (sentence.Length <= lineLength)
        {
            keyMdl = "modelMessageOneLine";
        }
        else if (sentence.Length <= lineLength * 2)
        {
            keyMdl = "modelMessageTwoLines";
        }
        else
        {
            keyMdl = "modelMessageThreeLines";
        }

        keyMdl += (thePlayerHasSentTheMessage ? "Player" : "");
        messageToSend = mdl[keyMdl];

        msg["message " + messageIndex.ToString()] = PoolManager.SpawnObject(messageToSend, messagePosition, Quaternion.identity);
        msg["message " + messageIndex.ToString()].GetComponent<RectTransform>().SetParent(messageConteiner);
        msg["message " + messageIndex.ToString()].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        if (thePlayerHasSentTheMessage)
            messageComponents = msg["message " + messageIndex.ToString()].GetComponentInChildren<Message>();
        else
            messageComponents = msg["message " + messageIndex.ToString()].GetComponent<Message>();

        if (name != "")
        {
            messageComponents.nameObject.SetActive(true);
            messageComponents.nameText.text = name;
            messageComponents.nameText.color = colors[nameColor];
        }
        
        if (sentence != "")
        {
            messageComponents.sentenceObject.SetActive(true);
            messageComponents.sentenceText.text = sentence;

            int letterCount = sentence.Length;
            int letterIndex = 0;

            chatButton.previewText.text = "";

            if (letterCount >= 48)
            {
                while (letterCount > 3)
                {
                    chatButton.previewText.text += sentence[letterIndex];
                    letterIndex++;
                    letterCount--;
                }

                chatButton.previewText.text += "...";
            }
            else
            {
                while (letterCount > 0)
                {
                    chatButton.previewText.text += sentence[letterIndex];
                    letterIndex++;
                    letterCount--;
                }
            }
        }

        if (hour != "" && minute != "")
        {
            messageComponents.timeObject.SetActive(true);
            messageComponents.timeText.text = (hour + ":" + minute);
            chatButton.timeText.text = (hour + ":" + minute);

            if (newMessage)
            {
                for (int i = 0; i < celularTime.Length; i++)
                {
                    celularTime[i].text = (hour + ":" + minute);
                }
            }
        }

        if (newMessage)
        {
            if (audioSource.gameObject.activeSelf)
                chatButton.greenBall.SetActive(true);

            chatButton.chatRectTransform.SetSiblingIndex(1);
        }

        if (imageSent != null)
        {
            messageComponents.imageSentObject.SetActive(true);
            messageComponents.imageSentImage.sprite = imageSent;
            messageComponents.imageSentImage.SetNativeSize();

            int thisMessageIndex = messageIndex;
            img["image " + thisMessageIndex.ToString()] = PoolManager.SpawnObject(imageFullSize);
            imageComponents = img["image " + thisMessageIndex.ToString()].GetComponent<ShowUpImageComponents>();
            img["image " + thisMessageIndex.ToString()].SetActive(false);
            imageComponents.image.sprite = imageSent;
            imageComponents.image.SetNativeSize();
            
            messageComponents.imageSentButton.onClick.AddListener(delegate { DisplayImageFullSize(img["image " + thisMessageIndex.ToString()]); });
        }
        
        dialogueBoxesRectTransforms.Add(messageComponents.dialogueBoxRectTransform);

        if (messagesInTheScreenHeight <= inicialConteinerHeight)
        {
            messageConteiner.position = new Vector3(messageConteiner.position.x, messageConteiner.position.y - (messageComponents.dialogueBoxRectTransform.sizeDelta.y),
            messageConteiner.position.z);
            messagesInTheScreenHeight += messageComponents.dialogueBoxRectTransform.sizeDelta.y * 3/2;
        }
            
        else
        {
            messageConteiner.sizeDelta = new Vector2(messageConteiner.sizeDelta.x, messageConteiner.sizeDelta.y + messageComponents.dialogueBoxRectTransform.sizeDelta.y * 3 / 2);

            messageConteiner.position = new Vector3(messageConteiner.position.x, messageConteiner.position.y + (messageComponents.dialogueBoxRectTransform.sizeDelta.y * 3 / 2),
            messageConteiner.position.z);

            messagesInTheScreenHeight += messageComponents.dialogueBoxRectTransform.sizeDelta.y;
        }

        if (audioSource.gameObject.activeSelf && !thePlayerHasSentTheMessage)
            AudioManager.PlaySFX(audioSource, messageSound, volume);

        messageIndex++;

        float timeToNextMessage = times.Dequeue();

        if (nextMessage != null)
            StopCoroutine(nextMessage);

        nextMessage = StartCoroutine(NextMessage(timeToNextMessage));
    }

    //gera as respostas do menu de dialogo
    private void DisplayDialogueMenu()
    {
        int buttonsIndex = buttonsText.Count;
        answerBar.enabled = true;
        feedBackText.SetActive(true);
        buttonsPanel.SetActive(true);

        for (int i = 0; i < buttonsIndex; i++)
        {
            string buttonText = buttonsText.Dequeue();
            DialogueTrigger dialogueToTrigger = buttonsDialogueToTrigger.Dequeue();
            int pointsToAdd = buttonPointsToAdd.Dequeue();
            string keymdlBtn = "";
            
            if (buttonText.Length <= (lineLength / 2) - 1)
            {
                keymdlBtn = "modelButtonHalfLine";
            }
            else if (buttonText.Length <= lineLength - 1)
            {
                keymdlBtn = "modelButtonOneLine";
            }
            else if (buttonText.Length <= (lineLength * 2) - 1)
            {
                keymdlBtn = "modelButtonTwoLines";
            }
            else
            {
                keymdlBtn = "modelButtonThreeLines";
            }

            buttonToCreate = mdlBtn[keymdlBtn];

            btn["button" + i.ToString()] = PoolManager.SpawnObject(buttonToCreate);
            btn["button" + i.ToString()].GetComponent<RectTransform>().SetParent(buttonsConteiner);
            btn["button" + i.ToString()].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            btn["button" + i.ToString()].GetComponentInChildren<Text>().text = buttonText;

            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { isTalking = false; });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { dialogueToTrigger.TriggerDialogue(); });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { ResetButtons(buttonsIndex, btn); });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { AddPoints(pointsToAdd); });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { uiManager.HideAnswerButtons(zapPanel); });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { answerBar.enabled = false; });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { buttonsPanel.SetActive(false); });
            btn["button" + i.ToString()].GetComponent<Button>().onClick.AddListener(delegate { feedBackText.SetActive(false); });
        }
    }
    
    private void DisplayImageFullSize(GameObject image)
    {
        image.SetActive(true);
    }

    private void ResetButtons(int index, Dictionary<string, GameObject> btn)
    {
        for (int i = 0; i < index; i++)
        {
            btn["button" + i.ToString()].GetComponent<Button>().onClick.RemoveAllListeners();
            PoolManager.ReleaseObject(btn["button" + i.ToString()]);
        }
    }

    private void AddPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }
    
    private IEnumerator NextMessage(float timeToNextMessage)
    {
        yield return new WaitForSeconds(timeToNextMessage * Settings.messageSpeed);

        DisplayNextSentence();
    }
}
