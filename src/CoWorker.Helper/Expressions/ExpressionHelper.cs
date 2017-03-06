namespace System.Linq.Expressions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using CoWorker.Helper;
    using CoWorker.Helper.Expressions;

    public static class ExpressionHelper
    {
        public static ParameterExpression ToParameter(this Type type,string name = null) 
            => string.IsNullOrWhiteSpace(name) ? Expression.Parameter(type) : Expression.Parameter(type,name);

        public static Expression GroupWithBlock(this Expression expression,params Expression[] exps)
            => Expression.Block(new[] { expression }.Union(exps.DefaultIfEmpty(Expression.Default(typeof(object)))));

        public static Expression GroupWithBlock(this IEnumerable<Expression> expressions)
            => Expression.Block(expressions);

        public static Expression AssignFrom(this Expression left, Expression right)
            => Expression.Assign(left, right);

        public static Expression AssignTo(this Expression right,Expression left)
            => Expression.Assign(left,right);

        public static Expression ConvertTo(this Expression expression, Type type = null)
            => Expression.Convert(expression, type ?? typeof(object));

        public static Expression AsTypeTo(this Expression expression, Type type = null)
            => Expression.TypeAs(expression, type ?? typeof(object));

        public static Expression ToConstant(this object obj)
            => Expression.Constant(obj);

        public static Expression ToNullConstant(this Type type)
            => Expression.Constant(null,type);

        public static Expression Invoke(this Expression expression,params Expression[] args)
            => Expression.Invoke(expression,args);

        public static Expression BinaryWith(this Expression left,ExpressionType operation,Expression right)
            => Expression.MakeBinary(operation,left,right);

        public static Expression UnarySelf(this Expression expression,ExpressionType operation,Type type)
            => Expression.MakeUnary(operation,expression,type);

        public static Expression UnarySelf(this Expression expression,ExpressionType operation,Type type,MethodInfo method)
            => Expression.MakeUnary(operation,expression,type,method);

        public static Expression GetByIndex(this Expression expression,params Expression[] indexer)
            => Expression.ArrayIndex(expression,indexer);

        public static Expression InvokeWith(this Expression expression,params Expression[] args)
            => Expression.Invoke(expression,args);


        public static Expression EqualWith(this Expression left, Expression right, bool liftToNull, MethodInfo method)
            => Expression.Equal(left, right, liftToNull, method);

        public static Expression EqualWith(this Expression left, Expression right)
            => Expression.Equal(left, right);

        public static IEnumerable<Expression> GetPropertiesOrFields(this Expression expression, IEnumerable<string> names)
            => names.Select(x => expression.GetPropertyOrField(x));

        public static Expression JoinWith(this IEnumerable<Expression> expressions,Func<Expression,Expression,Expression> combine)
            => expressions.Aggregate((left, right) => combine(left, right));

        public static Expression StaticMethodCall(this MethodInfo method, params Expression[] args)
            => Expression.Call(method, args);

        public static Expression MethodCall(this Expression expression, MethodInfo method, params Expression[] args)
            => Expression.Call(method.IsStatic ? null : expression, method, args);

        public static Expression MethodCall(this Expression expression, string name, params Expression[] args)
            => MethodCall(expression,expression.Type.GetRuntimeMethod(name, args.Select(x => x.Type).ToArray()), args);

        private static Expression MethodReturnValue(this MethodCallExpression expression)
            => expression.Method.ReturnType == typeof(void) ? expression.MethodReturnVoid() : expression;

        private static Expression MethodReturnVoid(this MethodCallExpression expression)
            => expression.GroupWithBlock();

        public static Expression GetPropertyOrField(this Expression expression, string name)
            => expression.Type.GetTypeInfo()
                .DeclaredMembers
                .Where(x => x.Name == name)
                .Select(x => Expression.MakeMemberAccess(expression, x))
                .FirstOrDefault();
        
        public static Expression GetMember(this Expression expression, MemberInfo member)
            => Expression.MakeMemberAccess(expression, member.NotNull($"member {member.Name} is not exist"));

        public static BlockExpression Visit(this BlockExpression block, Action<IExpressionVisitor> visitor)
            => Expression.Block(visitor.CreateVisitor().Visit(block));

        public static Expression Visit(this Expression expression, Action<IExpressionVisitor> visitor)
            => visitor.CreateVisitor().Visit(expression);

        public static IEnumerable<Expression> Visit(this IEnumerable<Expression> expression, Action<IExpressionVisitor> visitor)
            => visitor.CreateVisitor().Visit(Expression.Block(expression).Expressions);

        private static ExpressionVisitorTemplate CreateVisitor(this Action<IExpressionVisitor> config)
        {
            var visitor = ExpressionVisitorTemplate.New;
            config(visitor);
            return visitor as ExpressionVisitorTemplate;
        }

        public static LambdaExpression MakeLambda(this ParameterExpression expression, IEnumerable<ParameterExpression> args, Func<ParameterExpression[], Expression> script)
            => Expression.Lambda(script(args.Prepend(expression).ToArray()), new ParameterExpression[] { expression }.Union(args));

        public static LambdaExpression MakeLambda(this ParameterExpression expression, IEnumerable<Type> args, Func<ParameterExpression[], Expression> script)
            => expression.MakeLambda(args.Select(x => x.ToParameter()), script);
        
        public static Expression<TDelegate> MakeLambda<TDelegate>(this ParameterExpression expression, IEnumerable<Type> args, Func<ParameterExpression[], Expression> script)
            where TDelegate : class
            => expression.MakeLambda(args, script) as Expression<TDelegate>;

        public static Expression<TDelegate> MakeLambda<TDelegate>(this ParameterExpression expression, Type args, Func<ParameterExpression[], Expression> script)
            where TDelegate : class
            => expression.MakeLambda<TDelegate>(Enumerable.Empty<Type>().Append(args), script);

        public static Expression<TDelegate> MakeLambda<TDelegate>(this ParameterExpression expression, Func<ParameterExpression, Expression> script)
            where TDelegate : class
            => expression.MakeLambda(script) as Expression<TDelegate>;

        public static LambdaExpression MakeLambda(this ParameterExpression expression, Func<ParameterExpression, Expression> script)
            => Expression.Lambda(script(expression), expression);
    }
}


