//Class that handles user input
//Harrison Vu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DefineEclipseProfile
{
    class EclipseProfileSetup
    {
        #region EclipseProfileSetup Variables
        private const String EXCEL_FILE_EXTENSION = ".xlsx";
        private const String FILE_DOES_NOT_EXIST_MESSAGE = "This file does not seem to exist in your Documents path!\n";
        private const String ECLIPSE_PROFILE_EXCEL_QUESTION = "What is the Eclipse Profile Excel File name? ";
        private String myFilePath = @"C:\Users\hv\Documents\"; //this line refers to the directory housing the Excel book
        private String excelFilePath = @""; //for saving the full excel path once the user inputs file name
        private String userInput = "";
        #endregion

        //Prompt user for Excel file name
        public void AskUserForExcelFileName()
        {
            #region Get File Name from User
            do
            {
                //Ask user what the file name is
                Console.Write(ECLIPSE_PROFILE_EXCEL_QUESTION);
                userInput = Console.ReadLine();

                excelFilePath = myFilePath + userInput + EXCEL_FILE_EXTENSION;

                //Check if it's in correct location
                if (!File.Exists(excelFilePath))
                {
                    Console.WriteLine(FILE_DOES_NOT_EXIST_MESSAGE);
                }
            } while (!File.Exists(excelFilePath));
            #endregion
        }//end of AskUserForApplicatoinAndSetPath method

        //**************** Setters and Getters *************************
        public String getExcelFilePath()
        {
            return excelFilePath;
        }
    }//end of class EclipseProfileSetup
}//end of namespace DefineEclipseProfile
