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
        public static Random random = new Random(Environment.TickCount);

        public const string res_folder = "../../Resources/";
        public const string file_system_path = res_folder + "File_System";
        public static File_System file_system = null;
        public static File_System.User cur_user;

        public static Scheduler scheduler = null;
        #endregion DATA

        #region FORMS
        public static FORM_Authorization FORM_Authorization;
        public static FORM_Main FORM_Main;
        #endregion FORMS
    }
}
