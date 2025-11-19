using System;
using Vosk;
using NAudio.Wave;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

public class VoiceToTextProtocol//<-- Class that handles the voice to text protocol
{
    private Model model;// <-- Vosk model for speech recognition
    private VoskRecognizer recognizer;//<-- Vosk recognizer for processing audio input

    public void VoiceToTextProtocolCreator(string modelPath) //<-- Method to build or construct the voice to text protocol
    {
        model = new Model(modelPath); 
        recognizer = new VoskRecognizer(model, 16000.0f);
    }

    public string ReadPlayersInput() //<-- Method to read a single players input and register it as text
    {
        string PlayerInput = "";
        string ConfirmationStatement = "Enter"; //Make it so once you say enter the Input is confirmed

        using (var waveIn = new WaveInEvent()) //<-- Using NAudio event system to capture audio from microphone
        {
            waveIn.DeviceNumber = 0; //<-- use the default microphone
            waveIn.WaveFormat = new WaveFormat(16000, 1); // <-- define the wave format and mono channel
            waveIn.BufferMilliseconds = 800; // <-- create a 0.8 second buffer for better procesing

            waveIn.DataAvailable += (sender, e) =>  //Event handler for when data is incoming to the mic
            {
                if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded)) // <-- Process the data to see if its understandable as speech
                {
                    string json = recognizer.Result(); // <-- Get the result in JSON format
                    var PlayerResponse = JsonSerializer.Deserialize<VoskResult>(json); // <-- Deserialize the JSON to get the text
                    Console.WriteLine("Did you say?: " + PlayerResponse?.text); // <-- Print the recognized text to the console
                    PlayerInput = PlayerResponse?.text;
                }
            };
            Console.WriteLine("[Speak your incantation]"); //<-- Prompt the user to speak
            waveIn.StartRecording(); //<-- starts recording audio from the mic
            Console.WriteLine("[Press any button to confirm your incantation]");
            Console.ReadKey(); //<-- waits for a key press to listen again
            waveIn.StopRecording();//<-- stops recording audio
        }

        return PlayerInput; //<-- returns the players input as text to be used in the main program

    }


}

/* This was built using the following resources:
1. This youtube video https://youtu.be/VIBSQhNxB_U
2. Vosk API documentation https://alphacephei.com/vosk/install
3. Here for the Json deserialization https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer.deserialize?view=net-7.0
*/