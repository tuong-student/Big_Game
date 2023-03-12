using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.RPGStats
{
    public interface IOperator<X>
    {
        X Create<T>(T other);
        X Sum(X a, X b);
        X Times(X a, X b);
        X Divide(X a, X b);
        X Negate(X a);
        X Max(X a, X b);
        X Min(X a, X b);
        X Set(X b);
        X zero { get; }
        X one { get; }
    }

    public interface ICompare<X>
    {
        bool IsGreater(X a, X b);
        bool IsLess(X a, X b);
        bool Equal(X a, X b);
        bool IsLessOrEqual(X a, X b);
        bool IsGreaterOrEqual(X a, X b);
    }

    public class RPGModifider
    {
        public static IOperator<S> GetOpType<S>()
        {
            switch (Type.GetTypeCode(typeof(S)))
            {
                case TypeCode.Double:
                    return (IOperator<S>)(object)default(OpDouble);
                case TypeCode.Single:
                    return (IOperator<S>)(object)default(OpFloat);
                case TypeCode.Int32:
                    return (IOperator<S>)(object)default(OpInt);
                default:
                    throw new NotImplementedException($"No IOperator<S> implementation for type {typeof(S)}.");
            }
        }

        public static ICompare<S> GetCompareType<S>()
        {
            switch (Type.GetTypeCode(typeof(S)))
            {
                case TypeCode.Double:
                    return (ICompare<S>)(object)default(CpDouble);
                case TypeCode.Single:
                    return (ICompare<S>)(object)default(CpFloat);
                case TypeCode.Int32:
                    return (ICompare<S>)(object)default(CpInt);
                default:
                    throw new NotImplementedException($"No ICompare<S> implementation for type {typeof(S)}.");
            }
        }

        internal struct OpFloat : IOperator<float>
        {
            public float Create<T>(T other) => Convert.ToSingle(other);
            public float Sum(float a, float b) => a + b;
            public float Times(float a, float b) => a * b;
            public float Divide(float a, float b) => a / b;
            public float Negate(float a) => -a;
            public float Max(float a, float b) => Math.Max(a, b);
            public float Min(float a, float b) => Math.Min(a, b);
            public float Set(float b) => b;
            public float zero => 0f;
            public float one => 1f;
        }

        internal struct OpDouble : IOperator<double>
        {
            public double Create<T>(T other) => Convert.ToDouble(other);
            public double Sum(double a, double b) => a + b;
            public double Times(double a, double b) => a * b;
            public double Divide(double a, double b) => a / b;
            public double Negate(double a) => -a;
            public double Max(double a, double b) => Math.Max(a, b);
            public double Min(double a, double b) => Math.Min(a, b);
            public double Set(double b) => b;
            public double zero => 0.0;
            public double one => 1.0;
        }

        internal struct OpInt : IOperator<int>
        {
            public int Create<T>(T other) => Convert.ToInt32(other);
            public int Sum(int a, int b) => a + b;
            public int Times(int a, int b) => a * b;
            public int Divide(int a, int b) => a / b;
            public int Negate(int a) => -a;
            public int Max(int a, int b) => Math.Max(a, b);
            public int Min(int a, int b) => Math.Min(a, b);
            public int Set(int b) => b;
            public int zero => 0;
            public int one => 1;
        }

        internal struct CpFloat : ICompare<float>
        {
            public bool Equal(float a, float b) => a == b;
            public bool IsGreater(float a, float b) => a > b;
            public bool IsGreaterOrEqual(float a, float b) => a >= b;
            public bool IsLess(float a, float b) => a < b;
            public bool IsLessOrEqual(float a, float b) => a <= b;
        }

        internal struct CpDouble : ICompare<double>
        {
            public bool Equal(double a, double b) => a == b;
            public bool IsGreater(double a, double b) => a > b;
            public bool IsGreaterOrEqual(double a, double b) => a >= b;
            public bool IsLess(double a, double b) => a < b;
            public bool IsLessOrEqual(double a, double b) => a <= b;
        }

        internal struct CpInt : ICompare<int>
        {
            public bool Equal(int a, int b) => a == b;
            public bool IsGreater(int a, int b) => a > b;
            public bool IsGreaterOrEqual(int a, int b) => a >= b;
            public bool IsLess(int a, int b) => a < b;
            public bool IsLessOrEqual(int a, int b) => a <= b;
        }
    }
}
