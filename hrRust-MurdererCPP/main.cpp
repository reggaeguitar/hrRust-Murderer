#include <vector>
#include <list>
#include <set>
#include <map>
#include <queue>
#include <string>
#include <sstream>
#include <iostream>
#include <algorithm>
#include <functional>
#include <numeric>
#include <cstdlib>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <cassert>
#include <bitset>
#include <unordered_map>

using namespace std;

const int MAX = (5 * 10e5) + 5;

int main()
{
	int numCases;
	scanf("%d", &numCases);
	while (numCases--)
	{
		int numVertices, numNonEdges;
		scanf("%d %d", &numVertices, &numNonEdges);
		vector<int> answers(numVertices + 1, 0);
		unordered_multimap<int, int> nonEdges(numNonEdges);
		for (int i = 0; i < numNonEdges; ++i)
		{
			int first, second;
			scanf("%d %d", &first, &second);
			if (first > second)
			{
				swap(first, second);
			}
			nonEdges.insert(make_pair(first, second));
		}
		int start;
		scanf("%d", &start);
		queue<pair<int, int> > q;
		q.push(make_pair(start, 0));
		bitset<MAX> visitedVertices;
		while (!q.empty())
		{
			auto curState = q.front(); q.pop();
			auto curVertex = curState.first;
			visitedVertices.set(curVertex, true);
			auto curDist = curState.second;
			for (int i = 1; i <= numVertices; ++i)
			{
				if (visitedVertices[i]) continue;
				int lower = min(curVertex, i);
				int upper = max(curVertex, i);
				auto its = nonEdges.equal_range(lower);
				bool cont = false;
				for (auto it = its.first; it != its.second; ++it)
				{
					if (it->second == upper)
					{
						cont = true;
						break;
					}
				}
				if (cont) continue;
				if (answers[i] == 0)
				{
					q.push(make_pair(i, curDist + 1));
					answers[i] = curDist + 1;
				}				
			}			
		}
		string output;
		for (int i = 1; i <= numVertices; ++i)
		{
			if (i != start)
			{
				output += to_string(answers[i]) + " ";
			}
		}
		cout << output << endl;
	}
	return 0;
}