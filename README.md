# Project Euler practice in C#

One console app, one C# file per Project Euler problem. Install the .NET 10 SDK to get started. No extra packages are needed.

This repository contains the runner and a blank template. Your problem statements, notes, and solutions live in `Problems/`, which is ignored by Git so your practice stays local.

## Start your first problem

Clone or fork this repository, open the project in Rider or your preferred editor, and open a terminal in the project folder. Generate your first blank problem:

```sh
dotnet run -- create:problem
```

This creates `Problems/Problem001.cs`. Open that file, paste the problem statement into the comment at the top, and write your code inside `Solve()`.

Replace `throw new NotImplementedException();` with your solution. When you have an answer, return it as a string using `return answer.ToString();` (where `answer` is your result variable).

From a terminal in this project folder, run:

```sh
dotnet run -- 1
```

The app prints your answer and how long your solution took. Check the answer on Project Euler; the app does not store correct answers or verify them automatically. The repository contains no solutions or spoilers.

Running an unfinished problem prints a reminder to add your code. Once implemented, you can run it again after every change. `dotnet run` compiles the code for you.

## Add the next day's problem

1. Generate the next problem file from the template:

   ```sh
   dotnet run -- create:problem
   ```

   If `Problem001.cs` is your highest-numbered file, this creates `Problems/Problem002.cs` with the class name and `Number` already filled in. Each run chooses one above the highest existing `Problem<number>.cs` filename. Existing files are never overwritten.

2. Open the generated file, paste the problem statement into the top comment, and write your solution in `Solve()`.
3. Run it:

   ```sh
   dotnet run -- 2
   ```

Run the same creation command each day. You can also copy the template manually to work on a specific problem: replace `{{ProblemNumber}}` with its number and `{{PaddedProblemNumber}}` with its number padded to at least three digits. The runner automatically finds classes that implement `IProblem`, and .NET includes new `.cs` files without a project-file edit. Each problem must have its own class name and `Number`.

All C# files compile together. If an unfinished file has a syntax error, fix it or comment out the incomplete code before running any command, including file creation. Leaving its original `throw new NotImplementedException();` in place is fine.

## Keep your practice local

The entire `Problems/` directory is excluded by `.gitignore`. Generated files still compile and run locally, but ordinary Git commits and pushes do not include them. Keep solution notes and scratch work in that directory too, and avoid force-adding it to Git.

Ignored files are not backed up to GitHub. Make a separate private backup if you want to preserve your work across machines.

## Useful commands

```sh
dotnet run                   # Run problem 1
dotnet run -- 2              # Run problem 2
dotnet run -- --list         # List problem files, including unfinished ones
dotnet run -- create:problem # Generate the next problem file
dotnet run -- --help         # Show usage
dotnet build                 # Check that everything compiles
dotnet run -c Release -- 2    # Run an optimized build for timing
```

## A few notes coming from Node

- Use `Console.WriteLine(value)` like `console.log(value)` while working.
- `int` is a 32-bit integer; `long` is a 64-bit integer. Euler calculations can exceed `int`, so choose numeric types deliberately. A literal such as `1L` has type `long`.
- Do your calculation with numbers, then call `.ToString()` on the final answer. The string return type also supports larger answers calculated with `System.Numerics.BigInteger`.
- `IProblem` describes what every problem provides: a number and a `Solve()` method. You can leave `Program.cs` and `IProblem.cs` alone while practicing.

## Files

- `Problems/`: your local problem files; generated as needed and ignored by Git.
- `Templates/ProblemTemplate.cs.txt`: used to generate each new problem. Its `.txt` extension keeps it out of compilation.
- `ProblemGenerator.cs`: copies the template and assigns the next problem number.
- `IProblem.cs`: the shared interface.
- `Program.cs`: problem selection, execution, and timing.
- `ProjectEulerPractice.csproj`: the .NET console project configuration.
