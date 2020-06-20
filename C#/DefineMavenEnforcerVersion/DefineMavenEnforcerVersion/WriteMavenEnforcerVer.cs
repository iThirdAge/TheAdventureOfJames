//Class that has the methods to actually manipulate the POM file
//Harrison Vu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DefineMavenEnforcerVersion
{
    class WriteMavenEnforcerVer
    {
        #region WriteMavenEnforcerVer Variables
        private const String pluginManagementTag = "<pluginManagement>";
        private const String pluginsTag = "<plugins>";
        private const String buildTag = "<build>";
        private const String enforcerGroupIdTag = "<groupId>org.apache.maven.plugins</groupId>";
        private const String enforcerArtifactIdTag = "<artifactId>maven-enforcer-plugin</artifactId>";
        private const String NO_PLUGIN_MAN_MESSAGE = "This has no " + pluginManagementTag + "\n";
        private const String PLUGIN_MAN_MESSAGE = "This has a " + pluginManagementTag + "\n";
        private bool hasPluginManagementTag = false;
        #endregion

        //This class, given a POM, will define the enforcer version within the pluginManagement section
        public void defineEnforcerVersion(String FilePath, String applicationName, String applicationVersion, String enforcerVersion)
        {
            String enforcerVersionTag = "<version>" + enforcerVersion + "</version>";
            String toReplaceDoesNotHavePluginManagementTag = "\t" + buildTag + "\n" + "\t\t<pluginManagement>\n\t\t\t<plugins>\n\t\t\t\t<plugin>\n\t\t\t\t\t"
                + enforcerGroupIdTag + "\n\t\t\t\t\t" + enforcerArtifactIdTag + "\n\t\t\t\t\t" + enforcerVersionTag + "\n\t\t\t\t</plugin>\n\t\t\t</plugins>\n\t\t</pluginManagement>";

            String toReplaceHasPluginManagementTag = "\t\t\t<plugins>\n\t\t\t\t<plugin>\n\t\t\t\t\t"
                + enforcerGroupIdTag + "\n\t\t\t\t\t" + enforcerArtifactIdTag + "\n\t\t\t\t\t" + enforcerVersionTag + "\n\t\t\t\t</plugin>";

            String FileContents = File.ReadAllText(FilePath); //get the contents in string form

            var linesOfFile = File.ReadAllLines(FilePath);
            foreach (var lineOfFile in linesOfFile)
            {
                if (lineOfFile.Contains(pluginManagementTag)) //looking for the pluginManagement tag. Once found we know to only define the enforcer version
                {
                    Console.WriteLine(PLUGIN_MAN_MESSAGE);
                    hasPluginManagementTag = true;
                }

                if (lineOfFile.Contains(pluginsTag) && hasPluginManagementTag == true) //saves us from reiteration, we know the next line should be <plugins> tag
                {
                    //replace the <plugins> tag with <plugins> + enforcer-related tags
                    FileContents = FileContents.Replace(lineOfFile, toReplaceHasPluginManagementTag);
                    File.WriteAllText(FilePath, FileContents);
                }
            }
            //After this loop, we know there's no pluginManagement tag so it must be added to the POM
            if (hasPluginManagementTag != true)
            {
                Console.WriteLine(NO_PLUGIN_MAN_MESSAGE);
                //replace <build> tag with <build> + <pluginManagement> + enforcer-related tags
                foreach (var lineOfFile in linesOfFile)
                {
                    if (lineOfFile.Contains(buildTag)) //in terms of efficiency, it would have been best to save the line number that had the buildtag rather than research doc
                    {
                        FileContents = FileContents.Replace(lineOfFile, toReplaceDoesNotHavePluginManagementTag);
                        File.WriteAllText(FilePath, FileContents);
                    }
                }
            }
        }//end of defineEnforcerVersion method
    }//end of class WriteMavenEnforcerVer
}//end of namespace DefineMavenEnforcerVersion