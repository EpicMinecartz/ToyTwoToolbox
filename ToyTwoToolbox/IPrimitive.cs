using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public interface IPrimitive {
        Type PrimType { get; }
        int type { get; set; }
        int materialID { get; set; }
        int vertexCount { get; set; }
        List<int> vertices { get; set; }
    }
}
