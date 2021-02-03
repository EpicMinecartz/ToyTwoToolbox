using System.Windows.Forms;

namespace ToyTwoToolbox {
    public interface IEditor {
        string tempName { get; set; }
        string filePath { get; set; }
        UserControl main { get; set; }
        TabController.TCTab owner { get; set; }

        bool SaveChanges(bool inMemory, string path);
        void Init(F_Base file = null);
    }

    //EDITOR TEMPLATE
    //First, obv use the interface above
    //Secondly, make sure you use some form of usercontrol.

}
