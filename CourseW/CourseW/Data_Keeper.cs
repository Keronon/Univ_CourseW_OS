using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseW
{
    struct Data_Keeper
    {
        #region DATA
        public const string res_folder = "../../Resources/";
        public static File_System file_system = new File_System(res_folder + "File_System");
        #endregion DATA

        #region FORMS
        public static FORM_Authorization FORM_Authorization;
        public static FORM_Main FORM_Main;
        #endregion FORMS
    }
}
