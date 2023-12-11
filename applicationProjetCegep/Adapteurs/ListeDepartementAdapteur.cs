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
        /// <summary>
        /// Variable qui représente l'activité
        /// </summary>
        private Activity context;
        /// <summary>
        /// Variable représentant la liste des départements
        /// </summary>
        private DepartementDTO[] listeDepartement;
        /// <summary>
        /// Focntion que donne les valeurs aux variables context et listeDepartement
        /// </summary>
        /// <param name="uneActivity"></param>
        /// <param name="uneListeDepartementDTO"></param>
        public ListeDepartementAdapteur(Activity uneActivity, DepartementDTO[] uneListeDepartementDTO)
        {
            context = uneActivity;
            listeDepartement = uneListeDepartementDTO;
        }
        /// <summary>
        /// Fonction qui retourne un departement selon la position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override DepartementDTO this[int position]
        {
            get
            {
                return listeDepartement[position];
            }
        }
        /// <summary>
        /// Fonciton qui retourne le nombre de départements dans la liste
        /// </summary>
        public override int Count
        {
            get
            {
                return listeDepartement.Length;
            }
        }
        /// <summary>
        /// Fonciton qui retourne la position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override long GetItemId(int position)
        {
            return position;
        }
        /// <summary>
        /// Fonction qui détermine l'affichage des départements dans un listView
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