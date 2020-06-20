//This class handles getting values to fill in for the Eclipse profile from an Excel spreadsheet
//Harrison Vu

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

namespace DefineEclipseProfile
{
    class EclipseProfileExcel
    {
        #region EclipseProfileExcel variables
        private const int MAIN_ECLIPSE_SPREADSHEET = 1;
        private const int MAIN_ECLIPSE_SHEET_START_ROW = 1;
        private const int ARTIFACT_NAME_COL = 1;
        private const int ARTIFACT_VER_COL = 2;
        private const int ECLIPSE_VER_COL = 3;
        private const int ECLIPSE_SCOPE_COL = 4;
        private const int ECLIPSE_TYPE_COL = 5;
        private const String EXCEL_PROCESS_NAME = "Excel";
        private const String EXCEL_EXTENSION = ".xlsx";
        private const String POM_EXTENSION = ".pom";
        private const String POM_WITH_XML_EXTENSION = "pom.xml";
        private const String ERR_OPENING_BOOK = "Error opening Excel book";
        private const String UNABLE_TO_FIND_MESSAGE = "Unable to find ";
        private const String DEFINING_ECLIPSE_MESSAGE = "Defining Eclipse Ver in ";
        private const String COPYING_MESSAGE = "Copying ";
        private const String windowsProfileType = "windows";
        private const String UNABLE_TO_OPEN_FILE_MESSAGE = "Unable to open specified file";
        private const String myBackups = @"C:\Users\hvu\Documents\backup\";
        private const String POM_PATH = @"C:\Users\hvu\Documents\poms\";
        private String POM_FilePath = @"C:\Users\hvu\Documents\poms\"; //modifiable POM path variable
        private String SOURCES_FilePath = @""; //where to copy POMs to.
        private String artifactName = ""; //for holding values from Excel cell
        private String artifactVer = ""; //for holding values from Excel cell
        private String eclipseVer = ""; //for holding values from Excel cell
        private String eclipseScope = ""; //for holding values from Excel cell
        private String eclipseProfileType = ""; //for holding values from Excel cell
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel.Worksheet xlWorkSheet;
        private static Excel.Range xlRange;
        private int numberOfRows = 0;
        #endregion 

        //open Excel to specific sheet number for reading information
        public void OpenExcelBook(String pathOfBookToOpen, int sheetNumberToView)
        {
            #region try to Open Excel Book to Sheet
            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(pathOfBookToOpen);
                xlWorkSheet = xlWorkbook.Sheets[sheetNumberToView];
                xlRange = xlWorkSheet.UsedRange;
                numberOfRows = xlRange.Rows.Count;
            }
            catch (IOException err)
            {
                //Generally will be errors about unable to open the book
                Console.WriteLine(UNABLE_TO_OPEN_FILE_MESSAGE);
            }
            #endregion
        }//end of OpenExcelBook

