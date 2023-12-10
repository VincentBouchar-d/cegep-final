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
        /// <summary>
        /// Variable représentant l'activité
        /// </summary>
        private Activity context;

        /// <summary>
        /// Liste de CégepDTO
        /// </summary>
        private CegepDTO[] listeCegep;
        /// <summary>
        /// Fonciton qui donne les valeurs aux variables context et listeCegep
        /// </summary>
        /// <param name="uneActivity"></param>
        /// <param name="uneListeCegepDTO"></param>
        public ListeCegepAdapteur(Activity uneActivity, CegepDTO[] uneListeCegepDTO)
        {
            context = uneActivity;
            listeCegep = uneListeCegepDTO;
        }/// <summary>
         /// Fonction qui retourne la position
         /// </summary>
         /// <param name="position"></param>
         /// <returns></returns>
        public override CegepDTO this[int position] 
        {
            get
            {
                return listeCegep[position]; 
            }
        }
        /// <summary>
        /// Fonction qui retourne le nombre de cégeps dans la liste
        /// </summary>
        public override int Count 
        {
            get
            {
                return listeCegep.Length;
            }
        }
        /// <summary>
        /// Fonction qui retourne la position de l'objet
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override long GetItemId(int position)
        {
            return position;
        }
        /// <summary>
        /// Fonction qui détermine la vue dans un listeView
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