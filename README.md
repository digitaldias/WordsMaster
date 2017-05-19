# WordsMaster
Sample program to show parallell execution using files. The program takes one parameter, the "subword" to find, then looks up all English words that contain this subword
and removes the subword from it. If the remaining characters still form a valid English word, the output is displayed. 

## Setting it up
- Make sure you have Visual Studio installed. Any version will do
- Clone this repository to your computer, open it, and compile the code (debug/release)
- in a command prompt, go to the bin/Debug (or bin/Release) folder, and type **WordsMaster lad**


Sample use: 

```
WordsMaster pi
```
Gives back a complete list of English words that contain "pi" and still is a valid english word once "pi" is removed (i.e. "Opinions" : "Onions")

>**NOTE<br />**
>The code is pointing to a relative path for the folder containing all words, so make sure to run this executable from the bin/debug or bin/Release folder. 


