using System;

namespace Infix2Assembly
{
    public enum TokenTypes
    {
        Operator,
        Number,
        Parenthesis
    }
    public enum OperatorTypes
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }
    public enum ParenthesisTypes
    {
        Open,
        Close
    }
    public class Token { }
    public class Operator : Token
    {
        public Operator(OperatorTypes operatorType) { OperatorType = operatorType; } // Constructor for operator class
        public OperatorTypes OperatorType { get; set; } // Member to hold operator type
        public int Precedence // Member to return precedence based on operator type, used inside infix parse algorithm
        {
            get
            {
                switch (OperatorType)
                {
                    case OperatorTypes.Multiply:
                    case OperatorTypes.Divide:
                        return 2;
                    case OperatorTypes.Plus:
                    case OperatorTypes.Minus:
                        return 1;
                    default:
                        throw new Exception("Invalid Operator Type While Calculating Precedence");
                }
            }
        }

        public override string ToString()
        {
            switch (OperatorType)
            {
                case OperatorTypes.Plus: return "+";
                case OperatorTypes.Minus: return "-";
                case OperatorTypes.Multiply: return "*";
                case OperatorTypes.Divide: return "/";
                default: return null;
            }
        } // Used to return string representation of object data
        public static OperatorTypes? GetOperatorType(string operatorValue)
        {
            switch (operatorValue)
            {
                case "+": return OperatorTypes.Plus;
                case "-": return OperatorTypes.Minus;
                case "*": return OperatorTypes.Multiply;
                case "/": return OperatorTypes.Divide;
                default: return null;
            }
        } // Returns operator type from character, used inside infix parse algorithm
    }
    class Parenthesis : Token
    {
        public Parenthesis(ParenthesisTypes parenthesisType) { ParenthesisType = parenthesisType; } // Constructor for parenthesis class
        public ParenthesisTypes ParenthesisType { get; set; } // Member to hold parenthesis type
        public override string ToString()
        {
            if (ParenthesisType == ParenthesisTypes.Open)
            {
                return "(";
            }
            else
            {
                return ")";
            }
        } // Used to return string representation of object data
        public static ParenthesisTypes? GetParenthesisType(string parenthesisValue)
        {
            switch (parenthesisValue)
            {
                case "(": return ParenthesisTypes.Open;
                case ")": return ParenthesisTypes.Close;
                default: return null;
            }
        }// Returns parenthesis type from character, used inside infix parse algorithm
    }
    class Numeric : Token
    {
        public Numeric(string value) { Value = value; } // Constructor for Numeric class
        public string Value { get; set; } // Member to hold numeric value
        public override string ToString() { return Value.ToString(); } // Used to return string representation of object data
    }
}
