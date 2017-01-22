using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressMapperTutorial.ExpressionTrees
{
    public static class Example1
    {
        public static void Show()
        {
            Func<int, int, bool> lambdaEqual = (x, y) => x == y;
            Console.WriteLine(lambdaEqual);

            ParameterExpression parameterX = Expression.Parameter(typeof(int), "x");
            ParameterExpression parameterY = Expression.Parameter(typeof(int), "y");
            LambdaExpression dynamicLambdaEqual = Expression.Lambda(
                body: Expression.Equal(parameterX, parameterY),
                parameters: new List<ParameterExpression> {parameterX, parameterY});
            Console.WriteLine(dynamicLambdaEqual);

            int a = 10;
            int b = 5;
            Console.WriteLine(lambdaEqual(a, b));
            Console.WriteLine(dynamicLambdaEqual.Compile().DynamicInvoke(a, b));

            a = 6;
            b = 6;
            Console.WriteLine(lambdaEqual(a, b));
            Console.WriteLine(dynamicLambdaEqual.Compile().DynamicInvoke(a, b));
        }
    }
}