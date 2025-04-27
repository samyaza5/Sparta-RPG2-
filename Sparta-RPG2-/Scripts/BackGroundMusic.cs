using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace Sparta_RPG2_
{
    class BackGroundMusic
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFile;

        public void Play()
        {
            try
            {
                wavePlayer = new WaveOutEvent();
                // ✨ 실행 파일 기준으로 절대경로 계산
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Sounds\background.wav");

                audioFile = new AudioFileReader(path);
                wavePlayer.Init(audioFile);
                wavePlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ 배경음 재생 실패: {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                wavePlayer?.Stop();
                audioFile?.Dispose();
                wavePlayer?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ 배경음 정지 실패: {ex.Message}");
            }
        }
    }
}
