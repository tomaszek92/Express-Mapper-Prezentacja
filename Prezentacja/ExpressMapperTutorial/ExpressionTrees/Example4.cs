using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressMapperTutorial.ExpressionTrees
{
    public static class Example4
    {
        public static void Show()
        {
            Func<int, int> factorialLambda = x =>
            {
                int result = 1;
                for (int i = 2; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            };

            ParameterExpression paramX = Expression.Parameter(typeof(int), "x");
            ParameterExpression paramResult = Expression.Parameter(typeof(int), "result");
            ParameterExpression paramI = Expression.Parameter(typeof(int), "i");

            LabelTarget label = Expression.Label(typeof(void));

            BlockExpression block = Expression.Block(
                new[] {paramResult, paramI},
                Expression.Assign(paramI, Expression.Constant(1)),
                Expression.Assign(paramResult, Expression.Constant(1)),
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThanOrEqual(paramI, paramX),
                        Expression.Assign(
                            paramResult,
                            Expression.Multiply(
                                paramResult,
                                Expression.PostIncrementAssign(paramI))),
                        Expression.Return(label, paramResult))
                ),
                Expression.Label(label),
                Expression.Assign(paramResult, paramResult));

            Func<int, int> dynamicLambdaFactorial = Expression.Lambda<Func<int, int>>(
                block, new List<ParameterExpression> {paramX}).Compile();
            Console.WriteLine(dynamicLambdaFactorial);

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{i}! = {factorialLambda(i)}, {dynamicLambdaFactorial(i)}");
            }
        }
    }
}