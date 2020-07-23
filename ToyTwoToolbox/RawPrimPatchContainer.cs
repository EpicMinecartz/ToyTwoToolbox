using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    /// <summary>
    /// Simple container class for Primitives and Patches<para/>
    /// <seealso cref="rawPrim"/> Will contain the object
    /// 
    /// </summary>
    class RawPrimContainer {

        object rawPrim;

        public RawPrimContainer(Primitive Prim) {
            rawPrim = Prim;
        }

        public RawPrimContainer(Patch patch) {
            rawPrim = patch;
        }

    }
}
