//Harrison Vu
//Class that handles getting values from Excel Sheet

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace DefineSkipDockerProperty
{
    class SkipDockerExcel
    {
        #region SkipDockerExcel Variables
        private const int MAIN_ECLIPSE_SPREADSHEET = 1;
        private const int MAIN_ECLIPSE_SHEET_START_ROW = 1;
        private const int ARTIFACT_NAME_COL = 1;
        private const int ARTIFACT_VER_COL = 2;
        private const String EXCEL_PROCESS_NAME = "Excel";
        private const String EXCEL_EXTENSION = ".xlsx";
        private const String POM_EXTENSION = ".pom";
        private const String ERR_OPENING_BOOK = "Error opening Excel book";
        private const String UNABLE_TO_FIND_MESSAGE = "Unable to find ";
        private const String DEFINING_DOCKER_SKIP_MESSAGE = "Defining docker.skip in ";
        private const String POM_START_PATH = @"C:\Users\hv\Documents\poms\"; //POM location
        private const String MY_BACKUPS = @"C:\Users\hv\Documents\backup\"; //backups for POMs being changed
        private String POM_FilePath = @"C:\Users\hv\Documents\poms\"; //POM path holder
        private String artifactName = ""; //for holding values from Excel cell
        private String artifactVer = ""; //for holding values from Excel cell
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel.Worksheet xlWorkSheet;
        private static Excel.Range xlRange;
        private int numberOfRows = 0;
        #endregion

        //open Excel to specific sheet number for reading information
        public void OpenExcelBook(String pathOfBookToOpen, int sheetNumberToView)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(pathOfBookToOpen);
                xlWorkSheet = xlWorkbook.Sheets[sheetNumberToView];
                xlRange = xlWorkSheet.UsedRange;
                numberOfRows = xlRange.Rows.Count;
            }
            catch (Exception err)
            {
                //Generally will be errors about unable to open the book
                Console.WriteLine(err);
            }
        }//end of OpenExcelBook

        public void OpenExcelBook(String pathOfBookToOpen)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(pathOfBookToOpen);
            }
            catch (Exception err)
            {
                //Generally will be errors about unable to open the book
                Console.WriteLine(err);
            }
        }//end of OpenExcelBook

        //close a given Excel book
        public void CloseExcelBook(Excel.Workbook toClose)
        {
            //Close and save changes to book
            GC.Collect();
            GC.WaitForPendingFinalizers();
            xlWorkbook.Close(toClose);
            xlApp.Quit();

            //end the Excel process so they don't stack per call
            foreach (Process process in Process.GetProcessesByName(EXCEL_PROCESS_NAME))
            {
                process.Kill();
            }
        }//end of CloseExcelBook

        //Could not think of a better name. This just reads from the Excel book and hands the class in WriteEclipseProfile information to do the actual write.
        public void WriteToPOMfromExcelBook(String pathOfBook)
        {
            //First we need to open our book
            OpenExcelBook(pathOfBook, MAIN_ECLIPSE_SPREADSHEET);

            for (int i = MAIN_ECLIPSE_SHEET_START_ROW; i <= numberOfRows; i++) //loop controls the rows we're looking at
            {
                //assemble information from Excel sheet then write to our POM
                WriteSkipDockerProperty WriteSkipDockerProperty = new WriteSkipDockerProperty();
                artifactName = xlRange.Cells[i, ARTIFACT_NAME_COL].Value2.ToString();
                artifactVer = xlRange.Cells[i, ARTIFACT_VER_COL].Value2.ToString();
                POM_FilePath = POM_FilePath + artifactName + "-" + artifactVer + POM_EXTENSION; //ipfactory\static-analysis-prod\poms\artifactName-artifactVersion.pom

                //Check if file exists; if it does, then we can write to it.
                if (!File.Exists(POM_FilePath))
                {
                    Console.WriteLine(UNABLE_TO_FIND_MESSAGE + artifactName + "-" + artifactVer + POM_EXTENSION); //file MIA
                }
                else //file does exist
                {
                    Console.WriteLine(DEFINING_DOCKER_SKIP_MESSAGE + artifactName + "-" + artifactVer + POM_EXTENSION); //file exists!

                    String backupLocation = MY_BACKUPS + artifactName + "-" + artifactVer + POM_EXTENSION;

                    //backup first
                    File.Copy(POM_FilePath, backupLocation);

                    WriteSkipDockerProperty.defineDockerSkipProperty(POM_FilePath, artifactName, artifactVer);
                }
                POM_FilePath = POM_START_PATH; //reset to back to POM path
            }

            //After this loop we know we've written to every file we could, time to close
            CloseExcelBook(xlWorkbook);
        }//end of WriteToPOMfromExcelBook

    } //end of class SkipDockerExcel
} //end of namespace DefineSkipDockerProperty
