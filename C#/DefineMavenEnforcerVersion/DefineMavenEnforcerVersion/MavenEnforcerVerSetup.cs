//Class that handles user input
//Harrison Vu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DefineMavenEnforcerVersion
{
    class MavenEnforcerVerSetup
    {
        #region MavenEnforcerVerSetup Variables
        private const String EXCEL_FILE_EXTENSION = ".xlsx";
        private const String FILE_DOES_NOT_EXIST_MESSAGE = "This file does not seem to exist in your Documents path!\n";
        private const String MAVEN_ENFORCER_EXCEL_QUESTION = "What is the Maven Enforcer Excel File name? ";
        private String myFilePath = @"C:\Users\hv\Documents\"; //Modify this to change where the search for the book is
        private String excelFilePath = @"";
        private String userInput = "";
        #endregion

        public void AskUserForExcelBook()
        {
            do
            {
                //As user what the file name is
                Console.Write(MAVEN_ENFORCER_EXCEL_QUESTION);
                userInput = Console.ReadLine();

                excelFilePath = myFilePath + userInput + EXCEL_FILE_EXTENSION;

                //Check if it's in correct location
                if (!File.Exists(excelFilePath))
                {
                    Console.WriteLine(FILE_DOES_NOT_EXIST_MESSAGE);
                }

            } while (!File.Exists(excelFilePath));
        }//end of AskUserForApplicatoinAndSetPath method

        //**************** Setters and Getters *************************
        public String getExcelFilePath()
        {
            return excelFilePath;
        }
    }//End of MavenEnforcerVerSetup class
}//End of namespace DefineMavenEnforcerVersion