using System.Xml.Linq;

namespace TournamentTree
{
    public class Program
    {
        private static void traverseForTests(LinkedList<int>[] sortedLists, TournamentTreeNode node, int space)
        {
            if (node == null)
                return;
            space += 10;
            traverseForTests(sortedLists, node.Right, space);
            Console.WriteLine();
            for (var i = 10; i < space; i++)
                Console.Write(" ");
            Console.Write("Array [" + (char)(node.Id + 'A') + "]");
            if (node.Left is null && node.Right is null)
            {
                Console.Write(" => ");
                foreach (var i in sortedLists[node.Id])
                    Console.Write(i + " ");
                if (sortedLists[node.Id].Count == 0)
                    Console.Write("Empty");
            }
            Console.WriteLine();
            for (var i = 10; i < space; i++)
                Console.Write(" ");
            if (node.Value == int.MinValue)
                Console.Write("   (-oo)");
            else if (node.Value == int.MaxValue)
                Console.Write("   (+oo)");
            else
                Console.Write("   (" + node.Value + ")");
            traverseForTests(sortedLists, node.Left, space);
            Console.WriteLine();
        }

        /// <summary>
        /// find median of N sorted arrays for O(mlog(N))
        /// </summary>
        private static TournamentTreeNode FindMedianExample(LinkedList<int>[] sortedLists)
        {
            var n = sortedLists.Length;
            var m = 0;
            for (var i = 0; i < n; i++)
                m += sortedLists[i].Count();
            m = (m + 1) / 2;
            var startedArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                startedArray[i] = sortedLists[i].First();
                sortedLists[i].RemoveFirst();
            }
            var tree = new TournamentTree(startedArray, TournamentTreeType.MinTree);
            traverseForTests(sortedLists, tree.Root, 0);
            Console.WriteLine("====================");
            for (var i = 0; i < m; ++i)
            {
                var index = tree.Root.Id;
                int nextToPush;
                if (sortedLists[index].Count == 0)
                    nextToPush = int.MaxValue;
                else
                {
                    nextToPush = sortedLists[index].First();
                    sortedLists[index].RemoveFirst();
                }
                tree.Replay(nextToPush);
                traverseForTests(sortedLists, tree.Root, 0);
                Console.WriteLine("====================");
            }
            return tree.Root;
        }

        /// <summary>
        /// merge N sorted arrays into one array for nlogn
        /// </summary>
        private static int[] KWayMergeExample(LinkedList<int>[] sortedLists)
        {
            var n = sortedLists.Length;
            var allSize = 0;
            for (var i = 0; i < n; i++)
                allSize += sortedLists[i].Count();
            var startedArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                startedArray[i] = sortedLists[i].First();
                sortedLists[i].RemoveFirst();
            }
            var tree = new TournamentTree(startedArray, TournamentTreeType.MinTree);
            var result = new int[allSize];
            for (var i = 0; i < allSize; ++i)
            {
                var index = tree.Root.Id;
                result[i] = tree.Root.Value;
                int nextToPush;
                if (sortedLists[index].Count == 0)
                    nextToPush = int.MaxValue;
                else
                {
                    nextToPush = sortedLists[index].First();
                    sortedLists[index].RemoveFirst();
                }
                tree.Replay(nextToPush);
            }
            return result;
        }

        private static void Main(string[] args)
        {
            // data to sort
            var data = new int[] { 22, 4, 11, 6, 9, -4, 21, 18, 10, -6, 11, 0 };
            // by asc
            TournamentSort.Sort(data, TournamentTreeType.MinTree);
            Console.WriteLine(string.Join(", ", data.ToList()));
            // by desc
            TournamentSort.Sort(data, TournamentTreeType.MaxTree);
            Console.WriteLine(string.Join(", ", data.ToList()));

            // find median of N sorted arrays
            var mid = FindMedianExample(new LinkedList<int>[]
            {
                new LinkedList<int>(new int[] { 12, 14, 15, 16, 17, 23 }),
                new LinkedList<int>(new int[] { 4, 5, 8, 10 }),
                new LinkedList<int>(new int[] { 1, 3, 6, 7, 9 }),
            });
            Console.WriteLine("The medium is " + mid.Value + " from array " + (char)(mid.Id + 'A'));
            Console.WriteLine("====================");

            // merge N sorted arrays into one array
            var mergedArrays = KWayMergeExample(new LinkedList<int>[]
            {
                new LinkedList<int>(new int[] { 12, 14, 15, 16, 17, 23 }),
                new LinkedList<int>(new int[] { 4, 5, 8, 10 }),
                new LinkedList<int>(new int[] { 1, 3, 6, 7, 9 }),
            });

            Console.WriteLine("After merge:");
            Console.WriteLine(string.Join(", ", mergedArrays.ToList()));
        }
    }
}