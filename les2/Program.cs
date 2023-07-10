using System;

namespace les2
{
    public static class Arry1
    {
        public static IEnumerable<IEnumerable<T>> GenerateCombinations<T>(this IEnumerable<T> source, int k, IEqualityComparer<T> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (k < 0)
                throw new ArgumentOutOfRangeException(nameof(k), "k должно быть положительным");

            var elements = source.ToList();
            if (k > elements.Count)
                throw new ArgumentException("k не может быть больше чем количество элементов");

            return GenerateCombinationsRecursive(elements, k, comparer);
        }

        private static IEnumerable<IEnumerable<T>> GenerateCombinationsRecursive<T>(List<T> elements, int k, IEqualityComparer<T> comparer)
        {
            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            for (int i = 0; i < elements.Count; i++)
            {
                var current = elements[i];
                var remaining = elements.Skip(i).ToList();

                foreach (var combination in GenerateCombinationsRecursive(remaining, k - 1, comparer))
                {
                    yield return new[] { current }.Concat(combination);
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> GenerateSubsets<T>(this IEnumerable<T> source)
        {
            var elements = source.ToList();
            var subsets = new List<IEnumerable<T>>();

            for (int i = 0; i < Math.Pow(2, elements.Count); i++)
            {
                var subset = new List<T>();

                for (int j = 0; j < elements.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        subset.Add(elements[j]);
                    }
                }

                subsets.Add(subset);
            }

            return subsets;
        }

        public static IEnumerable<IEnumerable<T>> GenerateChanges<T>(this IEnumerable<T> source)
        {
            var elements = source.ToList();
            var changes = new List<IEnumerable<T>>();
            
            GenerateChangesRecursive(elements, changes, 0);
            
            return changes;
        }
        
        private static void GenerateChangesRecursive<T>(List<T> elements, List<IEnumerable<T>> changes, int index)
        {
            if (index == elements.Count - 1)
            {
                changes.Add(elements.ToList());
                return;
            }
            
            for (int i = index; i < elements.Count; i++)
            {
                Swap(elements, index, i);
                GenerateChangesRecursive(elements, changes, index + 1);
                Swap(elements, index, i);
            }
        }
        
        private static void Swap<T>(List<T> elements, int i, int j)
        {
            var temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input1 = new[] { 1, 2, 3 };
            var k = 2;
            Console.WriteLine("Метод 1:");
            var combinations = input1.GenerateCombinations(k, EqualityComparer<int>.Default);
            foreach (var combination in combinations)
            {
                Console.WriteLine($"[{string.Join(", ", combination)}]");
            }

            var input2 = new[] { 1, 2 };
            var subsets = input2.GenerateSubsets();
            Console.WriteLine("Метод 2:");
            foreach (var subset in subsets)
            {
                Console.WriteLine($"[{string.Join(", ", subset)}]");
            }

            var input3 = new[] { 1, 2, 3 };
            Console.WriteLine("Метод 3:");
            var changes = input3.GenerateChanges();
            foreach (var change in changes)
            {
                Console.WriteLine($"[{string.Join(", ", change)}]");
            }
        }
    }
}
