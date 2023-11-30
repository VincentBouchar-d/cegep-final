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
    public class ListeEnseignantAdapteur : BaseAdapter<EnseignantDTO>
    {

        private Activity context;

        private EnseignantDTO[] listeEnseignant;

        public ListeEnseignantAdapteur(Activity uneActivity, EnseignantDTO[] uneListeEnseignantDTO)
        {
            context = uneActivity;
            listeEnseignant = uneListeEnseignantDTO;
        }
        public override EnseignantDTO this[int position]
        {
            get
            {
                return listeEnseignant[position];
            }
        }

        public override int Count
        {
            get
            {
                return listeEnseignant.Length;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.listeEnseignantItems, null);
            view.FindViewById<TextView>(Resource.Id.tvNom).Text = listeEnseignant[position].NoEmploye + "   " + listeEnseignant[position].Prenom + " " + listeEnseignant[position].Nom;
            return view;
        }
    }
}