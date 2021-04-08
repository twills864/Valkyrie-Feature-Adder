namespace Valkyrie_Feature_Adder
{
    public class PlayerFireStrategyBuilder : FireStrategyBuilder
    {
        public override string InitialPathTemplateCs => TemplatePaths.PathPlayerFireStrategy;
        public override string InitialDirDestination => UnityPaths.DirPlayerFireStrategy;

        public PlayerFireStrategyBuilder(string name) : base(name)
        {

        }
    }
}
