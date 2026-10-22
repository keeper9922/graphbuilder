using System.Globalization;
using NCalc;
using MathEvaluation;
using MathEvaluation.Context;
using MathEvaluation.Extensions;
using MathEvaluation.Parameters;

namespace graphbuilder;

public static class Evaluator
{
    public static Func<double, double> Evaluate(string expression)
    {
        var context = new ScientificMathContext();
        var parameters = new MathParameters();
        return x =>
        {
            parameters.BindVariable(x);
            return Convert.ToDouble(expression.Evaluate(parameters, context));
        };
    }
    /*public static double EvaluateFormula(string formula, double xValue)
    {
        // Создаем выражение
        Expression expr = new Expression(formula);

        // Регистрируем базовые математические функции (Sin, Cos, Tan, Abs, Sqrt и т.д.)
        // По умолчанию NCalc понимает многие функции из Math (Sin, Cos, Tan, Pow, Abs)
        expr.Parameters["x"] = xValue;

        // Вычисляем значение
        var result = expr.Evaluate();

        return Convert.ToDouble(result);
    }*/
}