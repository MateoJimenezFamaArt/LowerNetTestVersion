using System;
using System.Security.Cryptography;


public class Program
{

    /*RPS breakdown
    Light Beat Darkness
    Darkness Beat Neutral
    Neutral Beats Light

    Game: User must say out load Spell Nature + Spell Adjective to cast a spell and press enter to shoot it
        If they hit their enemy with a spell more potent they can deal extra damage
    */

    static string[] spellNature = new string[3] { "Neutral", "Light" , "Darkness"}; //Array with Natures
    static string[] spellAdjective = new string[3] {"Spark" , "Ray" , "Cone"}; // Array with Adjectives
    static string[] spellCasted = new string[3] { "Spark of Neutrality", "Ray of Light", "Cone of Darkness" }; // Array with Spell Casts

    static int playerHealth = 100; // Players health value
    static int garysHealth = 100; // Garys health
    static int magicalDamage = 10; // Base magic dmg
    static string playerName = ""; // Player name 

    static VoiceToTextProtocol voice = new VoiceToTextProtocol(); //Voice text protocol class
    static Random random = new Random(); // Random number generator


    static void Main(string[] args) 
    {

        voice = new VoiceToTextProtocol(); //<-- Created an instance of a VoiceToTextProtocol class

        //TLDR Figure out with teach how to make the path for the model document itself be embeded in the project as to make it compatible with other computres / enviroments
        voice.VoiceToTextProtocolCreator("C:\\Users\\gd81mateo\\Desktop\\VFS_LOCAL\\Term 1\\Programing\\Deliverable1TestOnLowerNet\\VoskLite");//<-- Initialized the model with the path to it
        //Meanwhile every time you get to a new enviroment just change the path to the model folder accordingly by copying its path from the project itslef in the ASK THE TEACH folder

        Console.Clear(); // Clear all the dependencies bloat

        Greetings(); // Salutes and catches players name

        Spells(); // Gives the mix of spells

        DisplayInstructions(); // Displays instructions

        StartCombatSequence(); // Starts the combat


    }


