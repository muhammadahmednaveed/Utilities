using System;
using System.Diagnostics;
using System.IO;
using OpenCvSharp;

namespace VideoCompressor;
class VideoCompressor
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the input folder path (press Enter to use the application's base directory):");
        string inputFolder = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputFolder))
        {
            inputFolder = AppDomain.CurrentDomain.BaseDirectory;
        }

        Console.WriteLine("Enter the output folder path (press Enter to create 'OutputFolder' in the input directory):");
        string outputFolder = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(outputFolder))
        {
            outputFolder = Path.Combine(inputFolder, "OutputFolder");
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string[] videoFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (var file in videoFiles)
        {
            string extension = Path.GetExtension(file).ToLower();
            if (extension == ".mp4" || extension == ".mov" || extension == ".avi" || extension == ".mkv")
            {
                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + "_compressed.mp4");
                CompressVideoByFFMPeg(file, outputFile);
            }
        }

        Console.WriteLine("Compression complete.");
    }

    static void CompressVideoByFFMPeg(string inputFile, string outputFile)
    {
        try
        {
            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = "ffmpeg"; // Make sure FFmpeg is installed and added to your PATH
            ffmpeg.StartInfo.Arguments = $"-i \"{inputFile}\" -vcodec libx264 -crf 23 \"{outputFile}\"";
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.StartInfo.RedirectStandardOutput = true;
            ffmpeg.StartInfo.RedirectStandardError = true;
            ffmpeg.StartInfo.CreateNoWindow = true;

            ffmpeg.Start();

            string output = ffmpeg.StandardOutput.ReadToEnd();
            string error = ffmpeg.StandardError.ReadToEnd();

            ffmpeg.WaitForExit();

            if (ffmpeg.ExitCode != 0)
            {
                Console.WriteLine($"Error compressing {Path.GetFileName(inputFile)}: {error}");
            }
            else
            {
                Console.WriteLine($"Successfully compressed {Path.GetFileName(inputFile)} to {outputFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    static void CompressVideoByOpenCvSharp(string inputFile, string outputFile)
    {
        try
        {
            using var capture = new VideoCapture(inputFile);
            int fourcc = VideoWriter.FourCC('X', '2', '6', '4'); // H.264 codec
            double fps = capture.Fps;
            int frameWidth = capture.FrameWidth;
            int frameHeight = capture.FrameHeight;

            using var writer = new VideoWriter(outputFile, fourcc, fps, new OpenCvSharp.Size(frameWidth, frameHeight));

            if (!writer.IsOpened())
            {
                Console.WriteLine($"Failed to open writer for {outputFile}");
                return;
            }

            using var frame = new Mat();
            while (capture.Read(frame))
            {
                if (frame.Empty())
                    break;
                writer.Write(frame);
            }

            Console.WriteLine($"Successfully compressed {inputFile} to {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}