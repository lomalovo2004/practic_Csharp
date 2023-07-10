using System;

public static class ArrayExtensions
{
    public static void Sort<T>(this T[] array, SortOrder sortOrder, SortAlgorithm sortAlgorithm)
    {
        switch (sortAlgorithm)
        {
            case SortAlgorithm.Insertion:
                InsertionSort(array, sortOrder);
                break;
            case SortAlgorithm.Selection:
                SelectionSort(array, sortOrder);
                break;
            case SortAlgorithm.Heap:
                HeapSort(array, sortOrder);
                break;
            case SortAlgorithm.Quick:
                QuickSort(array, sortOrder);
                break;
            case SortAlgorithm.Merge:
                MergeSort(array, sortOrder);
                break;
            default:
                throw new ArgumentException("Invalid sort algorithm specified.");
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

    private static void Heapify<T>(T[] array, int n, int i, SortOrder sortOrder){
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

    private static void MergeSort<T>(T[] array, SortOrder sortOrder)
    {
        MergeSort(array, 0, array.Length - 1, sortOrder);
    }

    private static void MergeSort<T>(T[] array, int left, int right, SortOrder sortOrder)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            MergeSort(array, left, middle, sortOrder);
            MergeSort(array, middle + 1, right, sortOrder);

            Merge(array, left, middle, right, sortOrder);
        }
    }

    private static void Merge<T>(T[] array, int left, int middle, int right, SortOrder sortOrder)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        T[] leftArray = new T[n1];
        T[] rightArray = new T[n2];

        Array.Copy(array, left, leftArray, 0, n1);
        Array.Copy(array, middle + 1, rightArray, 0, n2);

        int i = 0, j = 0;
        int k = left;

        while (i < n1 && j < n2)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                if (Comparer<T>.Default.Compare(leftArray[i], rightArray[j]) <= 0)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
            }
            else
            {
                if (Comparer<T>.Default.Compare(leftArray[i], rightArray[j]) >= 0)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
            }

            k++;
        }

        while (i < n1)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }
}

public enum SortOrder
{
    Ascending,
    Descending
}

public enum SortAlgorithm
{
    Insertion,
    Selection,
    Heap,
    Quick,
    Merge
}

class Program
{
    static void Main(string[] args)
    {
        int[] numbers3 = { 345, 24, 124, 0, 43 };
        numbers3.Sort(SortOrder.Ascending, SortAlgorithm.Insertion);
        Console.WriteLine("Сортировка вставками:");
        foreach (int number in numbers3)
        {
            Console.WriteLine(number);
        }

        int[] numbers4 = { 0, 111, 333, 666, 816 };
        numbers4.Sort(SortOrder.Ascending, SortAlgorithm.Selection);
        Console.WriteLine("Сортировка выбором:");
        foreach (int number in numbers4)
        {
            Console.WriteLine(number);
        }

        int[] numbers5 = { 14, 453, 21, 54, 56 };
        numbers5.Sort(SortOrder.Ascending, SortAlgorithm.Heap);
        Console.WriteLine("Пирамидальная сортировка:");
        foreach ( int number in numbers5)
        {
            Console.WriteLine(number);
        }

        int[] numbers6 = { 50, 23, 812, 1111, 999 };
        numbers6.Sort(SortOrder.Ascending, SortAlgorithm.Quick);
        Console.WriteLine("Быстрая сортировка:");
        foreach (int number in numbers6)
        {
            Console.WriteLine(number);
        }

        int[] numbers7 = { 17, 346, 14, 1, 90};
        numbers7.Sort(SortOrder.Ascending, SortAlgorithm.Merge);
        Console.WriteLine("Сортировка слиянием:");
        foreach (int number in numbers7)
        {
            Console.WriteLine(number);
        }

    }
}

