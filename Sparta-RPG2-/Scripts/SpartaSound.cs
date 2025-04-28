using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    class SpartaSound
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFile;

        public void Play()
        {
            try
            {
                wavePlayer = new WaveOutEvent();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Sounds\SpartaSound.mp3");
                audioFile = new AudioFileReader(path);
                audioFile.Volume = 0.3f; // 🔥 볼륨 조절 추가 (예: 30% 음량)
                wavePlayer.Init(audioFile);
                wavePlayer.Play();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"⚠️ SpartaSound 재생 실패: {ex.Message}");
            }
        }
    }
}
