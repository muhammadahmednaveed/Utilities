using System;
using System.Drawing;
using System.IO;
using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        string imagesDirectory = @"";
        string outputFilePath = @"";

        ExtractTextFromImages(imagesDirectory, outputFilePath);
    }

    static void ExtractTextFromImages(string imagesDirectory, string outputFilePath)
    {
        // Ensure Tesseract language data folder is set up correctly
        string tessDataPath = @"C:\Tesseract";

        // Initialize Tesseract OCR engine
        using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var imagePath in Directory.GetFiles(imagesDirectory, "*.jpeg"))
                {
                    Console.WriteLine($"Processing: {imagePath}");

                    try
                    {
                        // Load image
                        using (var img = Pix.LoadFromFile(imagePath))
                        {
                            // Perform OCR on the image
                            using (var page = engine.Process(img))
                            {
                                string text = page.GetText();
                                Console.WriteLine($"Extracted Text:\n{text}");
                                writer.WriteLine($"Text from {Path.GetFileName(imagePath)}:\n{text}\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine($"Text extraction complete! Results saved to {outputFilePath}");
    }
}