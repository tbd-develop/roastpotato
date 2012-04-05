using System.Linq;
using System.Linq.Expressions;

namespace RoastPotato.Recipes.Infrastructure
{
    /*
         * Credit: http://stackoverflow.com/users/390278/jeff-mercado
         * http://stackoverflow.com/questions/5744764/linq-to-sql-throwing-a-stackoverflowexception/5751931#5751931
         * */

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        protected ParameterRebinder(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }
        public Expression Rebind(Expression expr)
        {
            return Visit( expr );
        }
        public static Expression Rebind(Expression expr, ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            return new ParameterRebinder( oldParameter, newParameter ).Visit( expr );
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return ( node.Name == _oldParameter.Name ) ? _newParameter : node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            return node.Update( Visit( node.Expression ) );
        }
        protected override Expression VisitIndex(IndexExpression node)
        {
            return node.Update( Visit( node ), Visit( node.Arguments ) );
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return node.Update( Visit( node.Operand ) );
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            return node.Update( Visit( node.Left ), node.Conversion, Visit( node.Right ) );
        }
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return node.Update( Visit( node.Test ), Visit( node.IfTrue ), Visit( node.IfFalse ) );
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return node.Update( Visit( node.Object ), Visit( node.Arguments ) );
        }
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            return node.Update( Visit( node.Expression ), Visit( node.Arguments ) );
        }
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return node.Update( Visit( node.Body ), node.Parameters.Select( param => ( ParameterExpression )Visit( param ) ) );
        }
    }
}