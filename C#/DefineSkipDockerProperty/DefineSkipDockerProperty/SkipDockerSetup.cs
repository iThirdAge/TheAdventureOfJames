//Harrison Vu
//Class that handles user input

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DefineSkipDockerProperty
{
    class SkipDockerSetup
    {
        #region SkipDockerSetup Variables
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
            do
            {
                //As user what the file name is
                Console.Write(ECLIPSE_PROFILE_EXCEL_QUESTION);
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
        #region SkipDockerSetup Setters and Getters
        public String getExcelFilePath()
        {
            return excelFilePath;
        }
        #endregion

    } //end of class SkipDockerSetup
} //end of namespace DefineDockerSkipProperty
