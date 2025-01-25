namespace FileCatagorizer;

class Program
{
    static void Main(string[] args)
    {
        //// Source directory containing the files
        //string sourceDirectory = @"C:\Users\ahmed\Downloads\old Downloads";

        //// Destination directory where files will be moved
        //string destinationDirectory = @"C:\Users\ahmed\Downloads\Catagorized old Downloads";

        //// Create subdirectories in the destination directory if they don't exist
        //Directory.CreateDirectory(destinationDirectory);
        //Directory.CreateDirectory(Path.Combine(destinationDirectory, "Zip"));
        //Directory.CreateDirectory(Path.Combine(destinationDirectory, "Pictures"));
        //Directory.CreateDirectory(Path.Combine(destinationDirectory, "Videos"));
        //Directory.CreateDirectory(Path.Combine(destinationDirectory, "Documents"));
        //Directory.CreateDirectory(Path.Combine(destinationDirectory, "Others"));

        //// Move files to corresponding subfolders based on their extensions
        //CategorizeAndMoveFiles(sourceDirectory, destinationDirectory);

        Console.WriteLine("Files moved successfully!");
    }

    static void CategorizeAndMoveFiles(string sourceDir, string destinationDir)
    {
        // Get all files in the source directory and its subdirectories
        string[] files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            try
            {
                // Get the file extension
                string extension = Path.GetExtension(file).ToLower();

                // Define the destination directory based on file extension
                string destDir;
                switch (extension)
                {
                    case ".zip":
                        destDir = Path.Combine(destinationDir, "Zip");
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                    case ".bmp":
                        destDir = Path.Combine(destinationDir, "Pictures");
                        break;
                    case ".mp4":
                    case ".avi":
                    case ".mov":
                    case ".wmv":
                        destDir = Path.Combine(destinationDir, "Videos");
                        break;
                    case ".doc":
                    case ".docx":
                    case ".pdf":
                    case ".txt":
                        destDir = Path.Combine(destinationDir, "Documents");
                        break;
                    default:
                        destDir = Path.Combine(destinationDir, "Others");
                        break;
                }

                // Move the file to the destination directory
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destDir, fileName);
                File.Move(file, destFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file: {ex.Message}");
            }
        }
    }
}
