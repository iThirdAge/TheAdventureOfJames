# Docker
This contains the Dockerfile used to spin up my Jenkins Instance

Download the file and run the following command from the working directory
```
docker-compose up -d
```
This tells docker to run the YAML file and create 1 volume in the working directory to persist its information

## Credits
This is based on the following documents
* https://stackoverflow.com/questions/49984686/convert-a-docker-run-command-into-a-docker-compose/55129341
* https://www.jenkins.io/doc/tutorials/create-a-pipeline-in-blue-ocean/#run-jenkins-in-docker
