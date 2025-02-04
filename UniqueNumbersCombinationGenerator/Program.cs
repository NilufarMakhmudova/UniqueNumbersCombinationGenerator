class Program
{
	static void Main()
	{
		int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

		var candidateCombinations = GetCombinations(numbers, 5);
		var finalCombinations = new List<List<int>>();
		var addedCombinations = new List<NumberCombination>();

		foreach (var candidateCombination in candidateCombinations)
		{
			if (CandidateCombinationHasAnyNotAddedCombinationOfTwo(candidateCombination, addedCombinations))
			{
				finalCombinations.Add(candidateCombination);

				AddCandidateCombinationToAddedCombinations(candidateCombination, addedCombinations);
			}
		}
		
		var optimizedCombinations = OptimizeCombinations(finalCombinations, 5);

		foreach (var combination in optimizedCombinations)
		{
			Console.WriteLine(string.Join(",", combination));
		}

		Console.ReadLine();
	}

	private record NumberCombination(int a, int b);

	private static void AddCandidateCombinationToAddedCombinations(List<int> combinationsOfFive, List<NumberCombination> addedCombinations)
	{
		var combinationsOfTwo = GetCombinations(combinationsOfFive.ToArray(), 2);

		foreach (var combinationOfTwo in combinationsOfTwo)
		{
			if (addedCombinations.Any(x =>
				    (x.a == combinationOfTwo[0] && x.b == combinationOfTwo[1]) ||
				    (x.b == combinationOfTwo[0] && x.a == combinationOfTwo[1])))
			{
				continue;
			}

			addedCombinations.Add(new NumberCombination(combinationOfTwo[0], combinationOfTwo[1]));
		}
	}

	private static bool CandidateCombinationHasAnyNotAddedCombinationOfTwo(List<int> combinationsOfFive, List<NumberCombination> addedCombinations)
	{
		var combinationsOfTwo = GetCombinations(combinationsOfFive.ToArray(), 2);

		foreach (var combinationOfTwo in combinationsOfTwo)
		{
			if (!addedCombinations.Any(x => (x.a == combinationOfTwo[0] && x.b == combinationOfTwo[1]) || (x.b == combinationOfTwo[0] && x.a == combinationOfTwo[1])))
			{
				return true;
			}
		}

		return false;  
	}

	private static List<List<int>> GetCombinations(int[] numbers, int combinationLength)
	{
		if (numbers.Length < combinationLength)
		{
			return [numbers.ToList()];
		}

		List<List<int>> result = [];
		GenerateCombinations(numbers, combinationLength, 0, [], result);
		return result;
	}

	private static void GenerateCombinations(int[] numbers, int combinationLength, int start, List<int> current, List<List<int>> result)
	{
		if (current.Count == combinationLength)
		{
			result.Add([..current]);
			return;
		}

		for (var i = start; i < numbers.Length; i++)
		{
			current.Add(numbers[i]);
			GenerateCombinations(numbers, combinationLength, i + 1, current, result);
			current.RemoveAt(current.Count - 1);
		}
	}

	private static List<List<int>> OptimizeCombinations(List<List<int>> combinations, int combinationLength)
	{
		var result = new List<List<int>>();
		var currentList = new List<int>();

		foreach (var requestCombination in combinations)
		{
			if (requestCombination.Count < combinationLength)
			{
				result.Add([.. requestCombination]);
				continue;
			}

			var combinationsOfTwo = GetCombinations(requestCombination.ToArray(), 2);

			foreach (var combination in combinationsOfTwo)
			{
				if (result.Any(x => (x.Contains(combination[0]) && x.Contains(combination[1]))))
				{
					continue;
				}

				foreach (var number in combination)
				{
					if (currentList.Contains(number))
					{
						continue;
					}

					currentList.Add(number);

					if (currentList.Count == combinationLength)
					{
						result.Add([.. currentList]);

						currentList.Clear();
					}
				}
			}
		}

		if (currentList.Count > 0)
		{
			result.Add([.. currentList]);
		}

		return result;
	}

}