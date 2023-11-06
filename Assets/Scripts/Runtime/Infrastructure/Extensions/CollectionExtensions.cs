using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RussSurvivor.Runtime.Infrastructure.Extensions
{
  public static class CollectionExtensions
  {
    public static IEnumerable<T> RandomElements<T>(this IEnumerable<T> collection, int count)
    {
      if (count <= 0 || collection == null || collection.Count() < count)
        throw new InvalidExpressionException();

      return collection.OrderBy(x => UnityEngine.Random.value).Take(count);
    }
  }
}