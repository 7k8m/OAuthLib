using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace OAuthLib
{
    /// <summary>
    /// Parameter with name and value.
    /// </summary>
    public class Parameter
    {
        private String _name;
        private String _value;

        /// <summary>
        /// Name of parameter
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Value of parameter
        /// </summary>
        public String Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Construct parameter
        /// </summary>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        public Parameter(String name, String value)
        {
            _name = name;
            _value = value;
        }

        private static Parameter[] SortToNormalize(Parameter[] parameterArray)
        {
            List<Parameter> l = new List<Parameter>(parameterArray);
            l.Sort(
                delegate(Parameter x, Parameter y)
                {
                    if (!x._name.Equals(y._name))
                        return Uri.EscapeDataString(x._name).CompareTo(
                            Uri.EscapeDataString(y._name)
                        );
                    else
                        return Uri.EscapeDataString(x._value).CompareTo(
                                Uri.EscapeDataString(y._value)
                            );
                }
            );

            return l.ToArray();

        }

        internal static String ConcatToNormalize(Parameter[] parameterArray)
        {
            parameterArray = SortToNormalize(parameterArray);
            return ConCat(parameterArray,"");

        }

        internal static string ConCat(Parameter[] parameterArray)
        {
            return ConCat(parameterArray, "");
        }

        internal static string ConCat(Parameter[] parameterArray,String qutationMark)
        {

            if (parameterArray.Length == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            foreach (Parameter parameter in parameterArray)
            {
                sb.Append(qutationMark + Uri.EscapeDataString(parameter._name) + qutationMark);
                sb.Append("=");
                sb.Append(qutationMark + Uri.EscapeDataString(parameter._value) + qutationMark);
                sb.Append("&");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        internal static Parameter[] ConCatAsArray(
            params Parameter[][] parameterArrayArray
        )
        {
            List<Parameter> result = new List<Parameter>();
            foreach (Parameter[] array in parameterArrayArray)
            {
                result.AddRange(array);
            }

            return result.ToArray();

        }

        internal static Parameter[] Parse(String source)
        {

            List<Parameter > list =new List<Parameter> ();

            String[] nameAndValSetArray = source.Split('&');

            foreach (String nameAndValSet in nameAndValSetArray ){

                String[] nameAndVal = nameAndValSet.Split('=');

                nameAndVal[0] = Uri.UnescapeDataString(nameAndVal[0]);
                nameAndVal[1] = Uri.UnescapeDataString(nameAndVal[1]);

                list.Add(
                    new Parameter(
                        nameAndVal[0],
                        nameAndVal[1]
                    )
                );
                            
            }

            return list.ToArray();

        }

    }
}
