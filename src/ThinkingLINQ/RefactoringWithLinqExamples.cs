using System;
using System.Collections.Generic;
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
    }
}
