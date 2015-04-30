using System;
using System.Text;

namespace Interpreter
{
    public abstract partial class SyntaxTree
    {
        public SyntaxTree[] Children { get; protected set; }
        public abstract string Label { get; }

        public virtual void WriteScheme(StringBuilder b)
        {
            b.Append("(");
            b.Append(Label);
            foreach (var child in Children)
            {
                b.Append(" ");
                child.WriteScheme(b);
            }
            b.Append(")");
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            WriteScheme(b);
            return b.ToString();
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public abstract object Run(Dictionary dict);
    }

    /// <summary>
    /// A syntax tree with no children, e.g. a constant or variable reference.
    /// </summary>
    abstract class SyntaxTreeLeaf : SyntaxTree
    {
        static readonly SyntaxTree[] noChildren = new SyntaxTree[0];
        protected SyntaxTreeLeaf()
        {
            Children = noChildren;
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append(Label);
        }
    }

    class Constant : SyntaxTreeLeaf
    {
        public object Value { get; private set; }
        public Constant(object constantValue)
        {
            Value = constantValue;
        }

        public override string Label
        {
            get { return Value.ToString(); }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class VariableReference : SyntaxTreeLeaf
    {
        public string VariableName { get; private set; }

        public VariableReference(string variableName)
        {
            VariableName = variableName;
        }

        public override string Label
        {
            get { return VariableName; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class VariableAssignment : SyntaxTreeLeaf
    {
        public string VariableName { get; private set; }
        public SyntaxTree ValueExpression { get; private set; }

        public VariableAssignment(string variableName, SyntaxTree value)
        {
            VariableName = variableName;
            ValueExpression = value;
            Children = new[] { value };
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(set! ");
            b.Append(VariableName);
            b.Append(" ");
            ValueExpression.WriteScheme(b);
            b.Append(")");
        }

        public override string Label
        {
            get { return "set!"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class MemberReference : SyntaxTree
    {
        public SyntaxTree ObjectExpression { get; private set; }
        public string MemberName { get; private set; }

        public MemberReference(SyntaxTree oExpression, string member)
        {
            ObjectExpression = oExpression;
            MemberName = member;
            Children = new[] { oExpression };
        }

        public override void WriteScheme(StringBuilder b)
        {
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MemberName);
        }

        public override string Label
        {
            get { return "member"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class MemberAssignment : SyntaxTree
    {
        public SyntaxTree ObjectExpression { get; private set; }
        public string MemberName { get; private set; }
        public SyntaxTree ValueExpression { get; private set; }

        public MemberAssignment(SyntaxTree oExpression, string member, SyntaxTree value)
        {
            ObjectExpression = oExpression;
            MemberName = member;
            ValueExpression = value;
            Children = new[] { oExpression, value };
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(set-member! ");
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MemberName);
            b.Append(" ");
            ValueExpression.WriteScheme(b);
            b.Append(")");
        }

        public override string Label
        {
            get { return "member"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class MethodCall : SyntaxTree
    {
        public SyntaxTree ObjectExpression { get; private set; }
        public string MethodName { get; private set; }
        public SyntaxTree[] Arguments { get; private set; }

        public MethodCall(SyntaxTree oExpression, string method, params SyntaxTree[] args)
        {
            ObjectExpression = oExpression;
            MethodName = method;
            Arguments = args;
            var children = new SyntaxTree[args.Length + 1];
            children[0] = oExpression;
            args.CopyTo(children, 1);
            Children = children;
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(");
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MethodName);
            foreach (var arg in Arguments)
            {
                b.Append(" ");
                arg.WriteScheme(b);
            }
            b.Append(")");

        }

        public override string Label
        {
            get { return "call"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }

    class OperatorExpression : SyntaxTree
    {
        /// <summary>
        /// The operator, but this can't be called "operator" because that's a C# keyword.
        /// </summary>
        readonly Operator operation;

        public OperatorExpression(Operator op, params SyntaxTree[] args)
        {
            operation = op;
            Children = args;
        }

        public override string Label
        {
            get { return operation.Name; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            throw new NotImplementedException();
        }
    }
}
