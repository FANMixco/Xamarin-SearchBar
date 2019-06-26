using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Com.Mancj.Materialsearchbar.Adapter
{
    public abstract class SuggestionsAdapter<S, V> : RecyclerView.Adapter, IFilterable
    {
        private readonly LayoutInflater Inflater;
        protected List<S> Suggestions = new List<S>();
        protected List<S> Suggestions_clone = new List<S>();
        protected int MaxSuggestionsCount = 5;

        public void AddSuggestion(S r)
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

        public void SetSuggestions(List<S> suggestions)
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

        public void DeleteSuggestion(int position, S r)
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

        public List<S> GetSuggestions()
        {
            return Suggestions;
        }

        public int GetMaxSuggestionsCount()
        {
            return MaxSuggestionsCount;
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
        }

        public abstract int GetSingleViewHeight();

        public int GetListHeight()
        {
            return ItemCount * GetSingleViewHeight();
        }

        public abstract void OnBindSuggestionHolder(S suggestion, RecyclerView.ViewHolder holder, int position);

        public override int ItemCount => Suggestions.Count;

        public Filter Filter => null;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OnBindSuggestionHolder(Suggestions[position], holder, position);
        }

        public interface IOnItemViewClickListener
        {
            void OnItemClickListener(int position, View v);
            void OnItemDeleteListener(int position, View v);
        }
    }
}
