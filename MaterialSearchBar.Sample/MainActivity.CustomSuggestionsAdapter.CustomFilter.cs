using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using Java.Lang;

namespace MaterialSearchBar.Sample
{
    public partial class MainActivity
    {
        public partial class CustomSuggestionsAdapter
        {
            public class CustomFilter : Filter
            {
                public CustomSuggestionsAdapter Adapter { get; set; }
                private List<string> Suggestions_backup { get; set; }

                public CustomFilter(CustomSuggestionsAdapter adapter)
                {
                    Adapter = adapter;
                }

                protected override FilterResults PerformFiltering(ICharSequence constraint)
                {
                    if (Suggestions_backup == null)
                    {
                        Suggestions_backup = Suggestions_clone;
                    }

                    FilterResults results = new FilterResults();
                    string term = constraint.ToString();

                    if (!string.IsNullOrEmpty(term))
                    {
                        Suggestions = Suggestions_backup.Where(x => x.ToLower().Contains(term.ToLower())).Distinct().ToList();
                    }
                    else
                    {
                        Suggestions = Suggestions_backup;
                    }
                    results.Values = GetJavaObjects(Suggestions);
                    return results;
                }

                private Object[] GetJavaObjects(List<string> suggestions)
                {
                    Object[] resultsValues = new Object[suggestions.Count()];
                    for (var i = 0; i < suggestions.Count; i++)
                    {
                        resultsValues[i] = suggestions[i];
                    }
                    return resultsValues;
                }

                protected override void PublishResults(ICharSequence constraint, FilterResults results)
                {
                    Suggestions.Clear();
                    foreach (var item in (Object[])results.Values)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        {
                            Suggestions.Add(item.ToString());
                        }
                    }
                    Adapter.SetSuggestions(Suggestions);

                    Adapter.NotifyDataSetChanged();
                }
            }
        }
    }
}