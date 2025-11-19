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

    static string[] SpellNature = new string[3] { "Neutral", "Light" , "Darkness"}; //Array with Natures
    static string[] SpellAdjective = new string[3] {"Spark" , "Ray" , "Cone"}; // Array with Adjectives
    static string[] SpellCasted = new string[3] { "Spark of Neutrality", "Ray of Light", "Cone of Darkness" }; // Array with Spell Casts

    static int PlayerHealth = 100; // Players health value
    static int GarysHealth = 100; // Garys health
    static int MagicalDamage = 10; // Base magic dmg
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
        Console.WriteLine("Hello wizard, what is your name?"); //Ask the players name
        playerName = Console.ReadLine();
        Console.WriteLine($"Welcome, {playerName}!");
        Console.WriteLine("[Press enter to continue]");
        Console.ReadKey();
        Console.Clear();
    }
    private static void Spells()
    {
        Console.WriteLine($"{playerName} this are your current spells, use them to win against your opponent: GARY THE WIZARD");
        for (int i = 0; i < SpellNature.Length; i++) 
        {
            Console.WriteLine($"Spell  adjective: {SpellAdjective[i]}");
            Console.WriteLine($"Spell nature: {SpellNature[i]}");
            Console.WriteLine($"{SpellAdjective[i]} + {SpellNature[i]} = {SpellCasted[i]}");
        }
        Console.WriteLine("[Press enter to continue]");
        Console.ReadKey();
        Console.Clear();
    }
    private static void DisplayInstructions()
    {
        //Console.WriteLine("Light beats Darkness // Darkness Beats Neutral // Neutral Beats Light");
        Console.WriteLine($"After combining successfully a spell adjective and a spell nature you will successfully cast the spell");
        Console.WriteLine("[Press enter to start your epic wizard fight]");
        Console.ReadKey();
        Console.Clear();
    }
    private static void StartCombatSequence()
    {
        string PlayerSpell = "";
        string GarySpell = "";
        bool TheyAreAlive = true;

        Console.WriteLine("START EPIC COMBAT SEQUENCE");

        while (TheyAreAlive)
        {
            // Player attacks by casting an incantation
            PlayerSpell = PlayerCastsASpell();

            //Gary gets allocated a random spell to cast
            GarySpell = GaryCastsASpell();

            // Determine wizard winner
            DetermineTheMostPowerfullWizard(PlayerSpell, GarySpell);

            if (GarysHealth <= 0) { TheyAreAlive = false;}
            if (PlayerHealth <= 0) { TheyAreAlive = false; }
        }

        if(PlayerHealth <= 0 && GarysHealth >= 0) 
        {
            Console.WriteLine("GARY THE WIZARD has won the battle of wizards proving himself superior!");
        }
        if (GarysHealth <= 0 && PlayerHealth >= 0)
        {
            Console.WriteLine($"{playerName} is victorious on the epic battle of the wizards");
        }
        if (PlayerHealth < 0 && GarysHealth < 0)
        {
            Console.WriteLine("Both wizards fall to the ground as they slowly die, the council declares a draw between the two legendary wizards!");
        }

    }
    private static string PlayerCastsASpell()
    {
        Console.WriteLine($"{playerName} prepares to cast an all powerful incantation to face against GARY THE WIZARD");
        Console.WriteLine("Cast your Spell Adjective");
        string PlayerAdjective = voice.ReadPlayersInput();
        Console.WriteLine("Cast your Spell Nature");
        string PlayerNature = voice.ReadPlayersInput();
        //Console.WriteLine(PlayerAdjective + PlayerNature); DEBUG

        if (PlayerNature.ToLower() + PlayerAdjective.ToLower()  == "neutralspark") //DONE DONE
        {
            Console.WriteLine("The player casted " + SpellCasted[0]);
            return SpellCasted[0];
        }
        if (PlayerNature.ToLower() + PlayerAdjective.ToLower()  == "lightray") // DONE DONE
        {
            Console.WriteLine("The player casted " + SpellCasted[1]);
            return SpellCasted[1];
        }
        if (PlayerNature.ToLower() + PlayerAdjective.ToLower() == "darknesscone") // DONE DONE
        {
            Console.WriteLine("The player casted " + SpellCasted[2]);
            return SpellCasted[2];
        }
        else // Incantation gets checked to see if its a valid spell?
        {
            Console.WriteLine("GARY THE WIZARD CHUCKLES as you fumble your spells");
            return "";
        }

    }
    private static string GaryCastsASpell()
    {
        int randomSpellChooser = random.Next(3);
        return SpellCasted[randomSpellChooser];
        //Console.WriteLine("The random chose spell for gary is " + SpellCasted[randomSpellChooser]); DEBUG
    }
    private static void DetermineTheMostPowerfullWizard(string PlayersInputSpell , string GaryInputSpell)
    {
        
        //FIND A MORE EFICIENT WAY TO MAKE THIS HAPPEN INSTEAD OF REPEATING SO MUCH CODE ASK FOR SOME FEEDBACK ONCE ASSIGNMENT IS DONE
        // OR ASK TA FOR HELP
        
        //SPARK
        if (PlayersInputSpell == SpellCasted[0] && GaryInputSpell == SpellCasted[1]) // Player neutrality and gary light so player deals x2 damage
        {
            PlayerHealth -= MagicalDamage / 2;
            GarysHealth -= MagicalDamage * 2;
            Console.WriteLine($"{playerName} {PlayersInputSpell} beats GARY THE WIZARDS {GaryInputSpell}");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);
        }
        if (PlayersInputSpell == SpellCasted[0] && GaryInputSpell == SpellCasted[2]) // Gary darkness and player neutrality so gary deals x2 damage
        {
            GarysHealth -= MagicalDamage / 2;
            PlayerHealth -= MagicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {GaryInputSpell} has too much power for the flimsy {PlayersInputSpell} that {playerName} just casted!");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);
        }
        if (PlayersInputSpell == SpellCasted[0] && GaryInputSpell == SpellCasted[0]) // Both casted the same
        {
            GarysHealth -= MagicalDamage;
            PlayerHealth -= MagicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);

        }
        //LIGHT
        if (PlayersInputSpell == SpellCasted[1] && GaryInputSpell == SpellCasted[1]) // Both casted the same 
        {
            GarysHealth -= MagicalDamage;
            PlayerHealth -= MagicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);

        }
        if (PlayersInputSpell == SpellCasted[1] && GaryInputSpell == SpellCasted[2]) // Player cast light Gary cast Dark Player deals 2x
        {
            PlayerHealth -= MagicalDamage / 2;
            GarysHealth -= MagicalDamage * 2;
            Console.WriteLine($"{playerName} {PlayersInputSpell} beats GARY THE WIZARDS {GaryInputSpell}!");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);
        }
        if (PlayersInputSpell == SpellCasted[1] && GaryInputSpell == SpellCasted[0])  // Gary cast neutral player cast light Gary deals 2x
        {
            GarysHealth -= MagicalDamage / 2;
            PlayerHealth -= MagicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {GaryInputSpell} has too much power for the flimsy {PlayersInputSpell} that {playerName} just casted!");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);

        }
        //DARK
        if (PlayersInputSpell == SpellCasted[2] && GaryInputSpell == SpellCasted[1]) // Player cast dark Gary cast light Gary deals 2x 
        {
            GarysHealth -= MagicalDamage / 2;
            PlayerHealth -= MagicalDamage * 2;
            Console.WriteLine($"GARY THE WIZARDS {GaryInputSpell} has too much power for the flimsy  {PlayersInputSpell}  that {playerName} just casted!");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);

        }
        if (PlayersInputSpell == SpellCasted[2] && GaryInputSpell == SpellCasted[2]) // Both cast same
        {
            GarysHealth -= MagicalDamage;
            PlayerHealth -= MagicalDamage;
            Console.WriteLine($"It seems that GARY THE WIZARD and {playerName} are matched in skill and power");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);

        }
        if (PlayersInputSpell == SpellCasted[2] && GaryInputSpell == SpellCasted[0])  // Player cast dark Gary cast neutral Player deals 2x
        {
            PlayerHealth -= MagicalDamage / 2;
            GarysHealth -= MagicalDamage * 2;
            Console.WriteLine($"{playerName} spell beats GARY THE WIZARDS {GaryInputSpell}!");
            Console.WriteLine("Player Health: " + PlayerHealth);
            Console.WriteLine("GARY THE WIZARD Health: " + GarysHealth);
        }
        Console.WriteLine("Wizards prepare your next spells GO!");
        Console.WriteLine("[Press enter to Start the next round]");
        Console.ReadKey();
        Console.Clear();

       


    }




   





}
