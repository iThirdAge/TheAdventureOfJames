import os, shutil, sys

source_directory = sys.argv[1];

if source_directory in ("--help","-help","-h"):
    print ("provide src dir and dest dir in single quotes as 2 args")
    print ("copies files from source to destination")

#we aren't asking for help so we know we're going to commit to rest of script    
else:
    destination_directory = sys.argv[2];

    files_to_copy = os.listdir(source_directory)

    #change to the directory with the files
    os.chdir(source_directory)

    for file in files_to_copy:
        #copy only files, overwrites destination
        if os.path.isfile(file):
            shutil.copy(file, destination_directory)
