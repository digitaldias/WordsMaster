
# WordsMaster.Exe
Sample program to show parallell execution using files. The program takes one parameter, the "subword" to find, then looks up all English words that contain this subword
and removes the subword from it. If the remaining characters still form a valid English word, the output is displayed. Original idea from a meme like this:  

<center>

![Words to the vise](opinion.jpg)

</center>

## Setting it up
- Make sure you have Visual Studio installed. Any version will do
- Clone this repository to your computer, open it, and compile the code (debug/release)
- in a command prompt, go to the bin/Debug (or bin/Release) folder, and type, for example,  **WordsMaster lad**
- The code comes with a ready made list of English words, however, if you can find a more extensive list, you can use the project **CreateWordFiles** to put them into the expected form.




Sample use: 

```
F:\Dev\Private\WordsMaster\bin\Release> WordsMaster.exe lad
```
Produces the following output:
```
WORDSMASTER v1.0.0.0
2016-2017 - digitaldias


Loaded 128985 words in 15ms.
225 candidates resulted in 21 matches. This took 4 ms

RESULTS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ballade:bale             ballades:bales           blade:be                 bladed:bed               charlady:chary           cladist:cist             cladistic:cistic
cladists:cists           cladode:code             cladodes:codes           defiladed:defied         defilades:defies         fusillade:fusile         malady:may
overladed:overed         overlading:overing       paladin:pain             paladins:pains           salade:sae               sladang:sang
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Output written in 1ms
Program finished.
```
Gives back a complete list of English words that contain "lad" and still is a valid english word once "lad" is removed (i.e. "Paladin" : "Pain")

>**NOTE<br />**
>The code is pointing to a relative path for the folder containing all words, so make sure to run this executable from the bin/debug or bin/Release folder. 

## CreateWordFiles.Exe
Takes a single file as input + an output folder path. Will read all words contained in the input file, and create 
ouput files on the form "A Words.txt", "B Words.txt" ... "Z Words.txt". 

The input file is expected to contain all english words, split by a carriage return between each word.


