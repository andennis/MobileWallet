
using System.Collections.Generic;

namespace Common.Web
{
    public class Effects
    {
        public Effects(string name)
        {
            Name = name;
            Container = new List<string>();
        }
        protected string Name { get; set; }
        public IList<string> Container { get; private set; }
        public int Duration { get; set; }
        public bool Reverse { get; set; }
    }
}
