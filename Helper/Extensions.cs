using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ItersTutoriov1.Helper
{
    public static class Extensions
    {
        private static string BASE36 = "0123456789" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string ToBase36(this long input)
        {
            string r = string.Empty;
            int targetBase = BASE36.Length;
            do
            {
                r = string.Format("{0}{1}",
                    BASE36[(int)(input % targetBase)],
                    r);
                input /= targetBase;
            } while (input > 0);

            return r;
        }
        public static long FromBase36(this string input)
        {
            int srcBase = BASE36.Length;
            long id = 0;
            string r = (string) input.Reverse();

            for (int i = 0; i < r.Length; i++)
            {
                int charIndex = BASE36.IndexOf(r[i]);
                id += charIndex * (long)Math.Pow(srcBase, i);
            }

            return id;
        }

        public class PropertyCopier<TParent, TChild> where TParent : class where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();


                foreach (var parentProperty in parentProperties)
                {
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                            break;
                        }
                    }
                }
            }
        }
    }
}
