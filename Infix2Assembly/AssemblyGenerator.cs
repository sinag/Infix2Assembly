using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Infix2Assembly
{
    public static class AssemblyGenerator
    {
        public static string GenerateSource(Stack<Token> postfixTokens)
        {
            StringBuilder sb = new StringBuilder(); // Create a new string builder object to hold generated assembly source
            try
            {
                sb.Append(CodeBegin()); // Append code segment beginning routine to assembly source
                foreach (var token in postfixTokens) // Process every token inside postfix tokens
                {
                    if (token.GetType() == typeof(Numeric)) // If current token is numeric
                    {
                        sb.Append(Push(token)); // Append assembly source to push this numeric value to stack
                    }
                    if (token.GetType() == typeof(Operator)) // If current token is an operator
                    {
                        switch (((Operator)token).OperatorType) // Switch operator type
                        {
                            case OperatorTypes.Plus: // If current operator is a Plus sign
                                sb.Append(Addition()); // Append Addition routine to assembly source 
                                break;
                            case OperatorTypes.Minus: // If current operator is a Minus sign
                                sb.Append(Subtraction()); // Append Subtraction routine to assembly source 
                                break;
                            case OperatorTypes.Multiply: // If current operator is a Multiply sign
                                sb.Append(Multiplication()); // Append Multiplication routine to assembly source 
                                break;
                            case OperatorTypes.Divide: // If current operator is a Divide sign
                                sb.Append(Division()); // Append Division routine to assembly source 
                                break;
                            default:
                                break;
                        }
                    }
                }
                sb.Append(CodeEnd()); // Append static assembly source to print output and code ends parts
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception In GenerateSource: {ex.Message}");
            }
            return sb.ToString();
        }
        public static string Addition() // Method to generate addition routine assembly code
        {
            var result = Environment.NewLine + $@"; addition
pop ax
pop bx
add ax,bx ; result in ax
push ax" + Environment.NewLine;
            return result;
        }
        public static string Subtraction() // Method to generate subtraction routine assembly code
        {
            var result = Environment.NewLine + $@"; subtraction
pop bx
pop ax
sub ax,bx ; result in ax
push ax" + Environment.NewLine;
            return result;
        }
        public static string Multiplication() // Method to generate multiplication routine assembly code
        {
            var result = Environment.NewLine + $@"; multiplication
pop ax
pop bx
mul bx ; result in ax
xor dx,dx ; clear dx, assume 16 bit result
push ax" + Environment.NewLine;
            return result;
        }
        public static string Division() // Method to generate division routine assembly code
        {
            var result = Environment.NewLine + $@"; division
pop bx
pop ax
xor dx,dx ; clear dx, assume 16 bit operand
div bx ; result in ax
push ax" + Environment.NewLine;
            return result;
        }
        public static string Push(Token token) // Method to generate assembly code to add numeric value to stack
        {
            var result = $@"push 0{token.ToString()}h" + Environment.NewLine;
            return result;
        }
        public static string CodeBegin() // Method to generate beginning part of assembly code
        {
            var result = $@"code segment" + Environment.NewLine + Environment.NewLine;

            return result;
        }
        public static string CodeEnd() // Method to generate ending part of assembly code
        {
            var result = Environment.NewLine + $@"pop ax
call print_word
int 020h ; exit to dos interrupt

print_word:
push ax; put the value of ax on top of stack
shr ax, 8 ; right shift value of ax 8 bits (get leftmost two nibbles)
call print_byte
pop ax; restore value of ax
push ax; put the value of ax on top of stack
and ax, 0ffh ; get last 8 bits of ax (get rightmost two nibbles)
call print_byte
pop ax; restore value of ax
ret ; return

print_byte:
push ax; put the value of ax on top of stack
shr al, 4 ; right shift value of al 4 bits(get left nibble)
call printchar
pop ax; restore value of ax
and al, 0fh ; get last 4 bits of al(get right nibble)
call printchar
ret ; return

printchar:
add al, 030h ; add hex value 30 to convert to ascii(0=30h, 9=39h)
cmp al, 039h ; compare value with ascii code for 9
jle printchar_letter; jump if value is less than or equal(ZF, CF)
add al, 07h ; add 7 to convert numbers bigger than 9 to ascii(A=41h)

printchar_letter:
mov dl, al; move character to write to dl
mov ah, 02h ; set int21h mode to write character to stdout
int 021h ; execute interrupt
ret ; return

code ends";
            return result;
        }
    }
}
