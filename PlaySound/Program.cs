using NAudio.Wave;

if (args.Length == 0)
{
    Console.WriteLine("Usage: mp3player.exe <path_to_mp3>");
    return;
}

string mp3Path = args[0];

if (!File.Exists(mp3Path))
{
    Console.WriteLine("Error: File not found!");
    return;
}

try
{
    using var mp3Reader = new Mp3FileReader(mp3Path);
    using var outputDevice = new WaveOutEvent();

    outputDevice.Init(mp3Reader);
    outputDevice.Play();
    while (outputDevice.PlaybackState == PlaybackState.Playing)
    {
        Thread.Sleep(500); // Avoids busy-waiting
    }
    outputDevice.Stop();
}
catch (Exception ex)
{
    Console.WriteLine($"Error playing file: {ex.Message}");
}