    private static void Greetings()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Hello wizard, what is your name?"); //Ask the players name
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        playerName = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Welcome, {playerName}!");
        Console.WriteLine("[Press enter to continue]");
        Console.ReadKey();
        Console.Clear();
    }
    private static void Spells()
    {
        Console.WriteLine($"{playerName} this are your current spells, use them to win against your opponent: GARY THE WIZARD");
        for (int i = 0; i < spellNature.Length; i++) 
        {
            switch (i)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;

                default:
                    break;
            };

            Console.WriteLine($"Spell  adjective: {spellAdjective[i]}");
            Console.WriteLine($"Spell nature: {spellNature[i]}");
            Console.WriteLine($"{spellAdjective[i]} + {spellNature[i]} = {spellCasted[i]}");

            if (i == 2) {Console.BackgroundColor = ConsoleColor.Black;}
        }

        Console.WriteLine("[Press enter to continue]");
        Console.ReadKey();
        Console.Clear();
    }
    private static void DisplayInstructions()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        //Console.WriteLine("Light beats Darkness // Darkness Beats Neutral // Neutral Beats Light");
        Console.WriteLine($"After combining successfully a spell adjective and a spell nature you will successfully cast the spell");
        Console.WriteLine("For example, if you wish to cast Cone of Darkness");
        Console.WriteLine("1. Speak into the mic the word 'cone' when the prompt shows to speak a spells Adjective");
        Console.WriteLine("2. Speak into the mic the word 'darkness' when the prompt shows to speak a spells Nature");
        Console.WriteLine("Keep in mind that every round of spell casting both you and gary will suffer damage");
        Console.WriteLine("[Press enter to start your epic wizard fight]");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
    }
    private static void StartCombatSequence()
    {
        string playerSpell = "";
        string garySpell = "";
        bool theyAreAlive = true;

        Console.WriteLine("START EPIC COMBAT SEQUENCE");

        while (theyAreAlive)
        {
            // Player attacks by casting an incantation
            playerSpell = PlayerCastsASpell();

            //Gary gets allocated a random spell to cast
            garySpell = GaryCastsASpell();

            // Determine wizard winner
            DetermineTheMostPowerfullWizard(playerSpell, garySpell);

            if (garysHealth <= 0) { theyAreAlive = false;}
            if (playerHealth <= 0) { theyAreAlive = false; }
        }

        if(playerHealth <= 0 && garysHealth >= 0) 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("GARY THE WIZARD has won the battle of wizards proving himself superior!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        if (garysHealth <= 0 && playerHealth >= 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{playerName} is victorious on the epic battle of the wizards");
            Console.ForegroundColor = ConsoleColor.White;
        }
        if (playerHealth < 0 && garysHealth < 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Both wizards fall to the ground as they slowly die, the council declares a draw between the two legendary wizards!");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
    private static string PlayerCastsASpell()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"{playerName} prepares to cast an all powerful incantation to face against GARY THE WIZARD");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cast your Spell Adjective");
        Console.ForegroundColor = ConsoleColor.White;
        string PlayerAdjective = voice.ReadPlayersInput();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cast your Spell Nature");
        Console.ForegroundColor = ConsoleColor.White;
        string PlayerNature = voice.ReadPlayersInput();
        //Console.WriteLine(PlayerAdjective + PlayerNature); DEBUG

        if (PlayerNature.ToLower() + PlayerAdjective.ToLower()  == "neutralspark") //DONE DONE
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("The player casted " + spellCasted[0]);
            return spellCasted[0];
        }
        if (PlayerNature.ToLower() + PlayerAdjective.ToLower()  == "lightray") // DONE DONE
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("The player casted " + spellCasted[1]);
            return spellCasted[1];
        }
        if (PlayerNature.ToLower() + PlayerAdjective.ToLower() == "darknesscone") // DONE DONE
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("The player casted " + spellCasted[2]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            return spellCasted[2];
        }
        else // Incantation gets checked to see if its a valid spell?
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GARY THE WIZARD CHUCKLES as you fumble your spells");
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }

    }
    private static string GaryCastsASpell()
    {
        int randomSpellChooser = random.Next(3);
        return spellCasted[randomSpellChooser];
        //Console.WriteLine("The random chose spell for gary is " + SpellCasted[randomSpellChooser]); DEBUG
    }
    private static void DetermineTheMostPowerfullWizard(string playersInputSpell , string garyInputSpell)
    {
        
        //FIND A MORE EFICIENT WAY TO MAKE THIS HAPPEN INSTEAD OF REPEATING SO MUCH CODE ASK FOR SOME FEEDBACK ONCE ASSIGNMENT IS DONE
        // OR ASK TA FOR HELP
        
        //SPARK
        if (playersInputSpell == spellCasted[0] && garyInputSpell == spellCasted[1]) // Player neutrality and gary light so player deals x2 damage
        {
            playerHealth -= magicalDamage / 2;
            garysHealth -= magicalDamage * 2;
            Console.WriteLine($"{playerName} {playersInputSpell} beats GARY THE WIZARDS {garyInputSpell}");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);
        }
        if (playersInputSpell == spellCasted[0] && garyInputSpell == spellCasted[2]) // Gary darkness and player neutrality so gary deals x2 damage
        {
            garysHealth -= magicalDamage / 2;
            playerHealth -= magicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {garyInputSpell} has too much power for the flimsy {playersInputSpell} that {playerName} just casted!");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);
        }
        if (playersInputSpell == spellCasted[0] && garyInputSpell == spellCasted[0]) // Both casted the same
        {
            garysHealth -= magicalDamage;
            playerHealth -= magicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power, both casted {playersInputSpell}");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);

        }
        //LIGHT
        if (playersInputSpell == spellCasted[1] && garyInputSpell == spellCasted[1]) // Both casted the same 
        {
            garysHealth -= magicalDamage;
            playerHealth -= magicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power, both casted {playersInputSpell}");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);

        }
        if (playersInputSpell == spellCasted[1] && garyInputSpell == spellCasted[2]) // Player cast light Gary cast Dark Player deals 2x
        {
            playerHealth -= magicalDamage / 2;
            garysHealth -= magicalDamage * 2;
            Console.WriteLine($"{playerName} {playersInputSpell} beats GARY THE WIZARDS {garyInputSpell}!");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);
        }
        if (playersInputSpell == spellCasted[1] && garyInputSpell == spellCasted[0])  // Gary cast neutral player cast light Gary deals 2x
        {
            garysHealth -= magicalDamage / 2;
            playerHealth -= magicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {garyInputSpell} has too much power for the flimsy {playersInputSpell} that {playerName} just casted!");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);

        }
        //DARK
        if (playersInputSpell == spellCasted[2] && garyInputSpell == spellCasted[1]) // Player cast dark Gary cast light Gary deals 2x 
        {
            garysHealth -= magicalDamage / 2;
            playerHealth -= magicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {garyInputSpell} has too much power for the flimsy  {playersInputSpell}  that {playerName} just casted!");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);

        }
        if (playersInputSpell == spellCasted[2] && garyInputSpell == spellCasted[2]) // Both cast same
        {
            garysHealth -= magicalDamage;
            playerHealth -= magicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power, both casted  {playersInputSpell}");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);

        }
        if (playersInputSpell == spellCasted[2] && garyInputSpell == spellCasted[0])  // Player cast dark Gary cast neutral Player deals 2x
        {
            playerHealth -= magicalDamage / 2;
            garysHealth -= magicalDamage * 2;
            Console.WriteLine($"{playerName} spell beats GARY THE WIZARDS {garyInputSpell}!");
            Console.WriteLine("Player Health: " + playerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + garysHealth);
        }
        
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Wizards prepare your next spells GO!");
        Console.WriteLine("[Press enter to Start the next round]");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();
        Console.Clear();

    }

    private static void GoodBye()
    {
        Console.WriteLine(@"
XXXXX                 XXXXXXXXXXX    X                                     
X    XXXXX                 X         X                   XXXXXXXXXXXX      
X         XX               X         X                XXX           XXX    
X          X               X         X              XX                XX   
X         XX               X         X            XX                    X  
X         X                X         X            X                     XX 
X       XXX                X         X           XX                      XX
X      XX                  X         X           X                        X
X   XXX                    X         X           X                       X 
XXXX                       X         X           X                       X 
XXXX                       X         X           X                       X 
X  XXXXXXXXXXX             X         X           X                      XX 
X             XXXX         X         X           X                      X  
X                XX        X         X           X                     X   
X                  X       X         X            XX                  X    
X                 XX       X         X             XX                XX    
X                 X        X         X              XXX              X     
X                X         X         X                 XXXX       XXX      
X              XX          X         X                     XXXXXXXX        
X             XX           X         X                                     
X          XXX             X         X                                     
X   XXXXXXXX               X         X                                     
XXXXX                 XXXXXXXXXXX    XXXXXXXXXXXXXXXX                              

        ");
        Console.WriteLine("Thanks for playing this code is made and owned by BILO ");

    }

}


/*
XXXXX                 XXXXXXXXXXX    X                                     
X    XXXXX                 X         X                   XXXXXXXXXXXX      
X         XX               X         X                XXX           XXX    
X          X               X         X              XX                XX   
X         XX               X         X            XX                    X  
X         X                X         X            X                     XX 
X       XXX                X         X           XX                      XX
X      XX                  X         X           X                        X
X   XXX                    X         X           X                       X 
XXXX                       X         X           X                       X 
XXXX                       X         X           X                       X 
X  XXXXXXXXXXX             X         X           X                      XX 
X             XXXX         X         X           X                      X  
X                XX        X         X           X                     X   
X                  X       X         X            XX                  X    
X                 XX       X         X             XX                XX    
X                 X        X         X              XXX              X     
X                X         X         X                 XXXX       XXX      
X              XX          X         X                     XXXXXXXX        
X             XX           X         X                                     
X          XXX             X         X                                     
X   XXXXXXXX               X         X                                     
XXXXX                 XXXXXXXXXXX    XXXXXXXXXXXXXXXX                                

  */