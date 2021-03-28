using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab01.Core
{
    public class Glass
    {
        private Juice juice;

        public void Pour(Juice juice)
        {
            if (this.juice == null)
            {
                this.juice = juice;
            }
            else
            {
                this.juice = this.juice.Mix(juice);
            }
        }
    
        public Juice Empty()
        {
            if (this.juice != null)
            {
                Juice juice = this.juice;
                this.juice = null;
                return juice;
            }
            throw new Exception("Cannot empty the empty glass.");
        }

        public override String ToString()
        {
            if (juice != null)
            {
                return "Glass with juice. " + juice.ToString();
            }
            return "Glass is empty.";
        }
    }
}
