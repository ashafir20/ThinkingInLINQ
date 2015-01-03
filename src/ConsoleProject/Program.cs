using System;
using ThinkingLINQ;

namespace ConsoleProject
{
    public class Program
    {
        public void Main(string[] args)
        {
            var target = new RefactoringWithLinqExamples();
            target.AsParallel();
        }
    }
}
