namespace Task4
{
    public class Program
    {
        static void Main()
        {
            try
            {
                LOG layerOfGases = new("input.txt");

                while (layerOfGases.IterationContinues())
                        layerOfGases.AtmosChanges();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("ERROR: File not found!");
            }
        }
    }
}
