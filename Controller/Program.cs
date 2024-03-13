using System;
using System.IO;
using TextTranslationApp.Model;
using TextTranslationApp.View;

namespace TranslationUsingBashiniApi.Controller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TranslationModel translationModel = new TranslationModel();

            // Path to the input file
            string inputFilePath = @"input.txt";

            // Read input text from file
            string inputText = File.ReadAllText(inputFilePath);

            

            // Get user's choice
            Console.WriteLine(View.MainMenu);
            int choice = int.Parse(Console.ReadLine());

            // Get input text language from user
            Console.WriteLine(View.InputLanguagePrompt);
            string inputLanguage = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine(View.TranslationOutputLanguagePrompt);
                    string outputLanguage = Console.ReadLine();

                    // Call translation API
                    string translatedText = await translationModel.TranslateText(inputText, inputLanguage, outputLanguage);

                    // Write translated text to output file
                    File.WriteAllText("output.txt", translatedText);

                    Console.WriteLine("Translation completed. Translated text saved to output.txt");
                    break;

                case 2:
                    Console.WriteLine(View.SpeechOutputLanguagePrompt);
                    string outputLanguageForSpeech = Console.ReadLine();

                    // Call translation API
                    string translatedTextForSpeech = await translationModel.TranslateText(inputText, inputLanguage, outputLanguageForSpeech);

                    // Write translated text to output file
                    File.WriteAllText("output.txt", translatedTextForSpeech);

                    Console.WriteLine("Translation completed. Translated text saved to output.txt");

                    // Synthesize speech from translated text
                    await translationModel.SynthesizeSpeech(translatedTextForSpeech, outputLanguageForSpeech);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select either 1 or 2.");
                    break;
            }
        }
    }
}
