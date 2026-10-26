using System.Globalization;

namespace ProjectEulerPractice;

internal static class ProblemGenerator
{
    public static int CreateNext()
    {
        string? projectDirectory = FindProjectDirectory();
        if (projectDirectory is null)
        {
            Console.Error.WriteLine("Could not find ProjectEulerPractice.csproj beside this build.");
            return 1;
        }

        string templatePath = Path.Combine(projectDirectory, "Templates", "ProblemTemplate.cs.txt");
        string problemsDirectory = Path.Combine(projectDirectory, "Problems");

        try
        {
            if (!File.Exists(templatePath))
            {
                Console.Error.WriteLine($"Template not found: {templatePath}");
                return 1;
            }

            Directory.CreateDirectory(problemsDirectory);

            // Read filenames so gaps in numbering do not cause existing work to be reused.
            int highestNumber = 0;
            foreach (string file in Directory.EnumerateFiles(problemsDirectory, "Problem*.cs"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string suffix = fileName.Substring("Problem".Length);
                if (int.TryParse(suffix, NumberStyles.None, CultureInfo.InvariantCulture, out int number))
                {
                    highestNumber = Math.Max(highestNumber, number);
                }
            }

            if (highestNumber == int.MaxValue)
            {
                Console.Error.WriteLine("Cannot create another problem: the highest number is already int.MaxValue.");
                return 1;
            }

            int nextNumber = highestNumber + 1;
            string numberText = nextNumber.ToString(CultureInfo.InvariantCulture);
            string paddedNumber = nextNumber.ToString("D3", CultureInfo.InvariantCulture);
            string fileNameToCreate = $"Problem{paddedNumber}.cs";
            string destination = Path.Combine(problemsDirectory, fileNameToCreate);

            string contents = File.ReadAllText(templatePath)
                .Replace("{{ProblemNumber}}", numberText)
                .Replace("{{PaddedProblemNumber}}", paddedNumber);

            // CreateNew fails if the file exists, protecting existing solutions.
            using (var writer = new StreamWriter(File.Open(destination, FileMode.CreateNew)))
            {
                writer.Write(contents);
            }

            Console.WriteLine($"Created Problems/{fileNameToCreate}.");
            Console.WriteLine("Paste the problem into the top comment and write your solution in Solve().");
            Console.WriteLine($"Run it with: dotnet run -- {numberText}");
            return 0;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Could not create a problem file: {exception.Message}");
            return 1;
        }
    }

    private static string? FindProjectDirectory()
    {
        // Start at the executable so Rider's working directory does not affect the destination.
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory);
             directory is not null;
             directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "ProjectEulerPractice.csproj")))
            {
                return directory.FullName;
            }
        }

        return null;
    }
}
