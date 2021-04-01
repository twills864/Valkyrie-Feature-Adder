using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public abstract class FireStrategyBuilder : FeatureBuilder
    {
        public sealed override string Type => "Strategy";
        //public override string InitialPathTemplateCs => TemplatePaths.PathPlayerFireStrategy;
        //public override string InitialDirDestination => UnityPaths.DirPlayerFireStrategy;
        public sealed override string InitialPathObjectPoolCs => null;

        public FireStrategyBuilder(string name) : base(name)
        {

        }
    }
}
