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
    public class ListeDepartementAdapteur : BaseAdapter<DepartementDTO>
    {

        private Activity context;

        private DepartementDTO[] listeDepartement;

        public ListeDepartementAdapteur(Activity uneActivity, DepartementDTO[] uneListeDepartementDTO)
        {
            context = uneActivity;
            listeDepartement = uneListeDepartementDTO;
        }
        public override DepartementDTO this[int position]
        {
            get
            {
                return listeDepartement[position];
            }
        }

        public override int Count
        {
            get
            {
                return listeDepartement.Length;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.listeDepartementItems, null);
            view.FindViewById<TextView>(Resource.Id.tVNom).Text = listeDepartement[position].Nom;
            return view;
        }
    }
}