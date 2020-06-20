using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineEclipseProfile
{
    class Program
    {
        static void Main(string[] args)
        {
            //start off by getting input for where the Excel file is
            EclipseProfileSetup EclipseProfileSetup = new EclipseProfileSetup();
            EclipseProfileSetup.AskUserForExcelFileName();

            //Call the Excel class to get started processing
            EclipseProfileExcel EclipseProfileExcel = new EclipseProfileExcel();

            //Write by handing the Excel book
            EclipseProfileExcel.WriteToPOMfromExcelBook(EclipseProfileSetup.getExcelFilePath());
        }//end of Main
    }//end of class Program
}//end of namespace DefineEclipseProfile