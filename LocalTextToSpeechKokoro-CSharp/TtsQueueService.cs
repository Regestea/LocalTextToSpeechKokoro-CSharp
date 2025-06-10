using System;
using System.Collections.Generic;
using KokoroSharp;
using KokoroSharp.Core;

namespace LocalTextToSpeechKokoro_CSharp
{
    public class TtsQueueService
    {
        private readonly KokoroTTS _tts;
        private readonly KokoroVoice _voice;
        private readonly Queue<string> _queue = new();
        private bool _isSpeaking;

        public TtsQueueService(KokoroTTS tts, KokoroVoice voice)
        {
            _tts = tts;
            _voice = voice;
        }

        public void Enqueue(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            _queue.Enqueue(text);
            if (!_isSpeaking)
                SpeakNext();
        }

        public void EnqueueRange(IEnumerable<string> texts)
        {
            foreach (var text in texts)
                Enqueue(text);
        }

        private void SpeakNext()
        {
            if (_queue.TryDequeue(out var sentence))
            {
                _isSpeaking = true;
                var speech = _tts.Speak(sentence, _voice);
                speech.OnSpeechCompleted += _ => SpeakNext();
            }
            else
            {
                _isSpeaking = false;
            }
        }
    }
}

