using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressMapperTutorial.ExpressionTrees
{
    public static class Example2
    {
        public static void Show()
        {
            Func<int, bool> lessThanFiveLambda = num => num < 5;

            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lessThanFiveDynamicLambda = Expression.Lambda<Func<int, bool>>(
                numLessThanFive, new List<ParameterExpression> {numParam});
            Console.WriteLine(lessThanFiveDynamicLambda);

            Console.WriteLine(lessThanFiveLambda(5));
            Console.WriteLine(lessThanFiveDynamicLambda.Compile()(5));
            Console.WriteLine(lessThanFiveLambda(4));
            Console.WriteLine(lessThanFiveDynamicLambda.Compile()(4));
        }
    }
}