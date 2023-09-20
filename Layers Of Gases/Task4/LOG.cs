using TextFile;

namespace Task4
{
    public class LOG
    {
        public class InvalidAtmosphereVariableException : Exception { }

        private readonly List<LayerTypes> LayerList;
        private readonly List<AtmosphereVars> AtmosVars; 

        private readonly int layerNum;

        #region Constructor
        public LOG()
        {
            LayerList = new List<LayerTypes>();
            AtmosVars = new List<AtmosphereVars>();
        }

        public LOG(string fileName)
        {
            TextFileReader reader = new(fileName);

            LayerList = new();
            AtmosVars = new();

            try
            {
                bool l = reader.ReadInt(out layerNum);
                if (l) Console.WriteLine(layerNum);

                int i = 0;
                while (l && i < layerNum)
                {
                    l = reader.ReadChar(out char name);
                    l = reader.ReadDouble(out double thickness) && l;

                    LayerList.Add(LayerFactory.CreateLayer(name, thickness));

                    Console.WriteLine(LayerList[i].ToString());

                    i++;
                }
                
                l = reader.ReadChar(out char atmosVar);

                while (l)
                {
                    switch (atmosVar)
                    {
                        case 'T':   AtmosVars.Add(Thunderstorm.Instance(atmosVar)); break;

                        case 'S':   AtmosVars.Add(Sunshine.Instance(atmosVar)); break;

                        case 'O':   AtmosVars.Add(Other.Instance(atmosVar));    break;

                        default:    throw new InvalidAtmosphereVariableException();
                    }

                    Console.Write(atmosVar.ToString());

                    l = reader.ReadChar(out atmosVar);
                }
            }
            catch (LayerFactory.InvalidLayerNameException)
            {
                Console.WriteLine("ERROR: Invalid data from the file!");
            }
        }
        #endregion

        #region AtmosChanges
        public void AtmosChanges()
        {
            char[,] layerNameArrs =
            { 
                { LayerVisitor.GetOxygenLayer(), LayerVisitor.GetOzoneLayer() },
                { LayerVisitor.GetOxygenLayer(), LayerVisitor.GetCarbonDioxideLayer() },
                { LayerVisitor.GetOzoneLayer(), LayerVisitor.GetOxygenLayer() },
                { LayerVisitor.GetCarbonDioxideLayer(), LayerVisitor.GetOxygenLayer() }
            };

            double[] multiplierArr = { 0.5, 0.05, 0.1};

            for (int i = 0; i < AtmosVars.Count; i++)
            {
                if (IsTypeThunderstorm(AtmosVars[i].GetAtmosVar()))
                    LayerChangeProcess(layerNameArrs[0, 0], layerNameArrs[0, 1], multiplierArr[0]);

                if (IsTypeSunshine(AtmosVars[i].GetAtmosVar()))
                {
                    LayerChangeProcess(layerNameArrs[0, 0], layerNameArrs[0, 1], multiplierArr[1]);
                    LayerChangeProcess(layerNameArrs[3, 0], layerNameArrs[3, 1], multiplierArr[1]);
                }

                if (IsTypeOthers(AtmosVars[i].GetAtmosVar()))
                {
                    LayerChangeProcess(layerNameArrs[1, 0], layerNameArrs[1, 1], multiplierArr[2]);
                    LayerChangeProcess(layerNameArrs[2, 0], layerNameArrs[2, 1], multiplierArr[1]);
                }
            }

            Console.WriteLine("\n----------------------");
            Print();
        }

        private double LayerChangeProcess(char affectedLayer, char targetedLayer, double multiplier)
        {
            for (int j = 0; j < LayerList.Count; j++)
            {
                if (FoundFirstIdenticalLayer(affectedLayer, LayerList[j].GetName()))
                {
                    double changedAmount = LayerList[j].GetThickness() * multiplier;

                    double newThickness = LayerList[j].GetThickness() - changedAmount;
                    LayerList[j].SetThickness(newThickness);

                    if (ThicknessCheck(LayerList[j].GetThickness()))
                    {
                        RemoveLayer(j);

                        j--;
                    }

                    for (int k = j + 1; k < LayerList.Count; k++)
                    {
                        if (FoundFirstIdenticalLayer(LayerList[k].GetName(), targetedLayer)) return AddToNewLayer(changedAmount, ref k);

                        if (k == LayerList.Count - 1) return LastLayerCheck(targetedLayer, changedAmount);
                    }
                }
            }

            return 0;
        }
        #endregion

        #region Helper Function
        private LayerTypes RemoveLayer(int j)
        {
            for (int k = j + 1; k < LayerList.Count; k++)
            {
                if (FoundFirstIdenticalLayer(LayerList[j].GetName(), LayerList[k].GetName()))
                {
                    double newThickness = LayerList[k].GetThickness() + LayerList[j].GetThickness();
                    LayerList[k].SetThickness(newThickness);
                }
            }

            LayerTypes temp = LayerList[j];
            LayerList.RemoveAt(j);

            return temp;
        }

        private double AddToNewLayer(double changedAmount, ref int k)
        {
            double newThickness = LayerList[k].GetThickness() + changedAmount;

            LayerList[k].SetThickness(newThickness);

            return LayerList[k].GetThickness();
        }

        private double LastLayerCheck(char targetedLayer, double changedAmount)
        {
            if (ThicknessCheck(changedAmount)) return changedAmount;

            LayerList.Add(LayerFactory.CreateLayer(targetedLayer, changedAmount));

            return changedAmount;
        }

        private List<LayerTypes> Print()
        {
            for (int i = 0; i < LayerList.Count; i++) Console.WriteLine(LayerList[i].ToString());

            return LayerList;
        }
        #endregion

        #region Pre-Conditions
        public bool IterationContinues()
        {
            return (LayerList.Count < layerNum * 3 && LayerList.Count >= 3);
        }

        public bool IsTypeThunderstorm(char atmosVarID) { return atmosVarID == 'T'; }

        public bool IsTypeSunshine(char atmosVarID) { return atmosVarID == 'S'; }

        public bool IsTypeOthers(char atmosVarID) { return atmosVarID == 'O'; }

        public bool FoundFirstIdenticalLayer(char affectedLayer, char identicalLayer) { return affectedLayer == identicalLayer; }

        public bool ThicknessCheck(double thickness) { return thickness < 0.5; }
        #endregion
    }
}
