using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Infix2Assembly
{
    public static class Tools
    {
        public static string RemoveWhiteSpace(string strInput) // Replace whitespace from input string and return as result
        {
            return strInput.Replace(" ", "");
        }
        public static bool IsNumeric(string strInput) // Check if input string is a valid hex number (Regex match for (0123456789ABCDEF))
        {
            var result = false;
            if (Regex.IsMatch(strInput, "[a-fA-F0-9]"))
            {
                result = true;
            }
            return result;
        }
        public static Stack<Token> ReverseStack(Stack<Token> inputStack) // Reverse input stack object and return as result
        {
            Stack<Token> reversedStack = new Stack<Token>();
            while (inputStack.Count > 0)
            {
                reversedStack.Push(inputStack.Pop());
            }
            return reversedStack;
        }
        public static bool HelpRequired(string[] arguments) // Check if help argument is present
        {
            var result = false;
            foreach (var argument in arguments)
            {
                if (argument == "-h" || argument == "--help" || argument == "/?")
                {
                    result = true;
                }
            }
            return result;
        }
        public static void PrintUsage() // Print usage to console
        {
            Console.WriteLine("Infix2Assembly 1.0");
            Console.WriteLine("Usage: infix2assembly [filename]");
            Console.WriteLine("Reads input file line-by-line and converts infix expressions found in every line to a86 assembly source files");
        }
        public static bool ExportSource(string strFilename, string strSource) // Save generated assembly source as file
        {
            var result = false;
            try
            {
                File.WriteAllText(strFilename, strSource);
                result = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception in ExportSource Filename: {strFilename} Message: {ex.Message}");
            }
            return result;
        }
        public static List<string> ReadFile(string strFilename) // Read input file and return list of lines
        {
            List<string> result = new List<string>();
            try
            {
                result = File.ReadLines(strFilename).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ReadFile: {ex.Message}");
            }
            return result;
        }
        public static bool CheckFileName(string strFilename) // Validates string as filename
        {
            var result = false;
            if (strFilename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1)
            {
                result = true;
            }
            return result;
        }
    }
}
