
namespace PW.Controls
{
    public class TagCloudItemSize 
    {
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        
        public double XRadius
        {
            get { return XOffset * 6 / 10; }
        }
        
        public double YRadius
        {
            get { return YOffset * 2 / 3; }
        }
    }
}
