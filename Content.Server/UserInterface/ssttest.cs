using System.Speech.Recognition;
namespace Content.Server.UserInterface;


public sealed class SpeechRecognitionWrapper
{
    [Dependency] private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();

    public event Action<string>? SpeechRecognized;

    public SpeechRecognitionWrapper()
    {
        InitializeRecognizer();
    }

    private void InitializeRecognizer()
    {
        _recognizer = new SpeechRecognitionEngine();
        // Configure the recognizer (load grammar, etc.)
        _recognizer.SpeechRecognized += OnRecognizerSpeechRecognized;
        // Other setup...
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
