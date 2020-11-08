# Enum作Key的问题

**Dictionary**的key必须是唯一的标识，因此**Dictionary**需要对 key进行判等的操作，如果key的类型没有实现 IEquatable接口，则默认根据System.Object.Equals()和GetHashCode()方法判断值是否相等。我们可以看看常用作key的几种类型在.NET Framework中的定义：
``` csharp
public sealed class String : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<string>, IEnumerable, IEquatable<string> 

public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int> 

public abstract class **Enum** : ValueType, IComparable, IFormattable, IConvertible
```
注意**Enum**类型的定义与前两种类型的不同，它并没有实现IEquatable接口。因此，当我们使用**Enum**类型作为key值时，**Dictionary**的内部操作就需要将**Enum**类型转换为System.Object，这就导致了Boxing的产生。它是导致**Enum**作为 key值的性能瓶颈。