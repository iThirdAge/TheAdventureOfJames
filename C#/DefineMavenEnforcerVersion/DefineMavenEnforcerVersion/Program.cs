using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineMavenEnforcerVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            //start off by getting our input for where Excel file is
            MavenEnforcerVerSetup MavenEnforcerVerSetup = new MavenEnforcerVerSetup();
            MavenEnforcerVerSetup.AskUserForExcelBook();

            //we do not need to call the WriteMavenEnforcerVer class here to write, we instead call our Excel class as it makes the call there.
            MavenEnforcerExcel MavenEnforcerExcel = new MavenEnforcerExcel();

            //Write by handing the Excel book
            MavenEnforcerExcel.WriteToPOMfromExcelBook(MavenEnforcerVerSetup.getExcelFilePath());
        }//end of Main
    }//end of class Program
}//end of namespace DefineMavenEnforcerVersion