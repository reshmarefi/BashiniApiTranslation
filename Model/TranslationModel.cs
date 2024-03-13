using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TextTranslationApp.Model
{
    public class TranslationModel
    {
        public async Task<string> TranslateText(string text, string sourceLanguage, string targetLanguage)
        {
            // API endpoint for translation
            string apiUrl = "https://tts.bhashini.ai/v1/translate";

            // Create HTTP client
            using (HttpClient client = new HttpClient())
            {
                // Prepare request data
                var requestData = new
                {
                    inputText = text,
                    inputLanguage = sourceLanguage,
                    outputLanguage = targetLanguage
                };

                // Convert request data to JSON
                string jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                // Send POST request to API
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Return translated text
                    return jsonResponse;
                }
                else
                {
                    // If request fails, throw exception with error message
                    throw new Exception($"Translation API request failed: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }

        public async Task SynthesizeSpeech(string text, string language)
        {
            if (language != "Hindi" && language != "Bengali" && language != "Telugu" && language != "Marathi" && language != "Tamil" && language != "Kannada" && language != "Malayalam" && language != "English")
            {
                Console.WriteLine("Sorry ! Language is not available for speech synthesis.");
                return;
            }

            // Mapping of language names to language IDs
            Dictionary<string, string> languageIds = new Dictionary<string, string>
            {
                { "Hindi", "hi" },
                { "Bengali", "bn" },
                { "Telugu", "te" },
                { "Marathi", "mr" },
                { "Tamil", "ta" },
                { "Kannada", "kn" },
                { "Malayalam", "ml" },
                {"English","en" }
            };

            // Check if the provided language name exists in the mapping
            if (languageIds.ContainsKey(language))
            {
                string languageId = languageIds[language];

                // API endpoint for speech synthesis
                string apiUrl = "https://tts.bhashini.ai/v1/synthesize";

                // Create HTTP client
                using (HttpClient client = new HttpClient())
                {
                    // Prepare request data
                    var requestData = new
                    {
                        text = text,
                        languageId = languageId,
                        voiceId = 1 // Assuming voiceId 0 represents a default voice
                    };

                    // Convert request data to JSON
                    string jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

                    // Construct the request body
                    var requestBody = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                    // Send POST request to API
                    HttpResponseMessage response = await client.PostAsync(apiUrl, requestBody);

                    // Check if request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response content as audio file
                        byte[] audioData = await response.Content.ReadAsByteArrayAsync();

                        // Save audio file
                        File.WriteAllBytes("output.mp3", audioData);

                        Console.WriteLine("Speech synthesized. Audio file saved to output.mp3");
                    }
                    else
                    {
                        // If request fails, throw exception with error message
                        throw new Exception($"Speech synthesis API request failed: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            else
            {
                // If provided language name is not found in the mapping, throw an exception
                throw new ArgumentException("Invalid language name. Please provide a valid language name from the list.");
            }
        }
    }
}
