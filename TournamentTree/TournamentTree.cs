using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTree
{
    /// <summary>
    /// Type of TournamentTree
    /// </summary>
    public enum TournamentTreeType
    {
        /// <summary>
        /// Root will contains maximum value
        /// </summary>
        MaxTree,
        /// <summary>
        /// Root will contains minimum value
        /// </summary>
        MinTree
    }

    public class TournamentTree
    {
        public TournamentTreeNode Root { get; private set; }

        public readonly TournamentTreeType Type;

        public TournamentTree(int[] dataList, TournamentTreeType type)
        {
            Type = type;
            Build(dataList);
        }

        /// <summary>
        /// Build the tree from array O(n)
        /// </summary>
        private void Build(int[] dataList)
        {
            var nodes = new LinkedList<TournamentTreeNode>();
            TournamentTreeNode root = null;
            for (var i = 0; i < dataList.Length; i += 2)
            {
                var firstNode = new TournamentTreeNode(i, dataList[i]);
                if (i + 1 < dataList.Length)
                {
                    var secondNode = new TournamentTreeNode(i + 1, dataList[i + 1]);
                    switch (Type)
                    {
                        case TournamentTreeType.MaxTree:
                            root = (dataList[i].CompareTo(dataList[i + 1]) > 0) ? new TournamentTreeNode(i, dataList[i]) : new TournamentTreeNode(i + 1, dataList[i + 1]);
                            break;
                        case TournamentTreeType.MinTree:
                            root = (dataList[i].CompareTo(dataList[i + 1]) < 0) ? new TournamentTreeNode(i, dataList[i]) : new TournamentTreeNode(i + 1, dataList[i + 1]);
                            break;
                    }
                    root.Left = firstNode;
                    root.Right = secondNode;
                    nodes.AddLast(root);
                }
                else
                    nodes.AddLast(firstNode);
            }
            while (nodes.Count != 1)
            {
                var last = (nodes.Count % 2 == 1) ? (nodes.Count - 2) : (nodes.Count - 1);
                for (var i = 0; i < last; i += 2)
                {
                    var firstCandidate = nodes.First();
                    nodes.RemoveFirst();
                    var secondCandidate = nodes.First();
                    nodes.RemoveFirst();
                    switch (Type)
                    {
                        case TournamentTreeType.MaxTree:
                            root = (dataList[firstCandidate.Id].CompareTo(dataList[secondCandidate.Id]) > 0) ? new TournamentTreeNode(firstCandidate.Id, firstCandidate.Value) : new TournamentTreeNode(secondCandidate.Id, secondCandidate.Value);
                            break;
                        case TournamentTreeType.MinTree:
                            root = (dataList[firstCandidate.Id].CompareTo(dataList[secondCandidate.Id]) < 0) ? new TournamentTreeNode(firstCandidate.Id, firstCandidate.Value) : new TournamentTreeNode(secondCandidate.Id, secondCandidate.Value);
                            break;
                    }
                    root.Left = firstCandidate;
                    root.Right = secondCandidate;
                    nodes.AddLast(root);
                }
                if (nodes.Count % 2 == 1)
                {
                    nodes.AddLast(nodes.First());
                    nodes.RemoveFirst();
                }
            }
            Root = root;
        }

        /// <summary>
        /// Remove root, insert new candidate in leafs and replay (something like pop and rebuild, but for log(n))
        /// </summary>
        private void UpdateRoot(TournamentTreeNode node, int value)
        {
            if (node == null) 
                return;
            if (node.Left == null && node.Right == null)
            {
                node.Value = value;
                return;
            }
            if (node.Id == node.Left.Id)
                UpdateRoot(node.Left, value);
            if (node.Id == node.Right.Id)
                UpdateRoot(node.Right, value);
            switch (Type)
            {
                case TournamentTreeType.MaxTree:
                    if (node.Left.Value.CompareTo(node.Right.Value) >= 0)
                    {
                        node.Value = node.Left.Value;
                        node.Id = node.Left.Id;
                    }
                    else
                    {
                        node.Value = node.Right.Value;
                        node.Id = node.Right.Id;
                    }
                    break;
                case TournamentTreeType.MinTree:
                    if (node.Left.Value.CompareTo(node.Right.Value) <= 0)
                    {
                        node.Value = node.Left.Value;
                        node.Id = node.Left.Id;
                    }
                    else
                    {
                        node.Value = node.Right.Value;
                        node.Id = node.Right.Id;
                    }
                    break;
            }
        }

        /// <summary>
        /// Wrapper for UpdateRoot
        /// </summary>
        public void Replay(int value)
        {
            UpdateRoot(Root, value);
        }


        /// <summary>
        /// For print
        /// </summary>
        private void Traverse(TournamentTreeNode node, int offset)
        {
            if (node == null)
                return;
            offset += 10;
            Traverse(node.Right, offset);
            Console.WriteLine();
            for (var i = 10; i < offset; i++)
                Console.Write(" ");
            Console.WriteLine("Player [" + (node.Id) + "]");
            for (var i = 10; i < offset; i++)
                Console.Write(" ");
            if (node.Value.CompareTo(int.MaxValue) == 0)
                Console.WriteLine("   (+oo)");
            else if (node.Value.CompareTo(int.MinValue) == 0)
                Console.WriteLine("   (-oo)");
            else
                Console.WriteLine("   (" + node.Value + ")");
            Traverse(node.Left, offset);
            Console.WriteLine();
        }

        public void Print()
        {
            Traverse(Root, 0);
        }
    }
}
