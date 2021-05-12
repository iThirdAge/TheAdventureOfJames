# Jenkins
This contains the docker-compose file used to spin up a Jenkins Instance

Download the file and run the following commands from the working directory
```
docker-compose up -d
```

This tells docker to run the YAML file and create 1 volume in the working directory to persist its information
The volume is titled "jenkins" to persist its data.
It is configured to map port 8081 of your local machine to 8080 of the container. Thus to visit your Jenkins, go to localhost:8081

Run this command to access the initial admin password to set up your instance. 
Recall that Docker, by default, runs in Linux mode so think of all files it has as a Linux machine directory
```
docker exec jenkins cat /var/jenkins_home/secrets/initialAdminPassword
```

Run the following command to take down the instance
```
docker-compose down
```

To upgrade, simply change the tag it's pointing to
```
version: '3.7'
services:
  jenkins:
    image: jenkins/jenkins:2.223.1
    ...
```

## Credits
This is based on the following documents
* https://dev.to/andresfmoya/install-jenkins-using-docker-compose-4cab
* https://www.jenkins.io/doc/tutorials/create-a-pipeline-in-blue-ocean/#run-jenkins-in-docker
