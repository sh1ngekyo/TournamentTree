using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTree
{
    public static class TournamentSort
    {
        /// <summary>
        /// Sort for nlogn
        /// </summary>
        public static void Sort(int [] data, TournamentTreeType type)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));
            var tree = new TournamentTree(data, type);
            // we need to operate with infinity for replay
            var inf = type is TournamentTreeType.MaxTree ? int.MinValue : int.MaxValue;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = tree.Root.Value;
                tree.Replay(inf);
            }
        }
    }
}
