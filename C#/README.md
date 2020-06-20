### All of the following expect xlsx files that start on row 1

## DefineEclipseProfile expects a 1 sheet xslx file
```
ARTIFACT_NAME_COL = 1;
ARTIFACT_VER_COL = 2;
ECLIPSE_VER_COL = 3;
ECLIPSE_SCOPE_COL = 4;
ECLIPSE_TYPE_COL = 5;
```
Writes the eclipse profile into the POM

## DefineMavenEnforcerVersion expects a 1 sheet xslx file
```
ARTIFACT_NAME_COL = 1;
ARTIFACT_VER_COL = 2;
ENFORCER_VER_COL = 3;
```
Writes the maven-enforcer version into the POM

## DefineSkipDockerProperty expects a 1 sheet xslx file
```
ARTIFACT_NAME_COL = 1;
ARTIFACT_VER_COL = 2;
```
writes <docker.skip>true</docker.skip> into the POM