        //just open the Excel book; Method polymorphism
        public void OpenExcelBook(String pathOfBookToOpen)
        {
            #region Open Excel Book
            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(pathOfBookToOpen);
            }
            catch (IOException err)
            {
                //Generally will be errors about unable to open the book
                Console.WriteLine(UNABLE_TO_OPEN_FILE_MESSAGE);
            }
            #endregion
        }//end of OpenExcelBook

        //close a given Excel book
        public void CloseExcelBook(Excel.Workbook toClose)
        {
            #region save and close Excel book
            //Close and save changes to book
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            xlWorkbook.Close(toClose);
            xlApp.Quit();
            #endregion
            
            #region end Excel process
            //end the Excel process so they don't stack per call
            foreach (Process process in Process.GetProcessesByName(EXCEL_PROCESS_NAME))
            {
                process.Kill();
            }
            #endregion
        }//end of CloseExcelBook

        //Copy file from location to location
        public void CopyPOMtoDestination(String pomLocation, String artifactName, String artifactVer) //pom location refers to the POMs directory and what file to copy over
        {
            #region CopyPOMtoDestination variables
            String destinationDir = SOURCES_FilePath + artifactName + "-" + artifactVer;
            #endregion

            #region Copy from POMs directory to Sources
            //check to make sure there's a destination dir to write to, don't just create one
            if (!Directory.Exists(destinationDir))
            {
                Console.WriteLine(UNABLE_TO_FIND_MESSAGE + destinationDir + " directory.");
            }
            else //directory exists, we copy over
            {
                String fullDestination = destinationDir + @"\" +  POM_WITH_XML_EXTENSION;

                Console.WriteLine(COPYING_MESSAGE + pomLocation + " to " + fullDestination + "\n");
                File.Copy(pomLocation, fullDestination, true); //copy to and overwrite
            }
            #endregion
        }//end of CopyPOMtoDestination

        //Could not think of a better name. This just reads from the Excel book and hands the class in WriteEclipseProfile information to do the actual write.
        public void WriteToPOMfromExcelBook(String pathOfBook)
        {
            //First we need to open our book
            OpenExcelBook(pathOfBook, MAIN_ECLIPSE_SPREADSHEET);

            #region Read excel sheet and populate variables to determine pom to write to
            for (int i = MAIN_ECLIPSE_SHEET_START_ROW; i <= numberOfRows; i++) //loop controls the rows we're looking at
            {
                //assemble information from Excel sheet then write to our POM
                #region read Excel and fill variables
                WriteEclipseProfile WriteEclipseProfile = new WriteEclipseProfile();
                artifactName = xlRange.Cells[i, ARTIFACT_NAME_COL].Value2.ToString();
                artifactVer = xlRange.Cells[i, ARTIFACT_VER_COL].Value2.ToString();
                eclipseVer = xlRange.Cells[i, ECLIPSE_VER_COL].Value2.ToString();
                eclipseScope = xlRange.Cells[i, ECLIPSE_SCOPE_COL].Value2.ToString();
                eclipseProfileType = xlRange.Cells[i, ECLIPSE_TYPE_COL].Value2.ToString();
                eclipseProfileType = eclipseProfileType.ToLower();
                POM_FilePath = POM_FilePath +  artifactName + "-" + artifactVer + POM_EXTENSION;
                #endregion

                //Check if file exists; if it does, then we can write to it.
                #region write to the POM according to Excel row
                if (!File.Exists(POM_FilePath))
                {
                    Console.WriteLine(UNABLE_TO_FIND_MESSAGE + artifactName + "-" + artifactVer + POM_EXTENSION); //file MIA
                }
                else //file does exist
                {
                    #region POM file exists, write missing profile
                    Console.WriteLine(eclipseProfileType + ": " + DEFINING_ECLIPSE_MESSAGE + artifactName + "-" + artifactVer + POM_EXTENSION); //file exists!

                    String backupLocation = myBackups + artifactName + "-" + artifactVer + POM_EXTENSION;

                    //backup first
                    #region create a backup
                    File.Copy(POM_FilePath, backupLocation);
                    #endregion

                    #region write to the POM
                    WriteEclipseProfile.defineEclipseProfile(POM_FilePath, artifactName, artifactVer, eclipseVer, eclipseScope, eclipseProfileType);
                    #endregion

                    //copy those changes over since the Windows job needs it only for Windows profiles
                    #region copy the POM to the Sources share incase we need to run on Windows
                    if (eclipseProfileType == windowsProfileType)
                    {
                        CopyPOMtoDestination(POM_FilePath, artifactName, artifactVer);
                    }
                    #endregion
                    #endregion
                }
                POM_FilePath = POM_PATH; //reset to back to POM path
                #endregion
            }//end of for loop for excel
            #endregion

            //After this loop we know we've written to every file we could, time to close
            CloseExcelBook(xlWorkbook);
        }//end of WriteToPOMfromExcelBook
    }//end of class MavenEnforcerExcel
}//end of namespace DefineEclipseProfile
