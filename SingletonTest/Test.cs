using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingletonTest
{
    public class Test
    {
        private int myVar=2;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

    }
}
