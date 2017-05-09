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
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace EasyLayout.Forms
{
    public static class EasyLayoutForms
    {
        private struct Margin
        {
            public double? Right { get; set; }
            public double? Left { get; set; }
            public double? Top { get; set; }
            public double? Bottom { get; set; }
        }

        private class Assertion
        {
            public Assertion(View view)
            {
                View = view;
            }

            public View View { get; }
            public Guid? RelativeToViewId { get; private set; }
            public Margin Margin { get; private set; }
            private LeftExpression LeftExpression { get; set; }
            private RightExpression RightExpression { get; set; }
            public string LeftName => LeftExpression.Name;

            public void Initialize(LeftExpression leftExpression, RightExpression rightExpression)
            {
                if (!rightExpression.IsParent)
                {
                    RelativeToViewId = rightExpression.Id;
                }
                SetMargin(leftExpression, rightExpression);
                LeftExpression = leftExpression;
                RightExpression = rightExpression;
            }

            public Constraint GetConstraint(Assertion widthHeightAssertion = null)
            {
                if (RightExpression.IsConstant && RightExpression.Constant.HasValue)
                {
                    var constraint = Constraint.Constant(RightExpression.Constant.Value);
                    return constraint;
                }
                if (RightExpression.IsParent)
                {
                    return GetLayoutRuleForParent(widthHeightAssertion);
                }
                return GetLayoutRuleForSibling(LeftExpression.Position, RightExpression.Position,
                    LeftExpression.Name, RightExpression.Name);
            }

            private static double GetWidth(RelativeLayout relativeLayout, Guid id, double? widthAssertion)
            {
                if (widthAssertion.HasValue && widthAssertion > 0) return widthAssertion.Value;
                var child = GetChildById(relativeLayout, id);
                if (child == null) return 0;
                return child.Width != -1 ? child.Width : GetSize(relativeLayout, child).Width;
            }

            private static double GetHeight(RelativeLayout relativeLayout, Guid id, double? heightAssertion)
            {
                if (heightAssertion.HasValue && heightAssertion > 0) return heightAssertion.Value;
                var child = GetChildById(relativeLayout, id);
                if (child == null) return 0;
                return child.Height != -1 ? child.Height : GetSize(relativeLayout, child).Height;
            }

            /// <summary>
            /// Thought getting the height/width of an element in a constraint would be easy?  Hah.
            /// http://stackoverflow.com/questions/40942691/xamarin-forms-how-to-center-views-using-relative-layout-width-and-height-r
            /// </summary>
            private static Size GetSize(RelativeLayout relativeLayout, View visualElement)
            {
                if (visualElement == null) return Size.Zero;
                if (relativeLayout == null) return Size.Zero;
                return visualElement.Measure(relativeLayout.Width, relativeLayout.Height).Request;
            }

            private static View GetChildById(RelativeLayout relativeLayout, Guid id) => relativeLayout.Children.FirstOrDefault(i => i.Id == id);

            private Constraint GetLayoutRuleForParent(Assertion widthHeightAssertion)
            {
                Position childPosition = LeftExpression.Position;
                Position parentPosition = RightExpression.Position;
                string childName = LeftExpression.Name;
                Guid childId = LeftExpression.View.Id;

                var heightWidthConstant = widthHeightAssertion?.RightExpression.Constant;

                // X Constraints
                if (childPosition == Position.Left && parentPosition == Position.Left)
                    return Constraint.RelativeToParent(rl => rl.X);
                if (childPosition == Position.Right && parentPosition == Position.Right)
                    return Constraint.RelativeToParent(rl => rl.Width - GetWidth(rl, childId, heightWidthConstant));
                if (childPosition == Position.CenterX && parentPosition == Position.CenterX)
                    return Constraint.RelativeToParent(rl => (rl.Width * .5f) - (GetWidth(rl, childId, heightWidthConstant) * .5f));

                // Y Constraints
                if (childPosition == Position.Top && parentPosition == Position.Top)
                    return Constraint.RelativeToParent(rl => rl.Y);
                if (childPosition == Position.Bottom && parentPosition == Position.Bottom)
                    return Constraint.RelativeToParent(rl => rl.Height - GetHeight(rl, childId, heightWidthConstant));
                if (childPosition == Position.CenterY && parentPosition == Position.CenterY)
                    return Constraint.RelativeToParent(rl => (rl.Height * .5f) - (GetHeight(rl, childId, heightWidthConstant) * .5f));

                // todo: support more parent layout constraints
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

            private static Constraint GetLayoutRuleForSibling(Position leftPosition, Position rightPosition, string leftExpressionName, string rightExpressionName)
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

            public bool IsExplicitWidthAssertion()
            {
                return LeftExpression.Position == Position.Width;
            }

            public bool IsExplicitHeightAssertion()
            {
                return LeftExpression.Position == Position.Height;
            }

            public bool IsXConstraint()
            {
                return LeftExpression.Position == Position.Left ||
                       LeftExpression.Position == Position.Right ||
                       LeftExpression.Position == Position.CenterX;
            }

            public bool IsYConstraint()
            {
                return LeftExpression.Position == Position.Top ||
                       LeftExpression.Position == Position.Bottom ||
                       LeftExpression.Position == Position.CenterY;
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
            public double? Constant { get; set; }
            public bool IsConstant => !IsParent && Id == null && Constant != null;
        }

        public static double GetCenterX(this Rectangle rectangle)
        {
            return rectangle.Left + (rectangle.Width / 2);
        }

        public static double GetCenterY(this Rectangle rectangle)
        {
            return rectangle.Top + (rectangle.Height / 2);
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

        private static void UpdateLayoutParamsWithRules(RelativeLayout relativeLayout, IEnumerable<Assertion> viewAndRule)
        {
            var assertionsGroupedByView = viewAndRule.GroupBy(i => i.View);

            foreach (var viewAndRules in assertionsGroupedByView)
            {
                var view = viewAndRules.Key;

                var widthAssertion = GetExplicitWidthConstraint(viewAndRules);
                var heightAssertion = GetExplicitHeightConstraint(viewAndRules);
                var widthConstraint = widthAssertion?.GetConstraint();
                var heightConstraint = heightAssertion?.GetConstraint();
                var xConstraint = GetXConstraint(viewAndRules, widthAssertion);
                var yConstraint = GetYConstraint(viewAndRules, heightAssertion);

                relativeLayout.Children.Add(view, xConstraint, yConstraint, widthConstraint, heightConstraint);

                var thickness = CombineMargins(viewAndRules);
                view.Margin = thickness;
            }
        }

        private static Thickness CombineMargins(IGrouping<View, Assertion> viewAndRules)
        {
            double left = 0, top = 0, right = 0, bottom = 0;

            foreach (var rule in viewAndRules)
            {
                var margin = rule.Margin;
                if (margin.Left.HasValue)
                    left = margin.Left.Value;
                if (margin.Right.HasValue)
                    right = margin.Right.Value;
                if (margin.Top.HasValue)
                    top = margin.Top.Value;
                if (margin.Bottom.HasValue)
                    bottom = margin.Bottom.Value;
            }

            return new Thickness(left, top, right, bottom);
        }

        private static Constraint GetXConstraint(IGrouping<View, Assertion> assertions, Assertion widthAssertion)
        {
            var assertion = GetSingleAssertionOrDefault(assertions, i => i.IsXConstraint(), "Multiple assertions found affecting X");
            return assertion?.GetConstraint(widthAssertion);
        }

        private static Constraint GetYConstraint(IGrouping<View, Assertion> assertions, Assertion heightAssertion)
        {
            var assertion = GetSingleAssertionOrDefault(assertions, i => i.IsYConstraint(), "Multiple assertions found affecting Y");
            return assertion?.GetConstraint(heightAssertion);
        }

        private static Assertion GetExplicitHeightConstraint(IGrouping<View, Assertion> assertions)
        {
            return GetSingleAssertionOrDefault(assertions, i => i.IsExplicitHeightAssertion(), "Multiple height assertions found");
        }

        private static Assertion GetExplicitWidthConstraint(IGrouping<View, Assertion> assertions)
        {
            return GetSingleAssertionOrDefault(assertions, i => i.IsExplicitWidthAssertion(), "Multiple width assertions found");
        }

        private static Assertion GetSingleAssertionOrDefault(IGrouping<View, Assertion> viewsGroupedByRule, Func<Assertion, bool> func, string errorMessage)
        {
            var assertions = viewsGroupedByRule.Where(func).ToList();
            if (assertions.Count > 1) throw new ArgumentException(".  Element name: " + viewsGroupedByRule.First().LeftName);
            if (assertions.Count == 0) return null;
            return assertions[0];
        }

        private static IEnumerable<Assertion> ConvertConstraintsToRules(RelativeLayout relativeLayout, List<BinaryExpression> constraintExpressions)
        {
            return constraintExpressions.Select(i => GetViewAndRule(i, relativeLayout));
        }

        private static Assertion GetViewAndRule(BinaryExpression expr, RelativeLayout relativeLayout)
        {
            var leftExpression = ParseLeftExpression(expr.Left);
            var rightExpression = ParseRightExpression(expr.Right, relativeLayout);
            return GetRule(leftExpression, rightExpression);
        }

        private static Assertion GetRule(LeftExpression leftExpression, RightExpression rightExpression)
        {
            var rule = new Assertion(leftExpression.View);
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
                var fExpr = expr as MethodCallExpression;
                if (fExpr != null)
                {
                    position = GetPosition(fExpr);
                    memberExpression = fExpr.Arguments.FirstOrDefault() as MemberExpression;
                }
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
                if (viewExpression == null)
                {
                    return Eval(memberExpression) as View;
                }

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

            var fExpr = expr as MethodCallExpression;
            if (fExpr != null)
            {
                position = GetPosition(fExpr);
                viewExpr = fExpr.Arguments.FirstOrDefault() as MemberExpression;
            }

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

            var view = GetView(viewExpr);

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

        private static Position GetPosition(MethodCallExpression fExpr)
        {
            switch (fExpr.Method.Name)
            {
                case nameof(GetCenterX):
                    return Position.CenterX;
                case nameof(GetCenterY):
                    return Position.CenterY;
                default:
                    throw new NotSupportedException("Method " + fExpr.Method.Name + " is not recognized.");
            }
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
                case nameof(Rectangle.Center):
                    return Position.Center;
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
