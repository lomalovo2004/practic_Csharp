using System;

public static class ArrayExtensions
{
    public static void Sort<T>(this T[] array, SortOrder sortOrder, Comparison<T> comparison)
    {
        switch (sortOrder)
        {
            case SortOrder.Ascending:
                Array.Sort(array, comparison);
                break;
            case SortOrder.Descending:
                Array.Sort(array, (x, y) => comparison(y, x));
                break;
            default:
                throw new ArgumentException("Invalid sort order specified.");
        }
    }

    public static void Sort<T>(this T[] array, SortOrder sortOrder, IComparer<T> comparer)
    {
        switch (sortOrder)
        {
            case SortOrder.Ascending:
                Array.Sort(array, comparer);
                break;
            case SortOrder.Descending:
                Array.Sort(array, (x, y) => comparer.Compare(y, x));
                break;
            default:
                throw new ArgumentException("Invalid sort order specified.");
        }
    }

    private static void InsertionSort<T>(T[] array, SortOrder sortOrder)
    {
        for (int i = 1; i < array.Length; i++)
        {
            T key = array[i];
            int j = i - 1;

            if (sortOrder == SortOrder.Ascending)
            {
                while (j >= 0 && Comparer<T>.Default.Compare(array[j], key) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
            }
            else
            {
                while (j >= 0 && Comparer<T>.Default.Compare(array[j], key) < 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
            }

            array[j + 1] = key;
        }
    }

    private static void SelectionSort<T>(T[] array, SortOrder sortOrder)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < array.Length; j++)
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    if (Comparer<T>.Default.Compare(array[j], array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                else
                {
                    if (Comparer<T>.Default.Compare(array[j], array[minIndex]) > 0)
                    {
                        minIndex = j;
                    }
                }
            }

            T temp = array[minIndex];
            array[minIndex] = array[i];
            array[i] = temp;
        }
    }

    private static void HeapSort<T>(T[] array, SortOrder sortOrder)
    {
        int n = array.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(array, n, i, sortOrder);
        }

        for (int i = n - 1; i >= 0; i--)
        {
            T temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            Heapify(array, i, 0, sortOrder);
        }
    }

    private static void Heapify<T>(T[] array, int n, int i, SortOrder sortOrder)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (sortOrder == SortOrder.Ascending)
        {
            if (left < n && Comparer<T>.Default.Compare(array[left], array[largest]) > 0)
            {
                largest = left;
            }

            if (right < n && Comparer<T>.Default.Compare(array[right], array[largest]) > 0)
            {
                largest = right;
            }
        }
        else
        {
            if (left < n && Comparer<T>.Default.Compare(array[left], array[largest]) < 0)
            {
                largest = left;
            }

            if (right < n && Comparer<T>.Default.Compare(array[right], array[largest]) < 0)
            {
                largest = right;
            }
        }

        if (largest != i)
        {
            T temp = array[i];
            array[i] = array[largest];
            array[largest] = temp;

            Heapify(array, n, largest, sortOrder);
        }
    }

    private static void QuickSort<T>(T[] array, SortOrder sortOrder)
    {
        QuickSort(array, 0, array.Length - 1, sortOrder);
    }

    private static void QuickSort<T>(T[] array, int low, int high, SortOrder sortOrder)
    {
        if (low < high)
        {
            int partitionIndex = Partition(array, low, high, sortOrder);

            QuickSort(array, low, partitionIndex - 1, sortOrder);
            QuickSort(array, partitionIndex + 1, high, sortOrder);
        }
    }

    private static int Partition<T>(T[] array, int low, int high, SortOrder sortOrder)
    {
        T pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                if (Comparer<T>.Default.Compare(array[j], pivot) < 0)
                {
                    i++;
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            else
            {
                if (Comparer<T>.Default.Compare(array[j], pivot) > 0)
                {
                    i++;
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }

        T temp2 = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp2;

        return i + 1;
    }
}

public enum SortOrder
{
    Ascending,
    Descending
}

class Program
{
    static void Main(string[] args)
    {
        int[] numbers1 = { 50, 2, 8, 1, 9 };
        numbers1.Sort(SortOrder.Descending, (x, y) => x.CompareTo(y));
        Console.WriteLine("Сортировка вставками:");
        foreach (int number in numbers1)
        {
            Console.WriteLine(number);
        }

        int[] numbers2 = { 10, 3, 7, 4, 6 };
        numbers2.Sort(SortOrder.Ascending, (x, y) => x.CompareTo(y));
        Console.WriteLine("Сортировка выбором:");
        foreach (int number in numbers2)
        {
            Console.WriteLine(number);
        }

        int[] numbers3 = { 111, 341, 741, 454, 666 };
        numbers3.Sort(SortOrder.Descending, (x, y) => x.CompareTo(y));
        Console.WriteLine("Пирамидальная сортировка:");
        foreach (int number in numbers3)
        {
            Console.WriteLine(number);
        }

        int[] numbers4 = { 1025, 336, 57, 0, 32 };
        numbers4.Sort(SortOrder.Ascending, (x, y) => x.CompareTo(y));
        Console.WriteLine("Быстрая выбором:");
        foreach (int number in numbers4)
        {
            Console.WriteLine(number);
        }

        int[] numbers5 = { 645, 3462, 986, 999, 1 };
        numbers5.Sort(SortOrder.Ascending, (x, y) => x.CompareTo(y));
        Console.WriteLine("Сортировка слиянием:");
        foreach (int number in numbers5)
        {
            Console.WriteLine(number);
        }
    }
}
