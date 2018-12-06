About
=
This is a sample a86 compatible assembly code generator written for SWE 514 class @ Bogazici University.

This console program reads every line from input filename, treats every line as infix expressions, and generates a86 compatible assembly source files which will evaluate the expression and output result in hex to stdout.

[Assingment details](https://github.com/sinag/Infix2Assembly/blob/master/Infix2Assembly/Documents/swe514fall2018proj.pdf)

[Project documentation]()

Compile (Windows)
=

Install Visual Studio 2017, open solution click start.

or from developer command prompt run

>msbuild Infix2Assembly.sln

Compile (Linux & MacOS)
=

Install complete [Mono](https://www.mono-project.com/download/stable/) framework

than run

>xbuild Infix2Assembly.sln

Usage
=
Usage: infix2assembly [filename] 

### Parameters:
   
   -h: Print help
   
Sample
=
   Process sample.txt with 3 line store huffman table as huffman.txt and encoded output as output.txt
   
   Input:
   
   >infix2assembly sample.txt
   
   Output :
   
   >Input File: sample.txt
   
   >File Written: line1.asm
   
   >File Written: line2.asm
   
   >File Written: line3.asm