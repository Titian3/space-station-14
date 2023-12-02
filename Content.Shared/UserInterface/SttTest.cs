using System.Globalization;
using System.Speech.Recognition;

namespace Content.Shared.UserInterface
{
    public class SpeechRecognitionWrapper
    {
        [Dependency] private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();

        public event Action<string>? SpeechRecognized;

        public SpeechRecognitionWrapper()
        {
            InitializeRecognizer();
        }

        private void InitializeRecognizer()
        {
            CultureInfo culture = new CultureInfo("en-US");
            _recognizer = new SpeechRecognitionEngine(culture);

            _recognizer.SetInputToDefaultAudioDevice();

            Grammar dictationGrammar = new DictationGrammar();
            _recognizer.LoadGrammar(dictationGrammar);

            // Configure the recognizer (event handlers, etc.)
            _recognizer.SpeechRecognized += OnRecognizerSpeechRecognized;
        }

        private void OnRecognizerSpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            SpeechRecognized?.Invoke(e.Result.Text);
        }

        public void StartRecognition()
        {
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StopRecognition()
        {
            _recognizer.RecognizeAsyncStop();
        }

        // Dispose pattern if necessary
    }
}
