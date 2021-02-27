using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Later on, we need to break the input into words.
    //That means splitting a string at every space: ' '
    //The variable below holds just a single space.
    //So when we ask C# to split the input,
    //and C# asks 'where', we can give it this variable,
    //and C# will know to split the input at the spaces.
    private char[] spaceCharacter = new[] { ' ' };

    // Start is called before the first frame update
    void Start()
    {
        //Print starting text. So the player knows the game has started.
        OutputText.text = "The game is afoot!";
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
            else //If the player says anything else
            {
                //Print the super annoying thing that Kings Quest II prints. It's an inside joke. Sort of.
                OutputText.text += "You can't do that, at least not now.\n";
                //Tell Unity to move the scrollbar in .1 seconds.
                Invoke("MoveScrollbarToBottom", .1f);
            }

            //Reset the input field. This is a two-step process
            //Put nothingness in the field. So it shows nothing.
            InputText.text = "";
            //Reset all the back-end Unity input processing stuff, by re-activating the field.
            InputText.ActivateInputField();
        }
    }

}
