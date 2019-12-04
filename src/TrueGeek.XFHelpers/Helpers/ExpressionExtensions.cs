using System;
using System.Linq.Expressions;

namespace TrueGeek.XFHelpers.Helpers
{

    public static class ExpressionExtensions
    {

        // this is used by TGBaseViewModel.OnPropertyChanged
        public static string PropertyName<TProperty>(this Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            return memberExpression.Member.Name;
        }

    }

}
