using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class SuggestionsAdapter : RecyclerView.Adapter, IFilterable
    {
        protected readonly LayoutInflater Inflater;
        public static List<string> Suggestions { get; set; }
        protected static List<string> Suggestions_clone { get; set; }
        public static int MaxSuggestionsCount { get; set; }

        public void AddSuggestion(string r)
        {
            if (MaxSuggestionsCount <= 0)
            {
                return;
            }

            if (r == null)
            {
                return;
            }
            if (!Suggestions.Contains(r))
            {
                if (Suggestions.Count >= MaxSuggestionsCount)
                {
                    Suggestions.RemoveAt(MaxSuggestionsCount - 1);
                }
                Suggestions.Insert(0, r);
            }
            else
            {
                Suggestions.Remove(r);
                Suggestions.Insert(0, r);
            }
            Suggestions_clone = Suggestions;
            NotifyDataSetChanged();
        }

        public void SetSuggestions(List<string> suggestions)
        {
            Suggestions = suggestions;
            Suggestions_clone = suggestions;
            NotifyDataSetChanged();
        }

        public void ClearSuggestions()
        {
            Suggestions.Clear();
            Suggestions_clone = Suggestions;
            NotifyDataSetChanged();
        }

        public void DeleteSuggestion(int position, string r)
        {
            if (r == null)
            {
                return;
            }
            //delete item with animation
            if (Suggestions.Contains(r))
            {
                NotifyItemRemoved(position);
                Suggestions.Remove(r);
                Suggestions_clone = Suggestions;
            }
        }

        public int GetMaxSuggestionsCount()
        {
            return MaxSuggestionsCount;
        }

        public List<string> GetSuggestions()
        {
            return Suggestions;
        }

        public void SetMaxSuggestionsCount(int maxSuggestionsCount)
        {
            MaxSuggestionsCount = maxSuggestionsCount;
        }

        protected LayoutInflater GetLayoutInflater()
        {
            return Inflater;
        }

        public SuggestionsAdapter(LayoutInflater inflater)
        {
            Inflater = inflater;
            Suggestions = new List<string>();
            Suggestions_clone = new List<string>();
            MaxSuggestionsCount = 5;
        }

        public virtual int GetSingleViewHeight() { return 0; }

        public int GetListHeight()
        {
            return ItemCount * GetSingleViewHeight();
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public virtual void OnBindSuggestionHolder(string suggestion, RecyclerView.ViewHolder holder, int position)
        {

        }

        public override int ItemCount => Suggestions.Count;

        public Filter Filter { get; set; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            holder.IsRecyclable = false;
            OnBindSuggestionHolder(Suggestions[position], holder, position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return null;
        }
    }
}