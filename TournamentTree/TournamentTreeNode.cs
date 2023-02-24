using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTree
{
    public class TournamentTreeNode
    {
        public int Value { get; set; }
        public int Id { get; set; }
        public TournamentTreeNode Left { get; set; } = null;
        public TournamentTreeNode Right { get; set; } = null;

        public TournamentTreeNode(int id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}
