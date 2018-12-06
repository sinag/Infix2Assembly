using System;
using System.Collections.Generic;

namespace Infix2Assembly
{
    public static class InfixParser
    {
        public static Stack<Token> ParseString(string strExpression)
        {
            strExpression = Tools.RemoveWhiteSpace(strExpression); // Remove possible unnecessary whitespace from input string

            Stack<Token> tokens = new Stack<Token>(); // Intermediate stack to hold tokens durng parse operation
            Stack<Token> InfixTokens = new Stack<Token>(); // Stack to hold parsed infix tokens
            Stack<Token> PostfixTokens = new Stack<Token>(); // Stack to hold converted postfix tokens
            string store = ""; // Temporary storage to hold numeric values
            
            Stack<Token> result = new Stack<Token>(); // Initialize method return value, null result means possible exception inside
            try
            {
                while (strExpression.Length>0) // While input string has characters
                {
                    var currentchar = strExpression.Substring(0, 1); // Get first character of string to parse
                    if (Tools.IsNumeric(currentchar)) // Check if this character is numeric
                    {
                        //tokens.Push(new Numeric(currentchar)); // Store numeric character on the intermediate stack
                        store += currentchar;
                    }
                    else if (Operator.GetOperatorType(currentchar) != null) // Check if this character is an operator
                    {
                        if (store != "") // If there is a numeric value in store
                        {
                            tokens.Push(new Numeric(store)); // Finish the numeric value and add it to stack
                            store = ""; // Clear store
                        }
                        tokens.Push(new Operator((OperatorTypes)Operator.GetOperatorType(currentchar))); // Store operator on the intermediate stack 
                    }
                    else if (Parenthesis.GetParenthesisType(currentchar) != null) // Check if this character is parenthesis
                    {
                        if (store != "") // If there is a numeric value in store
                        {
                            tokens.Push(new Numeric(store)); // Finish the numeric value and add it to stack
                            store = ""; // Clear store
                        }
                        tokens.Push(new Parenthesis((ParenthesisTypes)Parenthesis.GetParenthesisType(currentchar))); // Store parenthesis on the intermediate stack 
                    }
                    else
                    {
                        if ((store != "" && Tools.IsNumeric(strExpression.Substring(1, 1)))) // ToDo comment here
                        {
                            throw new Exception("Invalid Character In Expression: " + currentchar);
                        }
                    }
                    strExpression = strExpression.Substring(1); // Move to the next character
                }

                if (store != "") // If there is still something in the numeric store
                {
                    tokens.Push(new Numeric(store)); // Add it to the stack
                }

                InfixTokens = Tools.ReverseStack(tokens); // Reverse intermediate stack to get infix tokens

                Stack<Token> output = new Stack<Token>(); // Create a new stack which will hold numeric values
                Stack<Token> operators = new Stack<Token>(); // Create a new stack which will hold operator values

                while (InfixTokens.Count > 0) // While there is an infixtoken to parse
                {
                    Token currentToken = InfixTokens.Pop(); // Temporary object to hold current token
                    if (currentToken.GetType() == typeof(Operator)) // If current token is an operator
                    {
                        while (operators.Count > 0 && operators.Peek().GetType() == typeof(Operator)) // While we have operators and the next operator is not a parenthesis
                        {
                            Operator currentOperator = (Operator)currentToken; // Temporary object to hold current operator
                            Operator nextOperator = (Operator)operators.Peek(); // Temporary object to hold next operator
                            if (currentOperator.Precedence <= nextOperator.Precedence) // If current operator's precedence is less than or equal to next operator
                            {
                                output.Push(operators.Pop()); // Add the operator on top of operators stack to output stack
                            }
                            else
                            {
                                break;
                            }
                        }
                        operators.Push(currentToken); // Add current operator to operators stack
                    }
                    else if (currentToken.GetType() == typeof(Parenthesis)) // If current token is a parenthesis
                    {
                        switch (((Parenthesis)currentToken).ParenthesisType) // Switch parenthesis type
                        {
                            case ParenthesisTypes.Open: // If it's an opening bracket 
                                operators.Push(currentToken); // Add current token to operators
                                break;
                            case ParenthesisTypes.Close: // If it's a closing bracket
                                while (operators.Count > 0) // Shift operators in between opening to output 
                                {
                                    Token nextOperator = operators.Peek(); // Temporary object to hold next operator
                                    if (nextOperator.GetType() == typeof(Parenthesis) && ((Parenthesis)nextOperator).ParenthesisType == ParenthesisTypes.Open) break; // if next operator is a parenthesis of type opening bracket
                                    output.Push(operators.Pop()); // Add the operator on top of operators stack to output stack
                                }
                                operators.Pop(); // Remove the processed operator from stack
                                break;
                        }
                    }
                    else if (currentToken.GetType() == typeof(Numeric)) // If current token is numeric
                    {
                        output.Push(currentToken); // Add current numeric token to output stack
                    }
                }

                while (operators.Count > 0) // For all remaining operators
                {
                    output.Push(operators.Pop()); // Add current operator to output
                }

                PostfixTokens = Tools.ReverseStack(output); // Reverse intermediate stack to get postfix tokens
                result = PostfixTokens; // Assign parsed postfix tokens to result
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ParseString: {ex.Message}");
            }
            return result; // Return converted postfix tokens result
        }
    }
}
