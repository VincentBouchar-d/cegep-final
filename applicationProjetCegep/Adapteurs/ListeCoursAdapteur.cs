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
    public class ListeCoursAdapteur : BaseAdapter<CoursDTO>
    {
        /// <summary>
        /// Variable représentant l'activité
        /// </summary>
        private Activity context;
        /// <summary>
        /// Variable représentant la liste des cours
        /// </summary>
        private CoursDTO[] listeCours;
        /// <summary>
        /// Fonciton qui donne les valueurs aux variables context et listeCours
        /// </summary>
        /// <param name="uneActivity"></param>
        /// <param name="uneListeCoursDTO"></param>
        public ListeCoursAdapteur(Activity uneActivity, CoursDTO[] uneListeCoursDTO)
        {
            context = uneActivity;
            listeCours = uneListeCoursDTO;
        }

        public override CoursDTO this[int position]
        {
            get
            {
                return listeCours[position];
            }
        }

        public override int Count
        {
            get
            {
                return listeCours.Length;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.listeCoursItems, null);
            view.FindViewById<TextView>(Resource.Id.tvNom).Text = listeCours[position].No + "   " + listeCours[position].Nom;
            return view;
        }
    }
}