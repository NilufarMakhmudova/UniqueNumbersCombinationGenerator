I have faced a problem to divide a list of items to a smaller list of items so that each number is paired with every other number in the set. 

I needed to find the distance between several coordinates and find out the closest coordinates combination. I would supply a list of 10 coordinates to a Map Servicce API to calculates the distance matrix, it would give me the distance from each coordinates to every other coordinates in the request.
The API had a limit of 10 coordinates per request. In our case it was possible to process more than 10 coordinates. So the coordinates needed to be sent in batches. However I needed to make sure each coordinates are paired with other coordinates that were sent as a part of previous request.
To minimize the number of requests to the API which charges expensive price per each request I needed to make sure to send the least amount of requests as possible.

I have translated the case to a simpler case using numbers instead of coordinates.

So I have a list of 12 numbers. Generate c# code which created a set of maximum 5 numbers so that each number is paired with every other number in the set. 

Sample input: `1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 `

Expected output:\
Set 1 - 1, 2, 3, 4, 5\
Set 2 - 2, 3, 4, 5, 6\
Set 3 - 3, 4, 5, 6, 7\
Set 4 - 4, 5, 6, 7, 8\
Set 5 - 5, 6, 7, 8, 9\
Set 6 - 6, 7, 8, 9, 10\
Set 7 - 7, 8, 9, 10, 11\
Set 8 - 8, 9, 10, 11, 12\
Set 9 - 9, 10, 11, 12, 1\
Set 10 - 10, 11, 12, 1, 2\
Set 11 - 10, 11, 12, 3, 4\
Set 12 - 10, 11, 12, 5, 6\
Set 13 - 12, 7

ChatGPT, DeepSeek, Copilot all were suggesting to use sliding window algorith to solve.

```
using System;
using System.Collections.Generic;
class Program
{
   static void Main()
   {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        List<HashSet<int>> sets = GenerateUniquePairSets(numbers, 5);
        for (int i = 0; i < sets.Count; i++)
        {
            Console.WriteLine($"Set {i + 1} - {string.Join(", ", sets[i])}");
        }
    }
    static List<HashSet<int>> GenerateUniquePairSets(List<int> numbers, int setSize)
    {
        List<HashSet<int>> sets = new List<HashSet<int>>();
        int totalNumbers = numbers.Count;
        // Iterate through the list to create sets of 5 numbers
        for (int i = 0; i < totalNumbers; i++)
        {
            HashSet<int> currentSet = new HashSet<int>();
            // Add 5 numbers to the current set, wrapping around if necessary
            for (int j = 0; j < setSize; j++)
            {
                int number = numbers[(i + j) % totalNumbers];
                currentSet.Add(number);
            }
            // Add the current set to the list of sets
            sets.Add(currentSet);
        }
        return sets;
    }
}
```

It did not include some combinations when executed. To solve the problem this algorithm was created
