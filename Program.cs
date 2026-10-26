using System.Diagnostics;
using ProjectEulerPractice;

if (args.Length > 0 && args[0] == "create:problem")
{
    if (args.Length != 1)
    {
        Console.Error.WriteLine("Usage: dotnet run -- create:problem");
        return 1;
    }

    return ProblemGenerator.CreateNext();
}

// Find problem classes automatically so new files need no runner changes.
var problems = typeof(IProblem).Assembly.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && typeof(IProblem).IsAssignableFrom(type))
    .Select(type => (IProblem)Activator.CreateInstance(type)!)
    .OrderBy(problem => problem.Number)
    .ToArray();

if (args.Length == 1 && args[0] is "--help" or "-h")
{
    Console.WriteLine("Run a problem:  dotnet run -- <number>");
    Console.WriteLine("List problems:  dotnet run -- --list");
    Console.WriteLine("Add a problem:  dotnet run -- create:problem");
    Console.WriteLine("With no arguments, problem 1 runs.");
    return 0;
}

if (args.Length == 1 && args[0] is "--list" or "list")
{
    Console.WriteLine("Available problems:");
    foreach (var problem in problems)
    {
        Console.WriteLine($"  {problem.Number:000}");
    }
    return 0;
}

int problemNumber = 1;
if (args.Length > 1 ||
    (args.Length == 1 && (!int.TryParse(args[0], out problemNumber) || problemNumber < 1)))
{
    Console.Error.WriteLine("Choose a positive problem number, for example: dotnet run -- 1");
    return 1;
}

var matches = problems.Where(problem => problem.Number == problemNumber).ToArray();
if (matches.Length == 0)
{
    Console.Error.WriteLine($"Problem {problemNumber:000} does not have a file yet.");
    Console.Error.WriteLine("Create your next problem with: dotnet run -- create:problem");
    Console.Error.WriteLine("Run dotnet run -- --list to see available problems.");
    return 1;
}

if (matches.Length > 1)
{
    Console.Error.WriteLine($"More than one class uses problem number {problemNumber}.");
    Console.Error.WriteLine("Give each problem a unique Number property.");
    return 1;
}

Console.WriteLine($"Problem {problemNumber:000}");
var stopwatch = Stopwatch.StartNew();

try
{
    string answer = matches[0].Solve();
    stopwatch.Stop();
    Console.WriteLine($"Answer: {answer}");
    Console.WriteLine($"Elapsed: {stopwatch.Elapsed.TotalMilliseconds:F2} ms");
    return 0;
}
catch (NotImplementedException)
{
    Console.WriteLine($"Ready for your solution in Problems/Problem{problemNumber:000}.cs.");
    Console.WriteLine("Replace the throw statement in Solve() with your code.");
    return 0;
}
