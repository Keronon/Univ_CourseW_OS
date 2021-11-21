using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseW
{
    struct DataKeeper
    {
        #region DATA
        public const string res_folder = "e:/studying/half_5/1) ОС/Курсовая/University_CourseW_OS/CourseW/CourseW/Resources/";
        public static File_System file_System = new File_System();
        #endregion DATA

        #region FORMS
        public static FORM_Authorization FORM_Authorization;
        public static FORM_Main FORM_Main;
        #endregion FORMS
    }
}
