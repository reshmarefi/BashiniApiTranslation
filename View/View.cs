using System;

namespace TextTranslationApp.View
{
    public static class View
    {
        public const string MainMenu = @"
Welcome to the Translation App

Choose an option:
1. Translation
2. Translated Speech
";

        public const string InputLanguagePrompt = @"
Enter the language of the input text: ";
        public const string TranslationOutputLanguagePrompt = @"
Enter the language of the output text: ";

        public const string SpeechOutputLanguagePrompt = @"
Enter the desired speech language (Marathi, Hindi, Telugu, Bengali, Tamil, Kannada, Malayalam, English): ";

        // Add more constants as needed
    }
}
