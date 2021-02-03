using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public interface IWorldObject {
        Type objectType { get; }
        F_NGN owner { get; set; }
        string name { get; set; }
        List<Shape> shapes { get; set; }
    }
}
