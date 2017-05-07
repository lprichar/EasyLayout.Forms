//
// Copyright (c) 2017 Lee P. Richardson
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace EasyLayout.Forms
{
    public static class EasyLayoutForms
    {
        private struct Margin
        {
            public int? Right { get; set; }
            public int? Left { get; set; }
            public int? Top { get; set; }
            public int? Bottom { get; set; }
        }

        private enum ConstraintType
        {
            X,
            Y,
            Height,
            Width,
        }

        private class Rule
        {
            public Rule(View view)
            {
                View = view;
            }

            public View View { get; }
            public Constraint Constraint { get; private set; }
            public Guid? RelativeToViewId { get; private set; }
            public Margin Margin { get; private set; }
            public int? Width { get; private set; }
            public int? Height { get; private set; }
            public ConstraintType ConstraintType { get; set; }

            public void Initialize(LeftExpression leftExpression, RightExpression rightExpression)
            {
                if (!rightExpression.IsParent)
                {
                    RelativeToViewId = rightExpression.Id;
                }
                SetMargin(leftExpression, rightExpression);
                SetLayoutRule(leftExpression, rightExpression);
                SetHeightWidth(leftExpression, rightExpression);
            }

            private void SetHeightWidth(LeftExpression leftExpression, RightExpression rightExpression)
            {
                // todo: support height/width constraints
                if (leftExpression.Position == Position.Width && rightExpression.IsConstant && rightExpression.Constant.HasValue)
                    Width = rightExpression.Constant.Value;
                if (leftExpression.Position == Position.Height && rightExpression.IsConstant && rightExpression.Constant.HasValue)
                    Height = rightExpression.Constant.Value;
                //if (leftExpression.Position == Position.Width && rightExpression.IsParent && rightExpression.Constant == null)
                //    Width = ViewGroup.LayoutParams.MatchParent;
                //if (leftExpression.Position == Position.Height && rightExpression.IsParent && rightExpression.Constant == null)
                //    Height = ViewGroup.LayoutParams.MatchParent;
            }

            private void SetLayoutRule(LeftExpression leftExpression, RightExpression rightExpression)
            {
                var layoutRule = GetLayoutRule(leftExpression, rightExpression);
                ConstraintType = layoutRule.Item1;
                Constraint = layoutRule.Item2;
            }

            private static Tuple<ConstraintType, Constraint> GetLayoutRule(LeftExpression leftExpression, RightExpression rightExpression)
            {
                if (rightExpression.IsConstant)
                {
                    return null;
                }
                if (rightExpression.IsParent)
                {
                    return GetLayoutRuleForParent(leftExpression.Position, rightExpression.Position, leftExpression.Name);
                }
                return GetLayoutRuleForSibling(leftExpression.Position, rightExpression.Position,
                    leftExpression.Name, rightExpression.Name);
            }

            private static Tuple<ConstraintType, Constraint> GetLayoutRuleForParent(Position childPosition, Position parentPosition, string childName)
            {
                if (childPosition == Position.Top && parentPosition == Position.Top)
                    return new Tuple<ConstraintType, Constraint>(ConstraintType.Y, Constraint.RelativeToParent(p => p.Y));
                if (childPosition == Position.Right && parentPosition == Position.Right)
                    return new Tuple<ConstraintType, Constraint>(ConstraintType.X, Constraint.RelativeToParent(p => p.Width));
                if (childPosition == Position.Bottom && parentPosition == Position.Bottom)
                    return new Tuple<ConstraintType, Constraint>(ConstraintType.Y, Constraint.RelativeToParent(p => p.Height));
                if (childPosition == Position.Left && parentPosition == Position.Left)
                    return new Tuple<ConstraintType, Constraint>(ConstraintType.X, Constraint.RelativeToParent(p => p.X));
                // todo: support more parent layout constraints
                //if (childPosition == Position.CenterX && parentPosition == Position.CenterX)
                //    return LayoutRules.CenterHorizontal;
                //if (childPosition == Position.CenterY && parentPosition == Position.CenterY)
                //    return LayoutRules.CenterVertical;
                //if (childPosition == Position.Center && parentPosition == Position.Center)
                //    return LayoutRules.CenterInParent;
                //if (childPosition == Position.Width && parentPosition == Position.Width)
                //    return null;
                //if (childPosition == Position.Height && parentPosition == Position.Height)
                //    return null;
                throw new Exception($"Unsupported parent positioning combination: {childName}.{childPosition} with parent.{parentPosition}");
            }

            private static Tuple<ConstraintType, Constraint> GetLayoutRuleForSibling(Position leftPosition, Position rightPosition, string leftExpressionName, string rightExpressionName)
            {
                // todo: support sibling layout constraints
                throw new NotImplementedException("Sibling layout in progress");
                //if (leftPosition == Position.Bottom && rightPosition == Position.Bottom)
                //    return LayoutRules.AlignBottom;
                //if (leftPosition == Position.Top && rightPosition == Position.Top)
                //    return LayoutRules.AlignTop;
                //if (leftPosition == Position.Right && rightPosition == Position.Right)
                //    return LayoutRules.AlignRight;
                //if (leftPosition == Position.Left && rightPosition == Position.Left)
                //    return LayoutRules.AlignLeft;
                //if (leftPosition == Position.Top && rightPosition == Position.Bottom)
                //    return LayoutRules.Below;
                //if (leftPosition == Position.Bottom && rightPosition == Position.Top)
                //    return LayoutRules.Above;
                //if (leftPosition == Position.Left && rightPosition == Position.Right)
                //    return LayoutRules.RightOf;
                //if (leftPosition == Position.Right && rightPosition == Position.Left)
                //    return LayoutRules.LeftOf;
                //if (leftPosition == Position.Baseline && rightPosition == Position.Baseline)
                //    return LayoutRules.AlignBaseline;
                //if (leftPosition == Position.Width && rightPosition == Position.Width)
                //    throw new ArgumentException("Unfortunatly Android's relative layout isn't sophisticated enough to allow constraining widths.  You might be able to achieve the same result by constraining Left's and Right's.");
                //if (leftPosition == Position.Height && rightPosition == Position.Height)
                //    throw new ArgumentException("Unfortunatly Android's relative layout isn't sophisticated enough to allow constraining heights.  You might be able to achieve the same result by constraining Top's and Bottom's.");
                //throw new ArgumentException($"Unsupported relative positioning combination: {leftExpressionName}.{leftPosition} with {rightExpressionName}.{rightPosition}");
            }

            private void SetMargin(LeftExpression leftExpression, RightExpression rightExpression)
            {
                Margin = GetMargin(leftExpression, rightExpression);
            }

            private Margin GetMargin(LeftExpression leftExpression, RightExpression rightExpression)
            {
                if (rightExpression.Constant == null) return new Margin();
                var value = rightExpression.Constant.Value;

                switch (leftExpression.Position)
                {
                    case Position.Top:
                        return new Margin { Top = value };
                    case Position.Baseline:
                        return new Margin { Top = value };
                    case Position.Right:
                        return new Margin { Right = -value };
                    case Position.Bottom:
                        return new Margin { Bottom = -value };
                    case Position.Left:
                        return new Margin { Left = value };
                    case Position.Width:
                        return new Margin();
                    case Position.Height:
                        return new Margin();
                    case Position.CenterX:
                        return value > 0 ?
                            new Margin { Left = value } :
                            new Margin() { Right = -value };
                    default:
                        throw new ArgumentException($"Constant expressions with {rightExpression.Position} are currently unsupported.");
                }
            }

        }

        private enum Position
        {
            Top,
            Right,
            Left,
            Bottom,
            CenterX,
            CenterY,
            Center,
            Constant,
            Width,
            Height,
            Baseline
        }

        private struct LeftExpression
        {
            public View View { get; set; }
            public Position Position { get; set; }
            public string Name { get; set; }
        }

        private struct RightExpression
        {
            public bool IsParent { get; set; }
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Position Position { get; set; }
            public int? Constant { get; set; }
            public bool IsConstant => !IsParent && Id == null && Constant != null;
        }

        public static int ToConst(this int i)
        {
            return i;
        }

        public static int ToConst(this float i)
        {
            return (int)i;
        }

        public static void ConstrainLayout(this RelativeLayout relativeLayout, Expression<Func<bool>> constraints)
        {
            var constraintExpressions = FindConstraints(constraints.Body);
            var viewAndRule = ConvertConstraintsToRules(relativeLayout, constraintExpressions);
            UpdateLayoutParamsWithRules(relativeLayout, viewAndRule);
        }

        private static void UpdateLayoutParamsWithRules(RelativeLayout relativeLayout, IEnumerable<Rule> viewAndRule)
        {
            var viewsGroupedByRule = viewAndRule.GroupBy(i => i.View);

            foreach (var viewAndRules in viewsGroupedByRule)
            {
                var view = viewAndRules.Key;

                var xConstraint = GetConstraint(viewAndRules, ConstraintType.X);
                var yConstraint = GetConstraint(viewAndRules, ConstraintType.Y);
                var widthConstraint = GetConstraint(viewAndRules, ConstraintType.Width);
                var heightConstraint = GetConstraint(viewAndRules, ConstraintType.Height);

                relativeLayout.Children.Add(view, xConstraint, yConstraint, widthConstraint, heightConstraint);
            }
        }

        private static Constraint GetConstraint(IGrouping<View, Rule> viewsGroupedByRule, ConstraintType constraintType)
        {
            return viewsGroupedByRule.FirstOrDefault(i => i.ConstraintType == constraintType)?.Constraint;
        }

        private static IEnumerable<Rule> ConvertConstraintsToRules(RelativeLayout relativeLayout, List<BinaryExpression> constraintExpressions)
        {
            return constraintExpressions.Select(i => GetViewAndRule(i, relativeLayout));
        }

        private static Rule GetViewAndRule(BinaryExpression expr, RelativeLayout relativeLayout)
        {
            var leftExpression = ParseLeftExpression(expr.Left);
            var rightExpression = ParseRightExpression(expr.Right, relativeLayout);
            return GetRule(leftExpression, rightExpression);
        }

        private static Rule GetRule(LeftExpression leftExpression, RightExpression rightExpression)
        {
            var rule = new Rule(leftExpression.View);
            rule.Initialize(leftExpression, rightExpression);
            return rule;
        }

        private static RightExpression ParseRightExpression(Expression expr, RelativeLayout relativeLayout)
        {
            Position? position = null;
            Expression memberExpression = null;
            int? constant = null;

            if (expr.NodeType == ExpressionType.Add || expr.NodeType == ExpressionType.Subtract)
            {
                var rb = (BinaryExpression)expr;
                if (IsConstant(rb.Left))
                {
                    throw new ArgumentException("Addition and substraction are only supported when there is a view on the left and a constant on the right");
                }
                if (IsConstant(rb.Right))
                {
                    constant = ConstantValue(rb.Right);
                    if (expr.NodeType == ExpressionType.Subtract)
                    {
                        constant = -constant;
                    }
                    expr = rb.Left;
                }
                else
                {
                    throw new NotSupportedException("Addition only supports constants: " + rb.Right.NodeType);
                }
            }

            if (IsConstant(expr))
            {
                position = Position.Constant;
                constant = ConstantValue(expr);
            }
            else
            {
                // todo: Do we want to support label.Right() instead of label.Bounds.Right?
                //var fExpr = expr as MethodCallExpression;
                //if (fExpr != null)
                //{
                //    position = GetPosition(fExpr);
                //    memberExpression = fExpr.Arguments.FirstOrDefault() as MemberExpression;
                //}
            }

            if (position == null)
            {
                var memExpr = expr as MemberExpression;
                if (memExpr == null)
                {
                    throw new NotSupportedException("Right hand side of a relation must be a member expression, instead it is " + expr);
                }

                position = GetPosition(memExpr);

                memberExpression = memExpr.Expression;
                if (memExpr.Expression == null)
                {
                    throw new NotSupportedException("Constraints should use views's Top, Bottom, etc properties, or extension methods like GetCenter().");
                }
            }

            View view = GetView(memberExpression);
            var memberName = GetName(memberExpression);
            var isParent = view == relativeLayout;

            // todo: Do we need to add Id's for relative layout's between views to work?
            //if (view != null && !isParent && view.Id == -1)
            //{
            //    view.Id = GenerateViewId();
            //}

            return new RightExpression
            {
                IsParent = isParent,
                Id = view?.Id,
                Position = position.Value,
                Name = memberName,
                Constant = constant
            };
        }

        private static string GetName(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression != null) return memberExpression.Member.Name;
            return expression?.ToString();
        }

        private static View GetView(Expression viewExpr)
        {
            if (viewExpr == null) return null;
            var memberExpression = viewExpr as MemberExpression;
            if (memberExpression != null)
            {
                var viewExpression = memberExpression.Expression as MemberExpression;
                var eval = Eval(viewExpression);
                var view = eval as View;
                if (view == null)
                {
                    throw new NotSupportedException("Constraints only apply to View's.");
                }
                return view;
            }
            throw new NotSupportedException("Constraints only apply to View.Frame.");
        }

        private static int ConstantValue(Expression expr)
        {
            return Convert.ToInt32(Eval(expr));
        }

        private static bool IsConstant(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
            {
                return true;
            }

            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                var mexpr = (MemberExpression)expr;
                // todo: Does this even work, where the heck is MemberType?
                if (mexpr.NodeType == ExpressionType.Constant)
                {
                    return true;
                }
                return false;
            }

            if (expr.NodeType == ExpressionType.Convert)
            {
                var cexpr = (UnaryExpression)expr;
                return IsConstant(cexpr.Operand);
            }

            var methodCall = expr as MethodCallExpression;
            if (methodCall != null)
            {
                return methodCall.Method.Name == nameof(ToConst);
            }

            return false;
        }

        private static LeftExpression ParseLeftExpression(Expression expr)
        {
            Position? position = null;
            MemberExpression viewExpr = null;

            // todo: Do we want extension methods like label.Right() instead of label.Bounds.Right?
            //var fExpr = expr as MethodCallExpression;
            //if (fExpr != null)
            //{
            //    position = GetPosition(fExpr);
            //    viewExpr = fExpr.Arguments.FirstOrDefault() as MemberExpression;
            //}

            if (position == null)
            {
                var memExpr = expr as MemberExpression;
                if (memExpr == null)
                {
                    throw new NotSupportedException("Left hand side of a relation must be a member expression, instead it is " + expr);
                }

                position = GetPosition(memExpr);

                viewExpr = memExpr.Expression as MemberExpression;
                viewExpr = viewExpr?.Expression as MemberExpression;
            }

            if (viewExpr == null)
            {
                throw new NotSupportedException("Constraints should use views's Top, Bottom, etc properties, or extension methods like GetCenter().");
            }

            var eval = Eval(viewExpr);
            var view = eval as View;
            if (view == null)
            {
                throw new NotSupportedException("Constraints only apply to views.");
            }

            return new LeftExpression
            {
                View = view,
                Position = position.Value,
                Name = viewExpr.Member.Name
            };
        }

        private static object Eval(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)expr).Value;
            }

            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                var mexpr = (MemberExpression)expr;
                var m = mexpr.Member;
                //todo: Where the heck is MemberType?
                //if (m.MemberType == MemberTypes.Field)
                //{
                var fieldInfo = m as FieldInfo;
                if (fieldInfo != null)
                {
                    var v = fieldInfo.GetValue(Eval(mexpr.Expression));
                    return v;
                }
            }

            if (expr.NodeType == ExpressionType.Convert)
            {
                var cexpr = (UnaryExpression)expr;
                var op = Eval(cexpr.Operand);
                if (cexpr.Method != null)
                {
                    return cexpr.Method.Invoke(null, new[] { op });
                }
                else
                {
                    return Convert.ChangeType(op, cexpr.Type);
                }
            }

            return Expression.Lambda(expr).Compile().DynamicInvoke();
        }

        private static Position GetPosition(MemberExpression memExpr)
        {
            switch (memExpr.Member.Name)
            {
                case nameof(Rectangle.X):
                    return Position.Left;
                case nameof(Rectangle.Y):
                    return Position.Top;
                case nameof(Rectangle.Left):
                    return Position.Left;
                case nameof(Rectangle.Top):
                    return Position.Top;
                case nameof(Rectangle.Right):
                    return Position.Right;
                case nameof(Rectangle.Bottom):
                    return Position.Bottom;
                case nameof(Rectangle.Width):
                    return Position.Width;
                case nameof(Rectangle.Height):
                    return Position.Height;
                default:
                    throw new NotSupportedException("Property " + memExpr.Member.Name + " is not recognized.");
            }
        }

        private static List<BinaryExpression> FindConstraints(Expression expr)
        {
            var binaryExpressions = new List<BinaryExpression>();
            FindConstraints(expr, binaryExpressions);
            return binaryExpressions;
        }

        private static void FindConstraints(Expression expr, List<BinaryExpression> constraintExprs)
        {
            var b = expr as BinaryExpression;
            if (b == null)
            {
                return;
            }

            if (b.NodeType == ExpressionType.AndAlso)
            {
                FindConstraints(b.Left, constraintExprs);
                FindConstraints(b.Right, constraintExprs);
            }
            else
            {
                constraintExprs.Add(b);
            }
        }
    }
}
