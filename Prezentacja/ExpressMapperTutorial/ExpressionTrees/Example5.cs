using System;
using System.Linq.Expressions;

namespace ExpressMapperTutorial.ExpressionTrees
{
    public static class Example5
    {
        public static void Show()
        {
            Expression<Func<int, bool>> exprTree = num => num < 5;

            // Decompose the expression tree.  
            ParameterExpression param = exprTree.Parameters[0];
            BinaryExpression operation = (BinaryExpression) exprTree.Body;
            ParameterExpression left = (ParameterExpression) operation.Left;
            ConstantExpression right = (ConstantExpression) operation.Right;

            Console.WriteLine($"Decomposed expression: {param.Name} => {left.Name} {operation.NodeType} {right.Value}");
        }
    }
}