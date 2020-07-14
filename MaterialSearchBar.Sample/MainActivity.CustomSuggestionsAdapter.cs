using System.Collections.Generic;
using System.Linq;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using tk.supernovaic.MaterialSearchBar.Adapter;

namespace MaterialSearchBar.Sample
{
    public partial class MainActivity
    {
        public partial class CustomSuggestionsAdapter : SuggestionsAdapter
        {
            private List<Product> Products { get; set; }

            public CustomSuggestionsAdapter(LayoutInflater inflater) : base(inflater)
            {
            }

            public void UpdateSuggestions(List<Product> products)
            {
                Products = products;
            }

            public override int GetSingleViewHeight()
            {
                return 0;
            }

            public override void OnBindSuggestionHolder(string suggestion, RecyclerView.ViewHolder holder, int position)
            {
                var sH = holder as SuggestionHolder;

                var currentProduct = Products.First(x => x.Title == suggestion);

                sH.Title.Text = currentProduct.Title;
                sH.SubTitle.Text = currentProduct.SubTitle;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                using (var view = Inflater.Inflate(Resource.Layout.item_custom_suggestion, parent, false))
                {
                    return new SuggestionHolder(view);
                }
            }
        }
    }
}