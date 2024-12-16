using NAudio.Utils;
using NAudio.Wave;

namespace AudioLib
{
    public class AudioRecorder
    {
        private readonly WaveFormat waveFormat = new WaveFormat(16000, 1);

        private WaveInEvent waveIn; // Used to capture audio from the microphone
        private MemoryStream? memoryStream; // Memory stream to hold the recorded audio
        private WaveFileWriter? writer; // Writer to write the audio data to the memory stream

        public AudioRecorder()
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = 0, // Default microphone
                WaveFormat = waveFormat
            };

            waveIn.DataAvailable += OnDataAvailable;
        }

        // Start recording
        public void StartRecording()
        {
            this.RefreshStream();
            writer = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), waveIn.WaveFormat);
            waveIn.StartRecording();
        }

        // Stop recording and close resources
        public void StopRecording()
        {
            waveIn.StopRecording();
            writer.Close();
        }

        // This event is fired when data is available to be written to the memory stream
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        // Get the recorded audio from the memory stream
        public MemoryStream GetAudioStream()
        {
            memoryStream.Position = 0;
            return memoryStream;
        }

        public void SaveMemoryStreamToWav(string outputPath)
        {
            // Make sure the MemoryStream position is at the beginning
            memoryStream.Position = 0;

            // Create a WaveFileWriter to write the audio to the output file
            using (var waveFileWriter = new WaveFileWriter(outputPath, waveFormat))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveFileWriter.Write(buffer, 0, bytesRead);
                }
            }

            this.RefreshStream();
        }

        private void RefreshStream()
        {
            if (memoryStream != null)
            {
                memoryStream.Dispose();
            }

            memoryStream = new MemoryStream();
        }
    }
}
