using System;
using System.Collections.Generic;
using System.Linq;
using ComposableRegex.Models;

namespace ComposableRegex.Controllers.RegexWorkers
{
    public class Regexer
    {
        public String Regex { get; private set; }

        public List<String> DebugTrace { get; private set; }

        public List<TreeData> Groups { get; private set; }

        public Regexer(string regex, bool includesComments = true, bool useDebug = true)
        {
            if (String.IsNullOrEmpty(regex))
            {
                throw new ArgumentNullException("regex", "Regex cannot be null");
            }

            DebugTrace = new List<string>();

            Parse(regex, includesComments, useDebug);
        }

        private void Parse(string regex, bool includeComments, bool useDebug)
        {
            var splits = regex.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Count() == 1)
            {
                Regex = splits.First();
                return;
            }

            Groups = (from item in splits.Take(splits.Count() - 1)
                          where !item.StartsWith("##")
                          let keyValueSplit = item.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries)
                          let key = keyValueSplit.First().Trim()
                          let value = keyValueSplit.Last().Trim()
                          where !String.IsNullOrEmpty(key)
                          where !String.IsNullOrEmpty(value)
                          let regexWithoutComments = includeComments ? value.Split(new[] { "##" }, 2, StringSplitOptions.RemoveEmptyEntries).First().Trim() : value
                          select new TreeData { Key = key, Value = regexWithoutComments }).ToList();

            var final = splits.Last().Trim();

            Regex = final;

            for (int i = Groups.Count - 1; i >= 0; i--)
            {
                var item = Groups[i];
                Regex = Regex.Replace(item.Key, item.Value);
                if (useDebug)
                {
                    DebugTrace.Add(Regex);
                }
            }
        }
    }
}