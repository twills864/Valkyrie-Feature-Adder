namespace Valkyrie_Feature_Adder
{
    public class EnemyFireStrategyBuilder : FireStrategyBuilder
    {
        public override string InitialPathTemplateCs => TemplatePaths.PathEnemyFireStrategy;
        public override string InitialDirDestination => UnityPaths.DirEnemyFireStrategy;

        public EnemyFireStrategyBuilder(string name) : base(name)
        {

        }
    }
}
