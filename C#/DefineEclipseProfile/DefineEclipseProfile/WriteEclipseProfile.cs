//Class that has the methods to actually manipulate the POM file
//Harrison Vu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DefineEclipseProfile
{
    class WriteEclipseProfile
    {
        #region WriteEclipseProfile variables
        private const String profilesTag = "<profiles>";
        private const String endProfilesTag = "</profiles>";
        private const String profileTag = "<profile>";
        private const String endProfileTag = "</profile>";
        private const String activationTag = "<activation>";
        private const String endActivationTag = "</activation>";
        private const String osTag = "<os>";
        private const String endOSTag = "</os>";
        private const String dependenciesTag = "<dependencies>";
        private const String endDependenciesTag = "</dependencies>";
        private const String dependencyTag = "<dependency>";
        private const String endDependencyTag = "</dependency>";
        private const String versionTag = "<version>";
        private const String endVersionTag = "</version>";
        private const String endProjectTag = "</project>";
        private const String scopeTag = "<scope>";
        private const String endScopeTag = "</scope>";
        private const String linuxSwtIdTag = "<id>swt-gtk-linux-x86_64</id>";
        private const String windowsSwtIdTag = "<id>swt-win32-win32-x86_64</id>";
        private const String eclipseGroupIdTag = "<groupId>org.eclipse.swt</groupId>";
        private const String linuxEclipseArtifactIdTag = "<artifactId>org.eclipse.swt.gtk.linux.x86_64</artifactId>";
        private const String windowsEclipseArtifactIdTag = "<artifactId>org.eclipse.swt.win32.win32.x86</artifactId>";
        private const String linuxFamilyTag = "<family>linux</family>";
        private const String windowsFamilyTag = "<family>windows</family>";
        private const String archAMDTag = "<arch>amd64</arch>";
        private const String NO_PROFILES_MESSAGE = "This has no " + profilesTag + "\n";
        private const String PROFILES_MESSAGE = "This has a " + profilesTag + "\n";
        private const String linuxProfileType = "linux";
        private const String windowsProfileType = "windows";
        private String swtTag = "";
        private String eclipseArtifactIdTag = "";
        private String familyTag = "";
        private bool hasProfilesTag = false;
        #endregion

        //This class, given a POM, will define the enforcer version within the pluginManagement section
        public void defineEclipseProfile(String FilePath, String applicationName, String applicationVersion, String eclipseVersion, String eclipseScope, String profileType)
        {
            #region defineEclipseProfile Variables
            String eclipseVersionTag = "<version>" + eclipseVersion + "</version>";
            String eclipseScopeTag = "<scope>" + eclipseScope + "</scope>";

            #region checkOStype
            //set our type of profile based on profileType
            if (profileType == linuxProfileType)
            {
                swtTag = linuxSwtIdTag;
                eclipseArtifactIdTag = linuxEclipseArtifactIdTag;
                familyTag = linuxFamilyTag;
            }
            else //windows profile
            {
                swtTag = windowsSwtIdTag;
                eclipseArtifactIdTag = windowsEclipseArtifactIdTag;
                familyTag = windowsFamilyTag;
            }
            #endregion

            //Construct string that looks like <profiles>...</profiles></project> since we want to add in the <profiles> section as it's MIA
            String toReplaceDoesNotHaveProfilesTag = "\t" + profilesTag + "\n\t\t" + profileTag + "\n\t\t\t" + swtTag + "\n\t\t\t\t" + activationTag + "\n\t\t\t\t\t" +
                osTag + "\n\t\t\t\t\t\t" + archAMDTag + "\n\t\t\t\t\t\t" + familyTag + "\n\t\t\t\t\t" + endOSTag + "\n\t\t\t\t" + endActivationTag +
                "\n\t\t\t\t" + dependenciesTag + "\n\t\t\t\t\t" + dependencyTag + "\n\t\t\t\t\t\t" + eclipseGroupIdTag + "\n\t\t\t\t\t\t" + eclipseArtifactIdTag +
                "\n\t\t\t\t\t\t" + versionTag + eclipseVersion + endVersionTag + "\n\t\t\t\t\t\t" + scopeTag + eclipseScope + endScopeTag + "\n\t\t\t\t\t" + endDependencyTag +
                "\n\t\t\t\t" + endDependenciesTag + "\n\t\t\t" + endProfileTag + "\n\t" + endProfilesTag + "\n" + endProjectTag;

            //Construct string that looks like <profiles><profile>...</profile> since profiles section exists we don't need the end tag
            String toReplaceHasProfilesTag = "\t" + profilesTag + "\n\t\t" + profileTag + "\n\t\t\t" + swtTag + "\n\t\t\t\t" + activationTag + "\n\t\t\t\t\t" +
                osTag + "\n\t\t\t\t\t\t" + archAMDTag + "\n\t\t\t\t\t\t" + familyTag + "\n\t\t\t\t\t" + endOSTag + "\n\t\t\t\t" + endActivationTag +
                "\n\t\t\t\t" + dependenciesTag + "\n\t\t\t\t\t" + dependencyTag + "\n\t\t\t\t\t\t" + eclipseGroupIdTag + "\n\t\t\t\t\t\t" + eclipseArtifactIdTag +
                "\n\t\t\t\t\t\t" + versionTag + eclipseVersion + endVersionTag + "\n\t\t\t\t\t\t" + scopeTag + eclipseScope + endScopeTag + "\n\t\t\t\t\t" + endDependencyTag +
                "\n\t\t\t\t" + endDependenciesTag + "\n\t\t" + endProfileTag;
            #endregion

            #region read file and write profile
            String FileContents = File.ReadAllText(FilePath); //get the contents in string form

            var linesOfFile = File.ReadAllLines(FilePath);
            foreach (var lineOfFile in linesOfFile)
            {
                if (lineOfFile.Contains(profilesTag)) //looking for the plugins tag. Once found we know to only define the eclipse tag
                {
                    Console.WriteLine(PROFILES_MESSAGE);
                    hasProfilesTag = true;
                }

                if (lineOfFile.Contains(profilesTag) && hasProfilesTag == true) //saves us from reiteration, we know the next line should be <plugin> tag after <plugins>
                {
                    //replace the <profiles> tag with <profiles> + eclipse profile
                    FileContents = FileContents.Replace(lineOfFile, toReplaceHasProfilesTag);
                    File.WriteAllText(FilePath, FileContents);
                }
            }
            //After this loop, we know there's no profiles tag so it must be added to the POM
            if (hasProfilesTag != true)
            {
                Console.WriteLine(NO_PROFILES_MESSAGE);

                //replace <profiles> tag with <profiles> + eclipse profile + </project> since we are adding it to the end
                foreach (var lineOfFile in linesOfFile)
                {
                    if (lineOfFile.Contains(endProjectTag)) //in terms of efficiency, it would have been best to save the line number that had the </project> rather than re-search doc
                    {
                        FileContents = FileContents.Replace(lineOfFile, toReplaceDoesNotHaveProfilesTag);
                        File.WriteAllText(FilePath, FileContents);
                    }
                }
            }
            #endregion

        }//end of defineEclipseProfile method
    }//end of class WriteEclipseProfile
}//end of namespace DefineEclipseProfile