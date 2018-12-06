using System;

namespace Infix2Assembly
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0 || args.Length > 1 || Tools.HelpRequired(args)) // Check for wrong or help arguments
                {
                    Tools.PrintUsage(); // Print usage
                }
                else
                {
                    Console.WriteLine($"Input File: {args[0]}"); // Output filename
                    if (Tools.CheckFileName(args[0])) // Check if filename is valid
                    {
                        var inputLines = Tools.ReadFile(args[0]); // Read lines from input file
                        if (inputLines.Count>0) // If we have expressions
                        {
                            int i = 1; // Initialize variable for filename iteration
                            foreach (var line in inputLines) // For every line
                            {
                                var postfixTokens = InfixParser.ParseString(line); // Create postfix tokens stack from string expression
                                if (postfixTokens.Count>0) // Check if there was an error or expression is empty
                                {
                                    var source = AssemblyGenerator.GenerateSource(postfixTokens); // Generate assembly source
                                    if (Tools.ExportSource($"line{i}.asm", source)) // Export source string as file
                                    {
                                        Console.WriteLine($"File Written: line{i}.asm"); // Display good result
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"File Skipped: line{i}.asm"); // Display bad result
                                }
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input File Is Empty");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Filename");
                        Tools.PrintUsage();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception In Main Thread: {ex.Message}");
            }
        }
    }
}

// Documentation
// Homework summary + assumptions
// Infix to Postfix algorithm
// https://www.includehelp.com/c/infix-to-postfix-conversion-using-stack-with-c-program.aspx
// http://csis.pace.edu/~wolf/CS122/infix-postfix.htm
// Assembly hex to ascii conversion & splitting eax using nibbles to use int21h
//0x0 + 0x30 = 0x30
//0x1 + 0x30 = 0x31
//0x2 + 0x30 = 0x32
//0x3 + 0x30 = 0x33
//0x4 + 0x30 = 0x34
//0x5 + 0x30 = 0x35
//0x6 + 0x30 = 0x36
//0x7 + 0x30 = 0x37
//0x8 + 0x30 = 0x38
//0x9 + 0x30 = 0x39
//0xA + 0x30 + 7 = 0x41
//0xB + 0x30 + 7 = 0x42
//0xC + 0x30 + 7 = 0x43
//0xD + 0x30 + 7 = 0x44
//0xE + 0x30 + 7 = 0x45
//0xF + 0x30 + 7 = 0x46
// UML diagram
// Usage
// compilation (mono)