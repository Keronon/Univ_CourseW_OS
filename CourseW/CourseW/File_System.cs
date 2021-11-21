using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class File_System
    {
        #region VARS
        Super_Block superBlock;
        #endregion VARS

        #region FUNCS
        public File_System()
        {
            Log.Write("File_System | Initialization\n");

            Log.Write("File_System | Initialized\n");
        }
        #endregion FUNCS

        #region OTHER
        struct Super_Block
        {

        }
        #endregion OTHER
    }
}
