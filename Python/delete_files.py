import os, shutil, sys

folder_path = sys.argv[1];

if folder_path in ("--help","-help","-h"):
    print ("provide directory of files to delete in single quotes")
    print ("deletes files from specified folder")

else:
    #swap to directory containing files to delete
    os.chdir(folder_path)
    files_to_delete = os.listdir(folder_path)

    #do the deletion
    for file in files_to_delete:
        if os.path.isfile(file):
            os.remove(file)
