# FileSortRenamer2
Rename and sort files from a directory quicker than before with the help of powerful variables and more

## Step 1 : Start the software
You can run the program by simply starting it with a double click on the exe file (or the executing file on another system, since it is crossplatform compatible).

## Step 2 : Enter the path of the directory
The programs asks you for the path of the directory with all the files. (Before you continue, it is recommended to make a little backup.)

## Step 3 : The magic of patterns
You probably already heard from them. If not, here's a little explaination. Patterns are a kind of search which files you want to select.
Let's say you just want to rename all files with the name "Hello.jpg". Then you write exactly that into the pattern. It will only return one file, because that will select the exact name that you entered. To get like all jpg files, you can type \*.jpg. The little star means, that you want to select all files that starts with anything and end with .jpg. Keep the patterns empty to just select everything.

## Step 4 : Create a file name
Each file has different informations. With the help of variables, you can include this informations into the name of the file.
An example is:
Input: example\_file\_$[i]\_$[fcyyy]\_$[fcmm].$[fext]
Output file1: example\_file\_0\_2022\_03.jpg
Output file2: example\_file\_1\_2022\_04.jpg
Output file1: example\_file\_2\_2022\_05.jpg
In that case, the "i" represents a counter, which counts the files up starting from the number that you choose later and "fcyyy" is the year when the file got created and "fcmm" the month.
It is important to also add back the file extension, otherwise you will lose that one after renaming. The extension can be different for each file, except you selected only jpg files, then it is only jpg. The extension is saved in "fext". There is no special way in connecting text + variables as long as the variable is correct and you write it into $[nameOfVariable]. Hello$[i] is valid and Hello$$$$$$[i] is valid and Hello_$[i] is valid and so on ...

## Step 5 : Check your created name
This step is important. Better check twice before you rename all files and break your files. Type yes, if everything is correct or no to go back to the name builder.

## Step 6 : Sorting
All files were loaded by alphabet (A-Z). That way, the program will rename them. So the first element starting with an A for example will be 0, while B will be 1 if you include the "i" variable. The step 7 shows up, if you agree to this step (6) otherwise it will skip to step 8.

## Step 7 : Sorting 2.0
As we already talked about sorting the files, this step allows you to sort the files by name, extension, size, creationdate, editdate. Editdate represents the date when the file was written the last time.

## Step 8 : Start counter. You can ignore this, if you didn't added a counter variable. Otherwise you can add, where the variable should start to count. An example would be 0 or 1 or maybe 2. You can also leave it empty. The default value is 0.

## Step 9 : Check if you created a name, that makes all names unique. Let's say you selected 2 files and typed in "Hello.jpg" as a name. Now it will rename one file to "Hello.jpg", but for the other files it will fail, since the file already exist. If everything is correct, then the programm will finish everything else and tell you, when it is done.
