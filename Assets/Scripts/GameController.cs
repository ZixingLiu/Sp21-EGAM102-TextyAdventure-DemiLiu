using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class GameController : MonoBehaviour
{
    //We need a reference to the place where we print text. This variable will hold that reference.
    public Text OutputText;

    //We need to make sure the output space scrolls downwards when we add new text.
    //Therefore, we need a reference to that vertical scrollbar.
    //This variable will hold a reference to that vertical scrollbar.
    public Scrollbar VerticalScrollbar;

    //To read player input, we need a reference to the palce where the player types their input.
    //This variable will hold that reference.
    public InputField InputText;
    public GameObject FruityTree;
    public GameObject slime;
    public float TreePlantX;
    public List<string> SceneInventory, PlayerInventory, Command;
    public List <int> ResourceInventory;
    

    //Later on, we need to break the input into words.
    //That means splitting a string at every space: ' '
    //The variable below holds just a single space.
    //So when we ask C# to split the input,
    //and C# asks 'where', we can give it this variable,
    //and C# will know to split the input at the spaces.
    private char[] spaceCharacter = new[] { ' ' };
    private int wood, stars;
    private GameObject newTree;

    // Start is called before the first frame update
    void Start()
    {
        slime = GameObject.Find("Slime");

        //Print starting text. So the player knows the game has started.
        OutputText.text = "The game is afoot! \n";
        //Move the scrollbar to the end .. after a small delay.
        //UI things take time. If you try to move the scrollbar right after
        //printing text, the UI system will get confused.
        //So, move the scrollbar, but with a .1 second delay.
        Invoke("MoveScrollbarToBottom", .1f);

    }

    void MoveScrollbarToBottom()
    {
        VerticalScrollbar.value = 0;
    }

    void Update()
    {
        //autoComplete when user press Tab
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            for(int i=0; i< Command.Count; i++)
            {
                string command = Command[i];
                for(int j=0; j< command.Length; j++)
                {
                    string chara = command.Substring(0, j);

                    if(chara == InputText.text)
                    {
                        InputText.text = command;
                        InputText.MoveTextEnd(false);
                        break;
                        
                    }
                }
                
            }
           
        }
     
       //Did the player press return? If so, look at the input text, and do what needs doing.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Capitalization can confuse computers. To a computer "Hello world" is different than "hello world"
            //Here, convert input text to lower case, so our code doesn't get confused by CaPiTaL letters
            InputText.text = InputText.text.ToLower();

            //We want to be able to look at individual words in the player input. 
            //Fortunately, C# has a function that will split a big string into little strings
            //Below, we tell C# to split the input string at every space, and make us an array of the pieces.
            //Each piece will be a word.
            string[] inputWords = InputText.text.Split(spaceCharacter);

            //If player says "Hello world" or "hello world" or "HELLO WORLD" etc
            if (inputWords[0] == "hello" && inputWords[1] == "world")
            {
                //The world does *not* say 'hello' in return. The world is silent.
                OutputText.text += "The world is silent\n";
                //Tell Unity to move the scrollbar in .1 seconds.
                Invoke("MoveScrollbarToBottom", .1f);
            }
            else if (inputWords[0] == "get")
            {
                if (inputWords[1] == "hat" && SceneInventory.Contains("hat"))
                {
                    GameObject.Find("Hat").GetComponent<SpriteRenderer>().enabled = false;
                    SceneInventory.Remove("hat");
                    PlayerInventory.Add("hat");
                    OutputText.text += "You pick up the hat.\n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else if (inputWords[1] == "glasses" && SceneInventory.Contains("glasses"))
                {
                    GameObject.Find("Glasses").GetComponent<SpriteRenderer>().enabled = false;
                    SceneInventory.Remove("glasses");
                    PlayerInventory.Add("glasses");
                    OutputText.text += "You pick up the glasses.\n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else if (inputWords[1] == "fish" && inputWords[2] == "net" && SceneInventory.Contains("fishNet"))
                {
                    GameObject.Find("FishNet").GetComponent<SpriteShapeController>().enabled = false;
                    SceneInventory.Remove("fishNet");
                    PlayerInventory.Add("fishNet");
                    OutputText.text += "You pick up the fish net. You need to find an bee to attract fish.\n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                    SceneInventory.Add("bee");
                    GameObject.Find("Bee").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("FishNetInHand").GetComponent<SpriteShapeController>().enabled = true;
                }
                else if (inputWords[1] == "bee" && SceneInventory.Contains("bee"))
                {
                    GameObject.Find("Bee").GetComponent<SpriteRenderer>().enabled = false;
                    SceneInventory.Remove("bee");
                    PlayerInventory.Add("bee");
                    OutputText.text += "You pick up the bee \n <color=yellow>Cat:Also you need to put on these waders to walk into water</color> \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                    GameObject.Find("Waders").GetComponent<SpriteShapeController>().enabled = true;
                    SceneInventory.Add("waders");
                }
                else if (inputWords[1] == "waders" && SceneInventory.Contains("waders"))
                {
                    GameObject.Find("Waders").GetComponent<SpriteShapeController>().enabled = false;
                    SceneInventory.Remove("waders");
                    PlayerInventory.Add("waders");
                    OutputText.text += "You pick up the waders \n ";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else
                {
                    OutputText.text += "You cannot get " + inputWords[1] + " here \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
            }
            //use
            else if (inputWords[0] == "use")
            {
                if (inputWords[1] == "bee" && PlayerInventory.Contains("bee"))
                {
                    GameObject.Find("BeeInPond").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("Fish").GetComponent<SpriteRenderer>().enabled = true;
                    OutputText.text += "<color=yellow> Cat: Fish appear !!! meow</color> \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                    PlayerInventory.Remove("bee");

                }
                else if (inputWords[1] == "fish" && inputWords[2] == "net" && PlayerInventory.Contains("fishNet"))
                {
                    GameObject.Find("BeeInPond").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("Fish").GetComponent<SpriteRenderer>().enabled = false;
                    OutputText.text += "You catch the fish. \n <color=yellow>Cat: Thank you so much human. meow</color> \n";
                    
                   
                    GameObject.Find("CatchedFish").GetComponent<SpriteRenderer>().enabled = true;
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else
                {
                    OutputText.text += "You cannot use " + inputWords[1] + " now \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
            }
            //wear
            else if (inputWords[0] == "wear")
            {
                if (inputWords[1] == "hat" && PlayerInventory.Contains("hat"))
                {
                    GameObject.Find("HatOnHead").GetComponent<SpriteRenderer>().enabled = true;
                    OutputText.text += "You put the hat on your head. \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else if (inputWords[1] == "waders" && PlayerInventory.Contains("waders"))
                {
                    OutputText.text += "You put waders on. Now you can walk into water.\n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else if (inputWords[1] == "glasses" && PlayerInventory.Contains("glasses"))
                {
                    GameObject.Find("GlassesOnHead").GetComponent<SpriteRenderer>().enabled = true;
                    OutputText.text += "You put the hat on your glasses. \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else
                {
                    OutputText.text += "You don't have a " + inputWords[1] + ". You can't wear you don't have. \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
            }
            //harvest
            else if (inputWords[0] == "harvest")
            {
                if (inputWords[1] == "tree" && SceneInventory.Contains("tree"))
                {
                    
                    Destroy(GameObject.Find("FruityTree(Clone)"));
                    SceneInventory.Remove("tree");
                    wood += 10;
                    stars += 3;
                    OutputText.text += "You gain 10 woods and 3 stars \n";

                    ResourceInventory[0] = wood;
                    ResourceInventory[1] = stars;


                    PlayerInventory.Add("treeSeed");
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else
                {
                    OutputText.text += "There is no  " + inputWords[1] + " you can harvest. \n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
            }
            //plant
            else if (inputWords[0] == "plant")
            {
                if (inputWords[1] == "tree" && PlayerInventory.Contains("treeSeed"))
                {

                    
                    SceneInventory.Add("tree");
                    PlayerInventory.Remove("treeSeed");
                    OutputText.text += "You plant a tree \n";
                    
                    newTree = Instantiate(FruityTree, this.transform) ;
                    newTree.transform.position = new Vector3(Random.Range(-TreePlantX, TreePlantX), -2f, 0);

                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
                else
                {
                    OutputText.text += "You do not have " + inputWords[1] + " seed to plant.\n";
                    Invoke("MoveScrollbarToBottom", 0.1f);
                }
            }
            //inventory
            else if (inputWords[0] == "inventory")
            {
                string inventoryString = "You have: ";
                for (int i = 0; i < PlayerInventory.Count; i++)
                {
                    inventoryString += PlayerInventory[i] + ", ";
                }
                OutputText.text += inventoryString + "\n";
                Invoke("MoveScrollbarToBottom", 0.1f);
            }
            //look
            else if (inputWords[0] == "look")
            {
                string sceneString = "There are: ";
                for (int i = 0; i < SceneInventory.Count; i++)
                {
                    sceneString += SceneInventory[i] + ", ";
                }
                OutputText.text += sceneString + "\n";
                Invoke("MoveScrollbarToBottom", 0.1f);
            }
            //talk
            else if (inputWords[0] == "talk")
            {

                if (inputWords.Length > 2)
                {
                    if (inputWords[2] == "hungry" && PlayerInventory.Contains("hungryTopic"))
                    {
                        
                        OutputText.text += "<color=yellow>Cat: I can give you this fishing net. Can you help me catch a fish? meow</color> \n";
                        GameObject.Find("FishNet").GetComponent<SpriteShapeController>().enabled = true;
                        PlayerInventory.Remove("hungryTopic");
                        SceneInventory.Add("fishNet");
                        Invoke("MoveScrollbarToBottom", 0.1f);

                    }
                    else if (inputWords[2] == "fish" && PlayerInventory.Contains("fishTopic"))
                    {
                       
                        OutputText.text += "<color=yellow>Cat: Fish is my favorite food. meow</color> \n";

                        PlayerInventory.Remove("fishTopic");
                        Invoke("MoveScrollbarToBottom", 0.1f);
                    }
                    else
                    {
                        OutputText.text += "<color=yellow>Cat: Meow ? </color> \n";

                        Invoke("MoveScrollbarToBottom", 0.1f);
                    }
                }
                else
                {
                    if (inputWords[1] == "cat" && SceneInventory.Contains("cat"))
                    {

                        OutputText.text += "<color=yellow>Cat: QwQ, I am <b>hungry</b>. There is a pond, " +
                            "but the water is too deep for me to catch <b>fish</b> meow.</color> \n";

                        PlayerInventory.Add("hungryTopic");
                        PlayerInventory.Add("fishTopic");
                        Invoke("MoveScrollbarToBottom", 0.1f);
                    }
                    else
                    {
                        OutputText.text += " There is no " + inputWords[1] + " you can talk to.\n";
                        Invoke("MoveScrollbarToBottom", 0.1f);
                    }
                }


            }
            else if(inputWords[0] == "slime")
            {
                if(inputWords[1] == "sit")
                {
                    slime.GetComponent<Animator>().SetTrigger("Sit");
                }
                else if(inputWords[1] == "jump")
                {
                    slime.GetComponent<Animator>().SetTrigger("Jump");
                }
                else if (inputWords[1] == "float")
                {
                    slime.GetComponent<Animator>().SetTrigger("Float");
                }
                else if (inputWords[1] == "shake")
                {
                    slime.GetComponent<Animator>().SetTrigger("Shake");
                }
            }
            else //If the player says anything else
            {
                //Print the super annoying thing that Kings Quest II prints. It's an inside joke. Sort of.
                OutputText.text += "You can't do that, at least not now.\n";
                //Tell Unity to move the scrollbar in .1 seconds.
                Invoke("MoveScrollbarToBottom", 0.1f);
            }

            //Reset the input field. This is a two-step process
            //Put nothingness in the field. So it shows nothing.
            InputText.text = "";
            //Reset all the back-end Unity input processing stuff, by re-activating the field.
            InputText.ActivateInputField();
        }
    }

}
