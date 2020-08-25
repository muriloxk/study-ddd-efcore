using System;
using System.Linq;

namespace Ddd.EfCore
{
    public class Suffix : Entity
    {
        public static readonly Suffix Jr = new Suffix("Jr");
        public static readonly Suffix Sr = new Suffix("Sr");

        public static readonly Suffix[] AllSuffixes = { Jr, Sr };

        public string Name { get; }

        protected Suffix()
        {
        }

        private Suffix(string name)
            : base()
        {
            Name = name;
        }

        public static Suffix FromId(Guid id)
        {
            return AllSuffixes.SingleOrDefault(x => x.Id == id);
        }
    }
}
