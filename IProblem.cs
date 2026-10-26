namespace ProjectEulerPractice;

// This interface gives the runner a common way to run any problem.
public interface IProblem
{
    int Number { get; }
    string Solve();
}
