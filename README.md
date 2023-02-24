# TournamentTree
Tournamet Tree / Selection Tree / Winner Tree / Loser Tree 

A small example of the Tournament Tree data structure. I don't vouch for the correctness of the entire code. Maybe it will be useful for someone to write their solution.

One of my friends asked me to demonstrate:
- Sorting with this tree
- Finding the median of H sorted arrays
- Merging N sorted arrays (K way merge)

1. For sorting, its necessary to build a tournament tree and call the Replay procedure N times. Building a tree for O(n), 1 Replay for log(n) => O(nlogn). It's like heap sort, also unstable.
1. To find the median, you need to select the first elements from n arrays and build a tournament based on them. Then find the median position from the formula (N+1)/2. The next m times call Replay and return root.
1. To merge n sorted arrays, you need to select the first elements from n arrays and build a tournament based on them. Then call Replay n times and store the value of the root. O(nlog(n))
