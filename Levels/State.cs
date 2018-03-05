using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoContra.Levels
{
    public abstract class State
    {
        public State()
        {
        }
        

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }


    }    
}
