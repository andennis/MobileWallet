
namespace Common.Web
{
    public class Effects
    {
        public Effects(string name)
        {
            Name = name;
        }
        protected string Name { get; set; }
        //public IList<string> Container { get; }
        public int Duration { get; set; }
        public bool Reverse { get; set; }
    }
}
