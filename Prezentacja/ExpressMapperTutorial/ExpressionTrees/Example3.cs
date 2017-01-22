using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressMapperTutorial.ExpressionTrees
{
    public static class Example3
    {
        public static void Show()
        {
            Func<double, double, double, double> deltaLambda = (a, b, c) => b*b - 4*a*c;

            ParameterExpression paramA = Expression.Parameter(typeof(double), "a");
            ParameterExpression paramB = Expression.Parameter(typeof(double), "b");
            ParameterExpression paramC = Expression.Parameter(typeof(double), "c");

            LambdaExpression dynamicLambdaDelta = Expression.Lambda(
                Expression.Subtract(
                    left: Expression.Multiply(paramB, paramB),
                    right: Expression.Multiply(
                        left: Expression.Constant(4d, typeof(double)),
                        right: Expression.Multiply(paramA, paramC))),
                new List<ParameterExpression> {paramA, paramB, paramC});

            Console.WriteLine(dynamicLambdaDelta);

            double _a = 2;
            double _b = 3;
            double _c = -4;

            Console.WriteLine(deltaLambda(_a, _b, _c));
            Console.WriteLine(dynamicLambdaDelta.Compile().DynamicInvoke(_a, _b, _c));
        }
    }
}