namespace Task4
{
    abstract class LayerTypes
    {
        protected char name;
        protected double thickness;

        protected LayerTypes(char name, double thickness)
        {
            this.name = name;
            this.thickness = thickness;
        }

        public override string ToString() { return $"{name} {thickness:0.0}"; }

        public virtual char GetName() { return name; }

        public virtual double GetThickness() { return thickness; }

        public virtual double SetThickness(double thickness)
        {
            this.thickness = thickness;

            return this.thickness;
        }
    }

    #region Sub Classes
    class Ozone : LayerTypes
    {
        public static char LayerName { get; } = 'Z';

        public Ozone(double thickness) : base('Z', thickness) { }
    }

    class Oxygen : LayerTypes
    {
        public static char LayerName { get; } = 'X';

        public Oxygen(double thickness) : base('X', thickness) { }
    }

    class CarbonDioxide : LayerTypes
    {
        public static char LayerName { get; } = 'C';

        public CarbonDioxide(double thickness) : base('C', thickness) { }
    }
    #endregion

    static class LayerFactory
    {
        public class InvalidLayerNameException : Exception { }

        public static LayerTypes CreateLayer(char name, double thickness)
        {
            return name switch
            {
                'Z' => new Ozone(thickness),
                'X' => new Oxygen(thickness),
                'C' => new CarbonDioxide(thickness),
                _ => throw new InvalidLayerNameException(),
            };
        }
    }

    static class LayerVisitor
    {
        public static char GetOzoneLayer() { return Ozone.LayerName; }

        public static char GetOxygenLayer() { return Oxygen.LayerName; }

        public static char GetCarbonDioxideLayer() { return CarbonDioxide.LayerName; }
    }
}