//Harrison Vu
//Main entry point

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineSkipDockerProperty
{
    class Program
    {
        static void Main(string[] args)
        {
            //start off by getting input for where the Excel file is
            SkipDockerSetup SkipDockerSetup = new SkipDockerSetup();
            SkipDockerSetup.AskUserForExcelFileName();

            //Call the Excel class to get started processing
            SkipDockerExcel SkipDockerExcel = new SkipDockerExcel();

            //Write by handing the Excel book
            SkipDockerExcel.WriteToPOMfromExcelBook(SkipDockerSetup.getExcelFilePath());
        }
    }
}
