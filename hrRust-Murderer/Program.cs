namespace hrRust_Murderer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main()
        {
            int numCases = Int32.Parse(Console.ReadLine());
            while (numCases-- > 0)
            {
                var firstNums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                int numVertices = firstNums[0];
                int numNonEdges = firstNums[1];
                var answers = new int[numVertices + 1];
                var nonEdges = new HashSet<KeyValuePair<int, int>>();
                for (int i = 0; i < numNonEdges; i++)
                {
                    var lineSplit = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                    var first = Math.Min(lineSplit[0], lineSplit[1]);
                    var second = Math.Max(lineSplit[0], lineSplit[1]);
                    nonEdges.Add(new KeyValuePair<int, int>(first, second));
                }
                var start = Int32.Parse(Console.ReadLine());
                var q = new Queue<KeyValuePair<int, int>>();
                q.Enqueue(new KeyValuePair<int, int>(start, 0));
                var visitedVertices = new BitArray(numVertices + 1);
                while (q.Count > 0)
                {
                    var curState = q.Dequeue();
                    var curVertex = curState.Key;
                    visitedVertices.Set(curVertex, true);
                    var curDist = curState.Value;
                    for (int i = 1; i <= numVertices; i++)
                    {
                        if (visitedVertices[i]) continue;
                        var lower = Math.Min(curVertex, i);
                        var upper = Math.Max(curVertex, i);
                        if (nonEdges.Contains(new KeyValuePair<int, int>(lower, upper))) continue;
                        if (answers[i] == 0)
                        {
                            q.Enqueue(new KeyValuePair<int, int>(i, curDist + 1));
                            answers[i] = curDist + 1;
                        }
                    }
                }
                var sb = new StringBuilder();
                for (int i = 1; i <= numVertices; i++)
                {
                    if (i != start)
                    {
                        sb.Append(answers[i]);
                        sb.Append(" ");
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
