using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[Equals]
public class NormalClass
{
    public int X { get; set; }

    public string Y { get; set; }

    public double Z { get; set; }

    public char V { get; set; }

    public static bool operator ==(NormalClass left, NormalClass right) => Operator.Weave(left, right);
    public static bool operator !=(NormalClass left, NormalClass right) => Operator.Weave(left, right);
}

public class NormalClassResult : IEquatable<NormalClassResult>
{
    static readonly IEqualityComparer<string> YComparer = StringComparer.OrdinalIgnoreCase;

    public int X { get; set; }

    [EqualityComparer(nameof(YComparer))]
    public string Y { get; set; }

    public double Z { get; set; }

    public char V { get; set; }

    public static bool operator ==(NormalClassResult left, NormalClassResult right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(NormalClassResult left, NormalClassResult right)
    {
        return !Equals(left, right);
    }

    static bool EqualsInternal(NormalClassResult left, NormalClassResult right)
    {
        if (left.X != right.X)
        {
            return false;
        }

        if (!YComparer.Equals(left.Y, right.Y))
        {
            return false;
        }

        if (left.Z != right.Z) // TODO: R# says Floating point comparison is bad, do we handle in the IL?
        {
            return false;
        }

        if (left.V != right.V)
        {
            return false;
        }

        return true;
    }

    public virtual bool Equals(NormalClassResult right)
    {
        return !ReferenceEquals(null, right) && (ReferenceEquals(this, right) || EqualsInternal(this, right));
    }

    bool IEquatable<NormalClass>.Equals(NormalClass other)
    {
        return !ReferenceEquals(null, right) && (ReferenceEquals(this, right) || EqualsInternal(this, right));
    }

    public override bool Equals(object right)
    {
        return !ReferenceEquals(null, right) && (ReferenceEquals(this, right) || GetType() == right.GetType() && EqualsInternal(this, (NormalClassResult)right));
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (((X.GetHashCode()) * 397 ^ (Y == null ? 0 : Y.GetHashCode())) * 397 ^ Z.GetHashCode()) * 397 ^ V.GetHashCode();
        }
    }
}