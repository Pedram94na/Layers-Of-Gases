namespace Task4
{
    abstract class AtmosphereVars
    {
        protected char atmosVar;

        protected AtmosphereVars(char atmosVar)
        {
            this.atmosVar = atmosVar;
        }

        public override string ToString() { return $"{atmosVar}"; }

        public virtual char GetAtmosVar() { return atmosVar; }
    }

    #region Sub Classes
    class Thunderstorm : AtmosphereVars
    {
        private Thunderstorm(char atmosVar) : base(atmosVar) { }

        private static Thunderstorm? instance;
        public static Thunderstorm Instance(char atmosVar)
        {
            instance ??= new Thunderstorm(atmosVar);

            return instance;
        }
    }

    class Sunshine : AtmosphereVars
    {
        private Sunshine(char atmosVar) : base(atmosVar) { }

        private static Sunshine? instance;
        public static Sunshine Instance(char atmosVar)
        {
            instance ??= new Sunshine(atmosVar);
            
            return instance;
        }
    }

    class Other : AtmosphereVars
    {
        private Other(char atmosVar) : base(atmosVar) { }

        private static Other? instance;
        public static Other Instance(char atmosVar)
        {
            instance ??= new Other(atmosVar);

            return instance;
        }
    }
    #endregion
}
