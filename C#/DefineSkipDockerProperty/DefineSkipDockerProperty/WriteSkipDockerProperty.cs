using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DefineSkipDockerProperty
{
    class WriteSkipDockerProperty
    {
        #region WriteSkipDockerProperty Variables
        private const String START_SKIP_DOCKER_TAG = "<docker.skip>";
        private const String END_SKIP_DOCKER_PROPERTY_TAG = "</docker.skip>";
        private const String START_PROPERTIES_TAG = "<properties>";
        private const String END_PROPERTIES_TAG = "</properties>";
        private const String SKIP_DOCKER_PROPERTY = "<docker.skip>true</docker.skip>";
        private const String NO_SKIP_DOCKER_PROPERTY_MESSAGE = " has no " + START_SKIP_DOCKER_TAG;
        private const String SKIP_DOCKER_PROPERTY_MESSAGE = " has a " + START_SKIP_DOCKER_TAG;
        private const String TO_REPLACE_HAS_PROPERTIES_SECTION = "\t" + START_PROPERTIES_TAG+ "\n\t\t" + SKIP_DOCKER_PROPERTY;
        private const String TO_REPLACE_HAS_NO_PROPERTIES_SECTION = "\t" + START_PROPERTIES_TAG + "\n\t\t" + SKIP_DOCKER_PROPERTY + "\n\t" + END_PROPERTIES_TAG;
        private const String HAS_PROPERTIES_SECTION_MESSAGE = " has a properties section!\n";
        private bool hasPropertiesSection = false;
        private bool hasDockerSkipProperty = false;
        #endregion

        //This class, given a POM, will define the enforcer version within the pluginManagement section
        public void defineDockerSkipProperty(String FilePath, String artifactName, String artifactVersion)
        {

            String FileContents = File.ReadAllText(FilePath); //get the contents in string form

            //************* GREEDY ASSUMPTION - there is a properties section in POM ********
            var linesOfFile = File.ReadAllLines(FilePath);
            foreach (var lineOfFile in linesOfFile)
            {
                if (lineOfFile.Contains(START_PROPERTIES_TAG)) //Look for properties section and throw in the docker skip
                {
                    FileContents = FileContents.Replace(lineOfFile, TO_REPLACE_HAS_PROPERTIES_SECTION);
                    File.WriteAllText(FilePath, FileContents);
                }
            }

        }//end of defineEclipseProfile method
    } //end of class WriteSkipDockerProperty
} //end of namespace DefineSkipDockerProperty
