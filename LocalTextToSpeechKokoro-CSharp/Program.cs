using KokoroSharp;
using KokoroSharp.Core;

namespace LocalTextToSpeechKokoro_CSharp;

class Program
{
    static void Main(string[] args)
    {
        KokoroTTS tts = KokoroTTS.LoadModel();
        KokoroVoice heartVoice = KokoroVoiceManager.GetVoice("af_heart");
        var ttsQueue = new TtsQueueService(tts, heartVoice);
        ttsQueue.EnqueueRange(new[]
        {
            "just smell of brass polish and old wood;",
            "it smelled of time itself. "
        });

        foreach (var input in ReadInputs())
        {
            ttsQueue.Enqueue(input);
        }
    }

    static IEnumerable<string> ReadInputs()
    {
        string input;
        while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            yield return input;
    }
}