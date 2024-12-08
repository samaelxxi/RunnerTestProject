using System;


[Serializable]
public struct BinomialDistribution
{
    public int Trials;
    public float Probability;

    private static readonly Random RandomGenerator = new();

    public BinomialDistribution(int trials, float probability)
    {
        if (trials < 0)
            throw new ArgumentException("Number of trials cannot be negative.");
        if (probability < 0f || probability > 1f)
            throw new ArgumentException("Probability must be between 0 and 1.");

        Trials = trials;
        Probability = probability;
    }

    public int Sample()
    {
        int successes = 0;

        for (int i = 0; i < Trials; i++)
            if (RandomGenerator.NextDouble() < Probability)
                successes++;

        return successes;
    }
}