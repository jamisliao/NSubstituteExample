namespace NSubstitute_Example
{
    public class Calculator : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Decrease(int x, int y)
        {
            return x - y;
        }

        public int Mix(int x, int y, int z)
        {
            int tmp = Add(x, y);
            int result = Decrease(tmp, z);
            return result;
        }
    }
}