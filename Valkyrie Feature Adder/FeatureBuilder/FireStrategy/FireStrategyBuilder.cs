namespace Valkyrie_Feature_Adder
{
    public abstract class FireStrategyBuilder : FeatureBuilder
    {
        public sealed override string FeatureType => "Strategy";
        public sealed override string InitialPathObjectPoolCs => null;

        public FireStrategyBuilder(string name) : base(name)
        {

        }
    }
}
