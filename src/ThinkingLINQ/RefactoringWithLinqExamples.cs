using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ThinkingLINQ
{
    //Refactoring with LINQ - Chapter 4
    //4-1. Replacing Loops by Using LINQ Operators

    /*Several looping constructs appear more often than others in code.Replacing these repeating looping constructs
    with standard LINQ operators usually results in shorter, more-intuitive code.This section shows how you can replace
    traditional looping constructs (which sometimes can become ugly quickly) with simpler, smaller, and intuitive LINQ
    queries.The biggest advantage of using LINQ over looping constructs is that you get to move the code one step closer
    to the concept.For example, consider the sentence “Check whether any element in the collection matches a given
    condition.” A looping construct doesn’t visually reflect the intent of that sentence. But LINQ operators and LINQ
    queries do.
    The recipes in this chapter show looping constructs and the equivalent code using LINQ operators side by side.
    Each section begins with a LINQ query operator that you can use to simplify the code, followed by the problem
    statement and a side-by-side comparison of the loop-based and LINQ-based approaches.*/

    public class RefactoringWithLinqExamples
    {
        //4-2. The Any Operator
        public void AnyLinq()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            bool isAny = nums.Any(n => n >= 150);
        }

        //4-3. The All Operator
        public void AllLinq()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            bool isAll = nums.All(n => n < 150);
        }


        //4-4. The Take Operator
        public void takeLinq()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            int[] first4 = new int[4];
            first4 = nums.Take(4).ToArray();
        }

        //4-5. The Skip Operator
        public void SkipLINQ()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            int[] skip4 = new int[nums.Length - 4];
            skip4 = nums.Skip(4).ToArray();
        }

        //4-6. The TakeWhile Operator
        //The TakeWhile operator enables you to take elements from a collection as long as a given condition is true.
        public void TakeWhileLinq()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            List<int> until50 = new List<int>();
            until50 = nums.TakeWhile(n => n < 50).ToList();
        }

        //4-7. The SkipWhile Operator
        //SkipWhile skips elements as long as a given condition is true. As soon as the condition becomes false, the operator
        //starts picking values.
        public void SkipWhileLinq()
        {
            int[] nums = { 14, 21, 24, 51, 131, 1, 11, 54 };
            List<int> skipWhileDivisibleBy7 =
            nums.SkipWhile(n => n % 7 == 0).ToList();
        }

        //The Of Type Operator
        //The OfType operator finds elements of only the given type from a collection that has elements of several types.
        public void OfType()
        {
            object[] things = {"Sam", 1, DateTime.Today, "Eric" };
            var stringOnlyValues = things.OfType<string>();
        }

        //4-18. The Cast Operator
       //Safe casting isn’t hard and shouldn’t hurt.The Cast<T>() operator can cast any loosely typed collection to a strongly
       //typed collection of the given type T.

        public void Cast()
        {
            object[] things = {"Sam","Dave","Greg","Travis", "Dan",2};
            var strThings = things.Select(t => t as string)
            .Where(t => t != null)
            .Cast<string>();
        }

        //4-19. The Aggregate Operator
        //The Aggregate operator joins the elements of a given collection by using a provided lambda function.

        public void Aggregate()
        {
            string[] names = { "Greg", "Travis", "Dan" };
            names.Aggregate((f, s) => f + "," + s);
        }

        //4-20. Replacing Nested Loops

/*      The SelectMany Operator
        If we want to print all the characters of all the words for each word in a given array, we can use nested loops or we can
        replace nested loops with SelectMany(), as shown next.Although this example is trivial, it is deliberately chosen so
        that you can relate it to one of your own one - to - many situations.You can use this operator to flatten your dictionarylike
        collections.
*/
        public void SelectMany()
        {
            string[] words = { "dog", "elephant", "fox", "bear"};
            List<char> allChars = new List<char>();
            foreach (string word in words)
            {
                allChars.AddRange(word.ToCharArray());
            }

            words.SelectMany(w => w.ToCharArray());
        }

        //Removing Nested Loops by Using SelectMany

        public void NestedLoop()
        {
            List<int> fromLoop = new List<int>();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    fromLoop.Add(i + j);

            int[] initialValues = Enumerable.Range(0, 10).ToArray();
            List<int> fromLINQ = Enumerable.Range(0, 10)
                                    .SelectMany(e => initialValues.Select(v => v + e)).ToList();

            //Finally check whether you have the same values or not.
            var flag = fromLoop.SequenceEqual(fromLINQ);
        }

        //Replacing If-Else Blocks Inside a Loop
        public void ReplacingIfElseBlocksInsideALoop()
        {
            var someThings = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    someThings.Insert(0, i);
                }
                else if ((2 * i + 1) % 2 == 0)
                {
                    someThings.Add(i);
                }
                else //everything else falls here
                {
                    someThings.Add(i);
                    someThings.Add(i + 1);
                }
            }

            someThings = new List<int>();
            Enumerable.Range(0, 4).Where(i => i % 2 == 0).ToList().ForEach(a => someThings.Insert(0, a));
            Enumerable.Range(0, 4).Where(i => (2 * i + 1) % 2 == 0).ToList().ForEach(a => someThings.Add(a));
            Enumerable.Range(0, 4).Where(i => (2 * i + 1) % 2 != 0 && i % 2 != 0).ToList().ForEach(a =>
                        someThings.AddRange(new int[] { a, a + 1 }));
        }

        //4-21. Running Code in Parallel Using AsParallel( ) and AsOrdered() Operators

        /*Making use of all your computing power is simple with LINQ. By using the AsParallel() operator, you can
        “automagically” make sure that your code runs faster. But be warned, plugging in AsParallel() doesn’t always
        guarantee faster execution time. Sometimes it might take longer to distribute the task to multiple processors, and
        it can take a longer time running the code in parallel than in sequential mode. AsParallel() splits the input data
        to multiple groups so the order of the elements in the input doesn’t remain intact. If you care about the order of the
        elements in the result, plug in AsOrdered() right after the AsParallel() call*/

        //finds all the prime numbers from 1 to 10,000 — fast.
        public void AsParallel()
        {
            Stopwatch w = new Stopwatch();
            w.Start();

            List<int> Qs = new List<int>();
            List<int> Qsp = new List<int>();
            List<int> Qsp1 = new List<int>();

            for (int i = 0; i < 2; i++)
                Qs = Enumerable.Range(1, 10000)
                    .Where(d => Enumerable.Range(2, d / 2).All(e => d % e != 0)).ToList();

            w.Stop();

            double timeWithoutParallelization = w.Elapsed.TotalMilliseconds;
            Stopwatch w2 = new Stopwatch();
            w2.Start();

            for (int i = 0; i < 2; i++)
                Qsp = Enumerable.Range(1, 10000).AsParallel().Where(d =>
                    Enumerable.Range(2, d / 2).All(e => d % e != 0)).ToList();

            w2.Stop();

            double timeWithParallelization = w2.Elapsed.TotalMilliseconds;
            double percentageGainInPerformance = 
                (timeWithoutParallelization - timeWithParallelization) / timeWithoutParallelization;

            bool isSame = Qs.SequenceEqual(Qsp); //false cause AsParallel does not save the order of the elements

            for (int i = 0; i < 2; i++)
                Qsp1 = Enumerable.Range(1, 10000).AsParallel().AsOrdered().Where(d =>
                    Enumerable.Range(2, d / 2).All(e => d % e != 0)).ToList();

            isSame = Qs.SequenceEqual(Qsp1); //true cause AsOrdered will save the order of the elements
        }
    }
}
