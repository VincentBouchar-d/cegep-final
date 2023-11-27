using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace applicationProjetCegep.Adapteurs
{
    public class ListeCegepAdapteur : BaseAdapter<CegepDTO>
    {

        private Activity context;

        private CegepDTO[] listeCegep;

        public ListeCegepAdapteur(Activity uneActivity, CegepDTO[] uneListeCegepDTO)
        {
            context = uneActivity;
            listeCegep = uneListeCegepDTO;
        }
        public override CegepDTO this[int position] 
        {
            get
            {
                return listeCegep[position]; 
            }
        }

        public override int Count 
        {
            get
            {
                return listeCegep.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.listeCegepItems, null);
            view.FindViewById<TextView>(Resource.Id.tVNom).Text = listeCegep[position].Nom;
            return view;
        }
    }
}